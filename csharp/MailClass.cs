/// ###################################
/// # OKIT pvt ltd
/// # ---------------------------------
/// # Class For Mail sending using SMTP;
/// # --------------------------------- 
/// # =================================
/// # Historique:
/// # ---------------------------------
/// # Created By: Ginu on Date: 13-June-2012
/// # Modified By:Vijay chandar on 21-Aug-2012; Added Method Textmail, Created some variables, Added Try catch
/// ###################################
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// </summary>

public class MailClass
{
    static string smtpClientAddress = "smtp.gmail.com";
    static string userId = "ginus@okit.in";
    static string password = "ginu2020";

    public static void ExtoMail(string from, string to, string subject, string message)
    {

        try
        {
            MailMessage mail = new MailMessage();
            //mail from address
            mail.From = new MailAddress(from);
            //mail to address
            mail.To.Add(to);
            //mail subject
            mail.Subject = subject;
            //mail message
            mail.Body = message;
            mail.IsBodyHtml = true;
            //setting SMTP Client
            SmtpClient smtp = new SmtpClient(smtpClientAddress);
            smtp.UseDefaultCredentials = false;
            //Adding mailid and passwod
            smtp.Credentials = new System.Net.NetworkCredential(userId, password);

            //to send mail
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            EXClass.Log(ex);
        }
           
    }
    public static void TextMail(string from, string to, string subject, string message)
    {
        try
        {
            MailMessage mail = new MailMessage();

            //mail from address
            mail.From = new MailAddress(from);
            //mail to address
            mail.To.Add(to);
            //mail subject
            mail.Subject = subject;
            //mail message
            mail.Body = message;
            mail.IsBodyHtml = false;
            //setting SMTP Client
            SmtpClient smtp = new SmtpClient(smtpClientAddress);
            smtp.UseDefaultCredentials = false;
            //Adding mailid and passwod
            smtp.Credentials = new System.Net.NetworkCredential(userId, password);

            //to send mail
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            EXClass.Log(ex);
        }

    }
}