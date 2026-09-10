using QuicklSignManager.Dto.Request;
using QuicklSignManager.Dto.Response;

namespace QuicklSignManager.Manager;

public interface IQuicklySignApiManager
{
    Task<QuicklySignResponse> SendEsignRequestAsync(DocumentPackRequest requestBody, CancellationToken cancellationToken = default);

    Task<QuicklySignResponse> WebhookSubscriptionAsync(WebhookRequest requestBody, CancellationToken cancellationToken = default);
}
