namespace Api.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Azure;
using Azure.Communication.Email;

using Domain;
using Domain.DTO;
using Microsoft.Extensions.Options;

public class EmailService: IEmailService
    {

   private readonly IdentityManagerSettings _settings;

    public EmailService(IOptions<IdentityManagerSettings> settings)//IdentityManagerSettings settings
    {
       _settings = settings.Value;
    }
    // logged in users 
    public  async Task<bool> AfterInquiry(InquiryDTO inquiryDTO, string name, string email)
    {
        string connectionString = "endpoint=https://emailsending420.unitedstates.communication.azure.com/;accesskey=ixhKzJrHEMFPyNhTNHzLoIecXjRzoVP900lp1eLQYBzzJssmj42iMneW3mow4cNY1glWBcNZNgzNRWur3nleCA==";
        var emailClient = new EmailClient(connectionString);

      

        string subject = string.Format("New inquiry");
        string body = string.Format($"Hej! Przeslanie inquiry powiodlo się! Dziękujemy za zaufanie naszej firmie {name} ~ Stachnet");
        EmailContent emailContent = new EmailContent(subject);
        emailContent.PlainText = body;
        string toEmailAddress = $"{email}";
        string fromEmailAddress = "DoNotReply@f658ca5b-2738-4ba2-8742-10bfa77468a9.azurecomm.net";
        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(toEmailAddress) };
        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage(fromEmailAddress, emailRecipients, emailContent);
        EmailSendOperation response = default;
        try
        {
            response = await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
       catch(Exception ex)
        {
            string a = ex.Message;
        }






        return response.Value.Status == EmailSendStatus.Succeeded;
    }


    public async Task<bool> DeliveryCreate(Guid delivery,string name, string email)
    {
        string connectionString = "endpoint=https://emailsending420.unitedstates.communication.azure.com/;accesskey=ixhKzJrHEMFPyNhTNHzLoIecXjRzoVP900lp1eLQYBzzJssmj42iMneW3mow4cNY1glWBcNZNgzNRWur3nleCA==";
        var emailClient = new EmailClient(connectionString);



        string subject = string.Format("New Delivery");
        string body = string.Format($"Hej! Twoje zamowienie zostaly przyjete do realizacji. O to jej id: {delivery}. Dziękujemy za zaufanie naszej firmie {name} ~ Stachnet");
        EmailContent emailContent = new EmailContent(subject);
        emailContent.PlainText = body;
        string toEmailAddress = $"{email}";
        string fromEmailAddress = "DoNotReply@f658ca5b-2738-4ba2-8742-10bfa77468a9.azurecomm.net";
        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(toEmailAddress) };
        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage(fromEmailAddress, emailRecipients, emailContent);
        EmailSendOperation response = default;
        try
        {
            response = await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
        catch (Exception ex)
        {
            string a = ex.Message;
        }






        return response.Value.Status == EmailSendStatus.Succeeded;
    }


}

