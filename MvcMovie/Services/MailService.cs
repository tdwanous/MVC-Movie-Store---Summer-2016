using MvcMovie.Models.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MvcMovie.Services
{
    public class MailService : IMailService
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();

        public string body;
        public string customerName;
        
        public void sendEmail(int MailId, int orderId = 0, string userName = "", string registerEmail = "")
        {
            
            var fromAddress = new MailAddress("minnesotamoviestore@gmail.com", "Minnesota Movie Store");
            var toAddress = new MailAddress("test@email.com", "Test");
            if(MailId == 1) {
                toAddress = new MailAddress(_dbContext.Orders.Find(orderId).Email, customerName);
                customerName = _dbContext.Orders.Find(orderId).FirstName + " " + _dbContext.Orders.Find(orderId).LastName;
            }
            else if (MailId == 2)
            {
                toAddress = new MailAddress(registerEmail, userName);
            }
            const string fromPassword = "groupthree";
            const string subject = "Order Confirmation";
            if (MailId == 1)
            {
                body =
                "<p> Dear " + customerName + ", </p>" +
                "<p> Thank you for placing an order with the Minnesota Movie Store.Your order is number " + orderId + " and is currently being processed and will be sent to you within the next <strong> 2 </strong> business days from the order being placed.For a reminder, the total due within 30 days of receiving your movies is " + _dbContext.Orders.Find(orderId).Total + " which can be made payable online or through the mail.</p>" +

                "<p> If you would like to check the status of your order, you can check out your orders on our website under the Orders heading on the top of the site.This will show you whether your order is still being processed, has shipped, or arrived according to our records.</p>" +

                "<p> Thank you for doing business with us!</p>" +

                "<p> Minnesota Movie Store Team </p>";
            }
            else if (MailId == 2)
            {
                body = "<p> Congratulation you are now a member of Minnesota Movie Store. Feel freel to buy or rent any movie of your choice. Your username is " + userName + ".</p>";
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
