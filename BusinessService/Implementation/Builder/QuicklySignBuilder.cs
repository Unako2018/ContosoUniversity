using Microsoft.Extensions.Configuration;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf.IO;
using Sanlam.AdvicePartner.Domain.Audit.Commands;
using Sanlam.AdvicePartner.Domain.Common.Commands;
using Sanlam.AdvicePartner.Domain.Common.Models;
using Sanlam.AdvicePartner.Domain.Core.DecisionObjects;
using Sanlam.AdvicePartner.Domain.Core.DecisionObjects.Models;
using Sanlam.AdvicePartner.Domain.Core.DistributionAudit;
using Sanlam.AdvicePartner.Domain.Core.ValueObjects;
using Sanlam.AdvicePartner.Domain.Core.ValueObjects.Models;
using Sanlam.AdvicePartner.Domain.Documents.Constants;
using Sanlam.AdvicePartner.Domain.Helpers;
using Sanlam.AdvicePartner.Domain.Helpers.Models;
using Sanlam.AdvicePartner.Domain.Models;
using Sanlam.AdvicePartner.Domain.Prospects.Commands;
using Sanlam.AdvicePartner.Domain.Prospects.Constants;
using Sanlam.AdvicePartner.Domain.Prospects.Mapping;
using Sanlam.AdvicePartner.Domain.Repositories.Uow;
using Sanlam.AdvicePartner.Domain.Services;
using Sanlam.AdvicePartner.Domain.Services.DistributionUser.Models;
using Sanlam.AdvicePartner.Domain.Services.Documents;
using Sanlam.AdvicePartner.Domain.Services.OnDemandDocument;
using Microsoft.EntityFrameworkCore;
using Sanlam.AdvicePartner.Integrations.ConnectedClients.QuicklySign.Constants;
using Sanlam.AdvicePartner.Integrations.ConnectedClients.QuicklySign.Dto.Request;
using Sanlam.AdvicePartner.Integrations.ConnectedClients.QuicklySign.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Sanlam.AdvicePartner.Domain.Prospects.Builder
{
    public class QuicklySignBuilder
    {
        private readonly AuthenticateClientRequestCommand _requestCommand;
        private readonly CancellationToken _cancellationToken;
        private readonly IQuicklySignApiManager _quicklySignApiManager;
        private readonly IIntermediaryService IntermediaryService;
        private readonly IValueObjectService _valueObjectFactory;
        private readonly IDocumentService _documentService;
        private readonly IDistributionAuditService _distributionAuditService;
        private readonly IOnDemandDocumentService _onDemandDocumentService;
        private readonly IDecisionObjectService _decisionObjectFactory;
        private readonly IUnitOfWork _uoW;
        private readonly string GeneratedLinkAdvicePartnerGatewayUrl;
        private readonly string _applicationVersion;
        private Prospect Prospect { get; }
        private string OnlineEsignApprovalTemplate { get; }
        private List<string> EntityKeys { get; set; } = new List<string>();

        public QuicklySignBuilder(
            Prospect prospect,
            IIntermediaryService intermediaryService,
            IQuicklySignApiManager quicklySignApiManager,
            IValueObjectService valueObjectFactory,
            IDistributionAuditService distributionAuditService,
            IOnDemandDocumentService onDemandDocumentService,
            IDocumentService documentService,
            IDecisionObjectService decisionObjectFactory,
        IConfiguration configuration,
            IUnitOfWork uoW,
            AuthenticateClientRequestCommand requestCommand,
            CancellationToken cancellationToken)
        {
            _quicklySignApiManager = quicklySignApiManager;
            _valueObjectFactory = valueObjectFactory;
            _documentService = documentService;
            _distributionAuditService = distributionAuditService;
            _uoW = uoW;
            Prospect = prospect;
            IntermediaryService = intermediaryService;
            _onDemandDocumentService = onDemandDocumentService;
            _decisionObjectFactory = decisionObjectFactory;
            _cancellationToken = cancellationToken;
            _requestCommand = requestCommand;
            var htmlTemplatePath = configuration.GetSection("QuicklyESigningHTML");
            if (htmlTemplatePath != null)
            {
                OnlineEsignApprovalTemplate = htmlTemplatePath.Value;
            }

            htmlTemplatePath = configuration.GetSection("GeneratedLinkAdvicePartnerGatewayUrl");
            if (htmlTemplatePath != null)
            {
                GeneratedLinkAdvicePartnerGatewayUrl = htmlTemplatePath.Value;
            }

            var appVersionSection = configuration.GetSection("ApplicationVersion");
            _applicationVersion = appVersionSection.Value ?? "-";
        }

        public async Task<Result<bool, FailedResult>> BuildAsync()
        {

            var intermediary = await IntermediaryService.GetIntermediaryDetail(Prospect.IntermediaryCode);

            var constructMessage = ConstructMessage(intermediary);

            return intermediary switch
            {
                _ when intermediary is null => ReturnError(HandlerError.NotFound(_requestCommand.MessageType, nameof(Intermediary), Prospect.IntermediaryCode, nameof(Prospect.IntermediaryCode))),
                _ => GetDocumentPackAsync(intermediary, Prospect.IsCareIntermediary).Result
                    .Match(success => BuildQuildQuicklySignRequest(success, constructMessage).Result, failed => failed)
                    .Match(success => GenerateWebHookSubscription().Result, failed => failed)
            };
        }


        private async Task<Result<bool, FailedResult>> BuildQuildQuicklySignRequest(IList<ProspectDocumentEsignDto> prospectDocuments, string messageBody)
        {

            var documentDefinitionList = new List<DocumentDefinition>();
            var esignDocumentStatusList = await GetESignDocumentStatusList();

            foreach (var documentType in prospectDocuments)
                documentDefinitionList.Add(documentType.Document.ToDocumentDefinitions(documentType.Value));

            var response = await _quicklySignApiManager.SendEsignRequestAsync(new DocumentPackRequest
            {
                Recipient = [Prospect.ToDocumentPackRecipients()],
                DocumentPackName = QuicklySignConstants.AdvicePartnerAuthorisationDocuments,
                DocumentType = QuicklySignConstants.DocumentType,
                Documents = documentDefinitionList,
                MailSettings = QuicklySignConstants.AdvicePartnerAuthorisationDocuments.ToDocumentPackMailSettings(messageBody),
                Signatories = [Prospect.ToSignatory(QuicklySignConstants.Signer1, QuicklySignConstants.PrimaryCommunicationChannel_Email)],
                SigningSettings = QuicklySignMapping.ToSigningSettings()
            }, _cancellationToken);

            if (response.Success)
            {
                var documentPackStatus = esignDocumentStatusList.FirstOrDefault(a => a.Key == response.Status);
                var esignDocPackEntity = new ESignDocumentPack
                {
                    DocumentPackKey = response.Key,
                    Name = QuicklySignConstants.AdvicePartnerAuthorisationDocuments,
                    SigningLink = response.SigningLink,
                    ESignDocumentStatusId = documentPackStatus.Id,
                    ProspectId = Prospect.Id,
                    ESignDocumentEntities = []
                };

                foreach (var document in response.Documents)
                {
                    EntityKeys.Add(document.Key);
                    documentPackStatus = esignDocumentStatusList.FirstOrDefault(a => a.Key == response.Status);

                    var entity = new ESignDocumentEntity
                    {
                        EntityKey = document.Key,
                        DocumentName = GetDocumentTypeKey(prospectDocuments, document.DocumentName),
                        ESignDocumentStatusId = documentPackStatus.Id,
                        ServePdfUrl = document.ServePdfUrl,
                        ESignDocumentPackId = esignDocPackEntity.Id,
                    };
                    esignDocPackEntity.ESignDocumentEntities.Add(entity);
                }

                await _uoW.CommandRepo<ESignDocumentPack>().AddAsync(esignDocPackEntity);
            }

            return response.Success;
        }

        private static string GetDocumentTypeKey(IList<ProspectDocumentEsignDto> prospectDocuments, string value)
           => prospectDocuments.FirstOrDefault(a => a.Value == value).Key;


        private async Task<Result<bool, FailedResult>> GenerateWebHookSubscription()
        {

            bool isSuccess = false;
            foreach (var currentKey in EntityKeys)
            {
                var webhookList = new WebhookRequest
                {
                    EntityKey = currentKey,
                    CallBack = $"{GeneratedLinkAdvicePartnerGatewayUrl}{ESignConstants.WebhookCallbackUrl}",
                };

                var response = await _quicklySignApiManager.WebhookSubscriptionAsync(webhookList, _cancellationToken);

                if (response.Success)
                {
                    //persist webhook subscription
                    var entity = new ESignWebhookSubscription
                    {
                        //ESignDocumentEntityId = response.WebhookResponse.Key,
                        ProspectId = Prospect.Id,
                        UpdatedDate = DateTimeOffset.FromFileTime(response.WebhookResponse.DateUpdated).DateTime
                    };

                   // await _uoW.CommandRepo<ESignWebhookSubscription>().AddAsync(entity);
                    isSuccess = response.Success;
                }
            }

            return isSuccess;
        }

        private async Task<Result<IList<ProspectDocumentEsignDto>, FailedResult>> GetDocumentPackAsync(Intermediary intermediary, bool? isCareIntermediary)
        {
            var disclosureDocument = await GetDisclosureDocument(intermediary);

            disclosureDocument = InjectEsignValues(disclosureDocument);

            var resultSet = new List<ProspectDocumentEsignDto>
            {new(DocumentType.DisclosureDocument, "Disclosure Document", Convert.FromBase64String(disclosureDocument)) };

            if (isCareIntermediary.HasValue && !isCareIntermediary.GetValueOrDefault())
            {
                var loadDocument = await GetLoaDocumentAsync(intermediary);
                resultSet.Add(new(DocumentType.LetterOfAuthorisation, "Letter of Authorisation", loadDocument.Value));
            }

            return resultSet;
        }

        private string InjectEsignValues(string disclosureDocument)
        {
            try
            {
                byte[] pdfBytes = Convert.FromBase64String(disclosureDocument);

                var disclosureEsignMappingList = GetDisclosureEsignMapping().OrderBy(a => a.Code).ToList();
                var startText = disclosureEsignMappingList.FirstOrDefault().Text;
                // Replace text in PDF bytes
                //var replacementOutputs = new Dictionary<string, XRect>();
                int pageNumber = 0;
                // Optional: Process with PdfSharpCore if additional manipulation needed
                MemoryStream msReport = new(pdfBytes);

                using (PdfDocument document = PdfDocument.Open(msReport))
                {
                     pageNumber = document.GetPages().Count();
                     var lastPage = document.GetPage(pageNumber);

                    if (lastPage.Text.Contains(startText))
                    {
                        var matchingArray = lastPage.GetWords().ToArray();
 
                        foreach(var record in  disclosureEsignMappingList)
                            if(matchingArray[record.Position] is not null)
                                BuildReplacementText(record, lastPage, matchingArray[record.Position]);
                    }
                }

                if (pageNumber > 0)
                    pdfBytes = ReplaceTextWithCoordinates(pdfBytes, pageNumber, disclosureEsignMappingList);

                return Convert.ToBase64String(pdfBytes);
            }
            catch (Exception ex)
            {
                // Log error and return original document
                Console.WriteLine($"Error injecting eSign values: {ex.Message}");
                return disclosureDocument;
            }
        }

        private static void BuildReplacementText(PDFReplacementDto replacement,Page page, UglyToad.PdfPig.Content.Word matchingWord)
        {
                var pigRect = matchingWord.BoundingBox;
            // Note: PDF coordinate origins differ between libraries. 
            // You may need to invert the Y-axis depending on page height: (page.Height - pigRect.Top)
            replacement.Coordinates = new XRect(pigRect.Left, page.Height - (pigRect.Top), pigRect.Width, pigRect.Height);

        }

        public static byte[] ReplaceTextWithCoordinates(byte[] pdfBytes, int targetPageNumber, List<PDFReplacementDto> replacements)
        {
            // 1. Use PdfPig to find the location metrics of the target text
            PdfSharp.Drawing.XRect targetRect = new XRect();


            MemoryStream msReport = new(pdfBytes);
            // 2. Open the file in PDFsharp to overlay graphics on top
            using (var pdfSharpDoc = PdfReader.Open(msReport, PdfDocumentOpenMode.Modify))
            {
                GlobalFontSettings.UseWindowsFontsUnderWindows = true;
                var page = pdfSharpDoc.Pages[targetPageNumber - 1];
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    foreach (var replacement in replacements)
                    {
                        // Strategy 1: Expand the white rectangle to fully cover the text area
                        var expandedRect = new XRect(
                            replacement.Coordinates.X - 2,  // Expand left
                            replacement.Coordinates.Y - 2,  // Expand top
                            replacement.Coordinates.Width + 4,  // Expand width
                            replacement.Coordinates.Height + 4  // Expand height
                        );

                        // Draw a solid white rectangle with border to erase the old text
                        gfx.DrawRectangle(
                       // White fill to cover text
                            XPens.White,     // White border to clean up edges
                            expandedRect
                        );

                        // Optional: Add a light gray border for debugging/visibility
                        // Uncomment the line below to see the exact area being covered
                        // gfx.DrawRectangle(XPens.LightGray, expandedRect);

                        // Write the new text over the exact same location
                        var font = new XFont("Arial", 10);
                        gfx.DrawString(replacement.ReplacementText, font, XBrushes.Black, 
                            replacement.Coordinates, XStringFormats.TopLeft);
                    }
                }

                // Save modified PdfDocument back to byte[]
                using (var output = new MemoryStream())
                {
                    pdfSharpDoc.Save(output, true);
                    pdfBytes = output.ToArray();
                }
            }

            return pdfBytes;
        }


        private List<PDFReplacementDto> GetDisclosureEsignMapping()
          => [.. _decisionObjectFactory.All<DisclosureEsignTransform>().Select(a => new PDFReplacementDto
            {
                Code = int.Parse(a.Code),
                Text = a.Text,
                OriginalText = a.OriginalText,
                ReplacementText = a.ReplacementText,
                Row = a.Row,
                Position = a.Position
            })];


        private async Task<Result<byte[], FailedResult>> GetLoaDocumentAsync(Intermediary intermediary)
        {

                var fullName =
                    $"{intermediary.FirstName} {intermediary.Surname} {intermediary.IntermediaryCode}";
                var auditEvents = new List<AuditEvent>
                {
                    AuditEventHelper.CreateEvent(intermediary.EmailAddress, fullName,
                        Prospect.MayBecomeCareIntermediary,
                        _valueObjectFactory.GetByCode<DocumentStatus>(DocumentStatus.DocumentCreated),
                        _requestCommand.IpAddress),
                    AuditEventHelper.CreateEvent(intermediary.EmailAddress, fullName,
                        Prospect.MayBecomeCareIntermediary,
                        _valueObjectFactory.GetByCode<DocumentStatus>(DocumentStatus.EmailSent), _requestCommand.IpAddress)
                };

                var reportCommand =
                    new ReportCommand(Prospect, intermediary, DocumentName.LetterOfAuthorisationPubFile)
                    {
                        GoalDetails = [],
                        AuditEvents = auditEvents
                    };

                var (data, message, xml) = await _documentService.GenerateDocumentAsync(reportCommand);

                var docToSave = AuditedDocumentHelper.CreateLOADocToSendToClient(Prospect, data, auditEvents);
                await _uoW.AuditedDocumentCommandRepository.AddAsync(docToSave);


            await _distributionAuditService.AuditLOADiscSent(intermediary.BluestarCode, intermediary.IntermediaryCode,
                Prospect.EmailAddress, Prospect.FirstName, _requestCommand.IpAddress);

            return data;
        }

        private async Task<string> GetDisclosureDocument(Intermediary intermediary)
        {

            var documentNameDisclosureDocumentFolder = "PERMIT";
            var idResult = await _onDemandDocumentService.SearchOnDemandAsync(intermediary.IntermediaryCode, "English");

            var result =
                await _onDemandDocumentService.RetrieveOnDemandDocument(idResult,
                    documentNameDisclosureDocumentFolder);

            await SaveDisclosureToAudit(new AuditDocumentCommand(intermediary.IntermediaryCode, intermediary.BluestarCode, DocumentName.DisclosureDocument,
                Prospect.EmailAddress, $"{Prospect.FirstName} {Prospect.Surname}", _requestCommand.IpAddress));

            return result;
        }

        private async Task SaveDisclosureToAudit(AuditDocumentCommand message)
        {
            var auditDetails = MandatoryAuditData($"{message.DocName} Created", "Document created",
                message.IntermediaryCode, _applicationVersion, message.IpAddress, message.BlueStarCode);
            auditDetails.Add("Email sent date and timestamp", DateTime.Now.ToString());
            auditDetails.Add("SenderEmail Address", message.EmailAddress);
            auditDetails.Add("SenderPreferred Name and Surname", message.PreferredNames);
            await _distributionAuditService.SaveAsync(auditDetails);
        }

        private static Dictionary<string, string> MandatoryAuditData(string eventName, string ApAuthorisationCapacity,
            string intermediaryCode, string applicationVersion, string userIp, string blueStarCode = "")
        {
            var auditDetails = new Dictionary<string, string>
            {
                { "Event", eventName },
                { "APAuthorisationCapacity", ApAuthorisationCapacity },
                { "AdvicePartnerVersionNumber", applicationVersion },
                { "APLoggedInUserID", Environment.MachineName },
                { "APLoggedInUserIP", userIp },
                { "IntermediaryCode", string.IsNullOrEmpty(intermediaryCode) ? "No Intermediary" : intermediaryCode }
            };
            if (!string.IsNullOrEmpty(blueStarCode)) auditDetails.Add("BlueStarCode", blueStarCode);
            return auditDetails;
        }

        private string ConstructMessage(Intermediary intermediary)
        {

            var html = File.ReadAllText(OnlineEsignApprovalTemplate);

            return html
                .Replace("{#tns:ClientNameAndSurname}", HttpUtility.HtmlEncode(Prospect.FirstName + " " + Prospect.Surname))
                .Replace("{#tns:IntermediaryName}", HttpUtility.HtmlEncode(intermediary.FirstName + " " + intermediary.Surname))
                .Replace("{#tns:IntermediaryNumber}", HttpUtility.HtmlEncode(Prospect.IntermediaryCode))
                .Replace("{#tns:IntermediaryEmail}", HttpUtility.HtmlEncode(intermediary.EmailAddress));
        }

        private static FailedResult ReturnError(Error error) => FailedResult.CreateBadRequest(error);

        private async Task<IEnumerable<ESignDocumentStatus>> GetESignDocumentStatusList()
        =>  await _uoW.QueryWithoutTracking<ESignDocumentStatus>().ToListAsync();

    }
}
