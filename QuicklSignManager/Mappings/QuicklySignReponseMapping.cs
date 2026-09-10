using QuicklSignManager.Dto.Response;
using System.Linq;

namespace QuicklSignManager.Mappings;

public static class QuicklySignReponseMapping
{
    public static QuicklySignResponse ToQuicklySignReponse(this WebhookSubscriptionResponse response)
    => response.Status.StatusCode != 200 || response.Data == null || response.Data.Webhook == null
            ? new QuicklySignResponse()
            :new QuicklySignResponse
                    {
                        Success = true,
                        WebhookResponse = response.Data.Webhook
                    };

    public static QuicklySignResponse ToQuicklySignReponse(this DocumentPackResponse response)
=>  response.Data == null || response.Data.DocumentPack == null || response.Data.DocumentPack.Documents == null || response.Data.DocumentPack.Documents.Count() ==0
        ? new QuicklySignResponse()
        : new QuicklySignResponse
        {
            Success = true,
            SigningLink = response.Data.SigningLink,
            Key = response.Data.DocumentPack.Key,
            Status = response.Data.DocumentPack.Status,
            StatusDetail = response.Data.DocumentPack.StatusDetail,
            Documents = response.Data.DocumentPack.Documents
        };
}
