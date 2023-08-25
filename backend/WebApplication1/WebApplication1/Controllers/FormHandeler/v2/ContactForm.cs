using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WebApplication1.Contracts.ContactForm.v2;
using WebApplication1.Controllers.FormHandeler.v2.Model;

namespace WebApplication1.Controllers.FormHandeler.v2
{
    public class ContactForm : IContactForm
    {
        public IFormDataResponse SubmitForm(IFormData data)
        {
            var firstName = CorrectNameFormats(data.FirstName);
            var midleName = CorrectNameFormats(data.MidleName);
            var lastName = CorrectNameFormats(data.LastName);

            var faultInEmail = FaultInEmail(data.Email);
            var faultInPhone = FaultInPhone(data.PhoneNumber);

            SendNotification(data);

            return new FormDataResponse();
        }

        private string CorrectNameFormats(string name)
        {
            if (name.Length == 0) return string.Empty;
            var correctedName = string.Empty;
            var names = name.Split(' ');

            for (int i = 0; i < names.Length; i++)
            {
                name = names[i];
                name.ToLower();

                correctedName += name[0].ToString().ToUpper();
                correctedName += name.Substring(1);

                if (names.Length != i + 1)
                {
                    correctedName += " ";
                }
            }

            return correctedName;
        }

        private string FaultInEmail(string email)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Regex emailRegex = new Regex(emailPattern);

            if (emailRegex.Match(email).Success) return string.Empty;
            return $"Invalid email: {email}";
        }

        private string FaultInPhone(int number)
        {
            if (number.ToString().Length == 8) return string.Empty;
            return $"Invalid number: {number}";
        }

        private string SendNotification(IFormData data)
        {
            try
            {
                var userName = "post.test.noe@gmail.com"; //Nei, jeg hadde ikke gjort dette med NOE annet enn en brenner bruker
                var password = "fddcfmqxnhsqjzrx"; //Nei, jeg hadde ikke gjort dette med NOE annet enn en brenner bruker
                var recipient = "io@nettbureau.no";
                recipient = "johnny.bakaas@gmail.com";
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true,
                };


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(userName),
                    Subject = recipient,
                    Body = MakeEmailHTML(data),
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(recipient);

                smtpClient.Send(mailMessage);
            }
            catch
            {
                return "Backend faild, please try again later";
            }

            return string.Empty;
        }

        private string MakeEmailHTML(IFormData data)
        {
            return $@"
<!DOCTYPE html>
<html lang=""no"">
  <head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
  </head>
  

  <body>
    <div style=""font-family: sans-serif;"">
      <h1>Svar på skjema</h1>
      <table class=""styled-table"" style=""font-size: 0.9em;border-collapse: collapse;margin: 25px 0;width: 400px;box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);"">
        <thead>
          <tr style=""background-color: #009879;color: #ffffff;text-align: left;"">
            <th style=""padding: 12px 15px;"">Felt</th>
            <th style=""padding: 12px 15px;"">Svar</th>
          </tr>
        </thead>
        <tbody>
          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Applicant</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Applicant}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Email</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Email}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Name</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.FirstName}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Phone</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.PhoneNumber}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </body>
</html>
";
        }
    }
}
