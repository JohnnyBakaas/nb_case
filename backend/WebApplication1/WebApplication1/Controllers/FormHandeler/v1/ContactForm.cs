using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WebApplication1.Contracts.ContactForm.v1;
using WebApplication1.Controllers.FormHandeler.v1.Model;

namespace WebApplication1.Controllers.FormHandeler.v1
{
    public class ContactForm : IContactForm
    {


        public IFormDataResponse SubmitForm(IFormData data)
        {
            string faultsInName = FaultInName(data.Name);
            string faultInEmail = FaultInEmail(data.Email);
            string faultInPhone = FaultInPhone(data.Phone);
            string faultInApplicant = FaultInApplicant(data.Applicant);

            List<string> errorMessages = new List<string>();

            if (faultInApplicant != string.Empty) { errorMessages.Add(faultInApplicant); }
            if (faultsInName != string.Empty) { errorMessages.Add(faultsInName); }

            if (faultInEmail != string.Empty) { errorMessages.Add(faultInEmail); }

            if (faultInPhone != string.Empty) { errorMessages.Add(faultInPhone); }

            if (errorMessages.Count == 0)
            {
                var sendNotification = SendNotification(data);
                if (sendNotification != string.Empty) errorMessages.Add(sendNotification);
            }

            return new FormDataResponse()
            {
                Success = errorMessages.Count == 0 ? true : false,
                Errors = errorMessages,
            };


        }

        private string FaultInName(string name)
        {
            /*
            Hvorfor ikke Regex? 1 jeg brukte det i frontend,
            så jeg vil demonstrere en annen måte å gjøre det på.
            Jeg tror også det er raskere å gjøre det på denne 
            måten(Ikke sikker) noe som er rellevant siden vi 
            betaler for server bruk og ikkeclient bruk. Også er 
            det gøy med preformance
            */

            if (name.Length == 0)
            {
                return "name length is 0";
            }

            if (name.Length <= 5)
            {
                return "name length is less than 5";
            }

            string faultMessage = string.Empty;

            if (!char.IsUpper(name[0]))
            {
                faultMessage += $"name:{name} is missing a capital " +
                    $"letter on [0] of the string. ";
            }

            bool lastWasSpace = false;
            bool thereIsNoWhiteSpace = true;

            for (int i = 1; i < name.Length; i++)
            {

                if (lastWasSpace)
                {
                    if (!char.IsUpper(name[i]))
                    {
                        faultMessage += $"name:{name} is missing a " +
                            $"capital letter on [{i}] of the string. ";
                    }
                    lastWasSpace = false;
                }

                if (name[i] == ' ')
                {
                    lastWasSpace = true;
                    thereIsNoWhiteSpace = false;
                }
            }

            if (thereIsNoWhiteSpace)
            {
                faultMessage += $"There is only one name in this string: {name}";
            }

            return faultMessage;
        }

        private string FaultInEmail(string email)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Regex emailRegex = new Regex(emailPattern);

            if (emailRegex.Match(email).Success) return string.Empty;
            return $"Invalid email: {email}";
        }

        private string FaultInPhone(string number)
        {
            string emailPattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
            Regex emailRegex = new Regex(emailPattern);

            if (emailRegex.Match(number).Success) return string.Empty;
            return $"Invalid number: {number}";
        }

        private string FaultInApplicant(string name)
        {
            // Ikke DRY, I know, med dgb
            if (name.Length == 0)
            {
                return "applicant length is 0";
            }

            if (name.Length <= 5)
            {
                return "applicant length is less than 5";
            }

            string faultMessage = string.Empty;

            if (!char.IsUpper(name[0]))
            {
                faultMessage += $"applicant:{name} is missing a capital " +
                    $"letter on [0] of the string. ";
            }

            bool lastWasSpace = false;
            bool thereIsNoWhiteSpace = true;

            for (int i = 1; i < name.Length; i++)
            {

                if (lastWasSpace)
                {
                    if (!char.IsUpper(name[i]))
                    {
                        faultMessage += $"applicant:{name} is missing a " +
                            $"capital letter on [{i}] of the string. ";
                    }
                    lastWasSpace = false;
                }

                if (name[i] == ' ')
                {
                    lastWasSpace = true;
                    thereIsNoWhiteSpace = false;
                }
            }

            if (thereIsNoWhiteSpace)
            {
                faultMessage += $"There is only one applicant in this string: {name}";
            }

            return faultMessage;
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

                //smtpClient.Send(, , "subject", "body");

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
            catch (Exception ex)
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
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Name}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Phone</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Phone}</td>
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
