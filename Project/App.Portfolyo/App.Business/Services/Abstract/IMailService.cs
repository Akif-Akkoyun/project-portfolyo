using Ardalis.Result;
using PortfolyoApp.Business.DTOs.Mail;

namespace PortfolyoApp.Business.Services.Abstract
{
    public interface IMailService
    {
        Task<Result> SendMailAsync(MailSendDTO mailSendDto);
    }
}
