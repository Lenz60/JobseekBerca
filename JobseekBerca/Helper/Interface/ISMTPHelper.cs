namespace JobseekBerca.Helper.Interface
{
    public interface ISMTPHelper
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}
