namespace Truck_company.Services;

public interface IEmailSender
{
    Task SendAsync(
        string subject,
        string htmlBody,
        string? replyTo = null,
        CancellationToken cancellationToken = default);
}
