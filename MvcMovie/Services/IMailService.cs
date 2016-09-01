namespace MvcMovie.Services
{
    public interface IMailService
    {
        void sendEmail(int MailId, int orderId, string userName, string registerEmail);
    }
}