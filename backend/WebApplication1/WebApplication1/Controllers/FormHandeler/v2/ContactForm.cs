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
            // Ider for spam filtrering:
            /*
             * whiteliste prefixes for forskjellige land
             * begrense antall ganger det samme tlf nummeret kan sende på nytt per tids enhet
             * bedre validering av tlf nr
             */

            var errors = new List<string>();
            var faultInPhone = FaultInPhone(data.PhoneNumber);

            if (faultInPhone != null)
            {
                errors.Add(faultInPhone);
                var res = new FormDataResponse();
                res.Errors = errors;
                res.Success = false;
            }

            var faultInEmail = FaultInEmail(data.Email);

            if (faultInEmail != null)
            {
                errors.Add(faultInEmail);
                var res = new FormDataResponse();
                res.Errors = errors;
                res.Success = false;
            }

            var firstName = CorrectNameFormats(data.FirstName);
            var midleName = CorrectNameFormats(data.MidleName);
            var lastName = CorrectNameFormats(data.LastName);

            var correctedData = new FormData()
            {
                FirstName = System.Net.WebUtility.HtmlEncode(firstName),
                MidleName = System.Net.WebUtility.HtmlEncode(midleName),
                LastName = System.Net.WebUtility.HtmlEncode(lastName),
                Applicant = System.Net.WebUtility.HtmlEncode(data.Applicant),
                Job = System.Net.WebUtility.HtmlEncode(data.Job),
                Date = data.Date,
                AboutTheJob = System.Net.WebUtility.HtmlEncode(data.AboutTheJob),
                Email = System.Net.WebUtility.HtmlEncode(data.Email),
                PhoneNumberPrefix = data.PhoneNumberPrefix,
                PhoneNumber = data.PhoneNumber,
            };

            SendNotification(data);

            return new FormDataResponse() { Success = true, Errors = new List<string>() };
        }

        private string CorrectNameFormats(string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            var correctedName = string.Empty;
            var names = name.Split(' ');

            for (int i = 0; i < names.Length; i++)
            {
                name = names[i].ToLower();

                correctedName += name[0].ToString().ToUpper();
                correctedName += name.Substring(1);

                if (names.Length != i + 1)
                {
                    correctedName += " ";
                }
            }

            return correctedName.Trim();
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
            var html = $@"<!DOCTYPE html>
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
        <tbody>";

            html += $@"
          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Applicant</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Applicant}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Fornavn</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.FirstName}</td>
          </tr>";

            if (data.MidleName != string.Empty)
                html += $@"
          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Mellomnavn</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.MidleName}</td>
          </tr>";

            html += $@"
          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Etternavn</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.LastName}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Email</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Email}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Land</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{GetCountryCode("+" + data.PhoneNumberPrefix.ToString())}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Telefon nr</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{"+" + data.PhoneNumberPrefix.ToString() + " " + data.PhoneNumber.ToString()}</td>
          </tr>

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Type jobb</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Job}</td>
          </tr>";

            if (data.AboutTheJob != string.Empty)
            {
                html += $@"

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Beskrivelse av jobben</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.AboutTheJob}</td>
          </tr>";
            }

            html += $@"

          <tr style=""border-bottom: 1px solid #dddddd;"">
            <td style=""padding: 12px 15px;max-width: 50px;"">Dato</td>
            <td style=""padding: 12px 15px;max-width: 50px;"">{data.Date.ToShortDateString()}</td>
          </tr>

";

            html += $@"
        </tbody>
      </table>
    </div>
  </body>
</html>";
            return html;
        }


        private string GetCountryCode(string PhoneNumberPrefix)
        {
            Console.WriteLine(PhoneNumberPrefix);
            var dictionary = new Dictionary<string, string>();

            dictionary.Add("AC", "+247");
            dictionary.Add("AD", "+376");
            dictionary.Add("AE", "+971");
            dictionary.Add("AF", "+93");
            dictionary.Add("AG", "+1-268");
            dictionary.Add("AI", "+1-264");
            dictionary.Add("AL", "+355");
            dictionary.Add("AM", "+374");
            dictionary.Add("AN", "+599");
            dictionary.Add("AO", "+244");
            dictionary.Add("AR", "+54");
            dictionary.Add("AS", "+1-684");
            dictionary.Add("AT", "+43");
            dictionary.Add("AU", "+61");
            dictionary.Add("AW", "+297");
            dictionary.Add("AX", "+358-18");
            dictionary.Add("AZ", "+994"); // or +374-97
            dictionary.Add("BA", "+387");
            dictionary.Add("BB", "+1-246");
            dictionary.Add("BD", "+880");
            dictionary.Add("BE", "+32");
            dictionary.Add("BF", "+226");
            dictionary.Add("BG", "+359");
            dictionary.Add("BH", "+973");
            dictionary.Add("BI", "+257");
            dictionary.Add("BJ", "+229");
            dictionary.Add("BM", "+1-441");
            dictionary.Add("BN", "+673");
            dictionary.Add("BO", "+591");
            dictionary.Add("BR", "+55");
            dictionary.Add("BS", "+1-242");
            dictionary.Add("BT", "+975");
            dictionary.Add("BW", "+267");
            dictionary.Add("BY", "+375");
            dictionary.Add("BZ", "+501");
            dictionary.Add("CA", "+1");
            dictionary.Add("CC", "+61");
            dictionary.Add("CD", "+243");
            dictionary.Add("CF", "+236");
            dictionary.Add("CG", "+242");
            dictionary.Add("CH", "+41");
            dictionary.Add("CI", "+225");
            dictionary.Add("CK", "+682");
            dictionary.Add("CL", "+56");
            dictionary.Add("CM", "+237");
            dictionary.Add("CN", "+86");
            dictionary.Add("CO", "+57");
            dictionary.Add("CR", "+506");
            dictionary.Add("CS", "+381");
            dictionary.Add("CU", "+53");
            dictionary.Add("CV", "+238");
            dictionary.Add("CX", "+61");
            dictionary.Add("CY", "+357"); // or +90-392
            dictionary.Add("CZ", "+420");
            dictionary.Add("DE", "+49");
            dictionary.Add("DJ", "+253");
            dictionary.Add("DK", "+45");
            dictionary.Add("DM", "+1-767");
            dictionary.Add("DO", "+1-809"); // and 1-829?
            dictionary.Add("DZ", "+213");
            dictionary.Add("EC", "+593");
            dictionary.Add("EE", "+372");
            dictionary.Add("EG", "+20");
            dictionary.Add("EH", "+212");
            dictionary.Add("ER", "+291");
            dictionary.Add("ES", "+34");
            dictionary.Add("ET", "+251");
            dictionary.Add("FI", "+358");
            dictionary.Add("FJ", "+679");
            dictionary.Add("FK", "+500");
            dictionary.Add("FM", "+691");
            dictionary.Add("FO", "+298");
            dictionary.Add("FR", "+33");
            dictionary.Add("GA", "+241");
            dictionary.Add("GB", "+44");
            dictionary.Add("GD", "+1-473");
            dictionary.Add("GE", "+995");
            dictionary.Add("GF", "+594");
            dictionary.Add("GG", "+44");
            dictionary.Add("GH", "+233");
            dictionary.Add("GI", "+350");
            dictionary.Add("GL", "+299");
            dictionary.Add("GM", "+220");
            dictionary.Add("GN", "+224");
            dictionary.Add("GP", "+590");
            dictionary.Add("GQ", "+240");
            dictionary.Add("GR", "+30");
            dictionary.Add("GT", "+502");
            dictionary.Add("GU", "+1-671");
            dictionary.Add("GW", "+245");
            dictionary.Add("GY", "+592");
            dictionary.Add("HK", "+852");
            dictionary.Add("HN", "+504");
            dictionary.Add("HR", "+385");
            dictionary.Add("HT", "+509");
            dictionary.Add("HU", "+36");
            dictionary.Add("ID", "+62");
            dictionary.Add("IE", "+353");
            dictionary.Add("IL", "+972");
            dictionary.Add("IM", "+44");
            dictionary.Add("IN", "+91");
            dictionary.Add("IO", "+246");
            dictionary.Add("IQ", "+964");
            dictionary.Add("IR", "+98");
            dictionary.Add("IS", "+354");
            dictionary.Add("IT", "+39");
            dictionary.Add("JE", "+44");
            dictionary.Add("JM", "+1-876");
            dictionary.Add("JO", "+962");
            dictionary.Add("JP", "+81");
            dictionary.Add("KE", "+254");
            dictionary.Add("KG", "+996");
            dictionary.Add("KH", "+855");
            dictionary.Add("KI", "+686");
            dictionary.Add("KM", "+269");
            dictionary.Add("KN", "+1-869");
            dictionary.Add("KP", "+850");
            dictionary.Add("KR", "+82");
            dictionary.Add("KW", "+965");
            dictionary.Add("KY", "+1-345");
            dictionary.Add("KZ", "+7");
            dictionary.Add("LA", "+856");
            dictionary.Add("LB", "+961");
            dictionary.Add("LC", "+1-758");
            dictionary.Add("LI", "+423");
            dictionary.Add("LK", "+94");
            dictionary.Add("LR", "+231");
            dictionary.Add("LS", "+266");
            dictionary.Add("LT", "+370");
            dictionary.Add("LU", "+352");
            dictionary.Add("LV", "+371");
            dictionary.Add("LY", "+218");
            dictionary.Add("MA", "+212");
            dictionary.Add("MC", "+377");
            dictionary.Add("MD", "+373"); // or +373-533
            dictionary.Add("ME", "+382");
            dictionary.Add("MG", "+261");
            dictionary.Add("MH", "+692");
            dictionary.Add("MK", "+389");
            dictionary.Add("ML", "+223");
            dictionary.Add("MM", "+95");
            dictionary.Add("MN", "+976");
            dictionary.Add("MO", "+853");
            dictionary.Add("MP", "+1-670");
            dictionary.Add("MQ", "+596");
            dictionary.Add("MR", "+222");
            dictionary.Add("MS", "+1-664");
            dictionary.Add("MT", "+356");
            dictionary.Add("MU", "+230");
            dictionary.Add("MV", "+960");
            dictionary.Add("MW", "+265");
            dictionary.Add("MX", "+52");
            dictionary.Add("MY", "+60");
            dictionary.Add("MZ", "+258");
            dictionary.Add("NA", "+264");
            dictionary.Add("NC", "+687");
            dictionary.Add("NE", "+227");
            dictionary.Add("NF", "+672");
            dictionary.Add("NG", "+234");
            dictionary.Add("NI", "+505");
            dictionary.Add("NL", "+31");
            dictionary.Add("NO", "+47");
            dictionary.Add("NP", "+977");
            dictionary.Add("NR", "+674");
            dictionary.Add("NU", "+683");
            dictionary.Add("NZ", "+64");
            dictionary.Add("OM", "+968");
            dictionary.Add("PA", "+507");
            dictionary.Add("PE", "+51");
            dictionary.Add("PF", "+689");
            dictionary.Add("PG", "+675");
            dictionary.Add("PH", "+63");
            dictionary.Add("PK", "+92");
            dictionary.Add("PL", "+48");
            dictionary.Add("PM", "+508");
            dictionary.Add("PR", "+1-787"); // and 1-939 ?
            dictionary.Add("PS", "+970");
            dictionary.Add("PT", "+351");
            dictionary.Add("PW", "+680");
            dictionary.Add("PY", "+595");
            dictionary.Add("QA", "+974");
            dictionary.Add("RE", "+262");
            dictionary.Add("RO", "+40");
            dictionary.Add("RS", "+381");
            dictionary.Add("RU", "+7");
            dictionary.Add("RW", "+250");
            dictionary.Add("SA", "+966");
            dictionary.Add("SB", "+677");
            dictionary.Add("SC", "+248");
            dictionary.Add("SD", "+249");
            dictionary.Add("SE", "+46");
            dictionary.Add("SG", "+65");
            dictionary.Add("SH", "+290");
            dictionary.Add("SI", "+386");
            dictionary.Add("SJ", "+47");
            dictionary.Add("SK", "+421");
            dictionary.Add("SL", "+232");
            dictionary.Add("SM", "+378");
            dictionary.Add("SN", "+221");
            dictionary.Add("SO", "+252");
            dictionary.Add("SR", "+597");
            dictionary.Add("ST", "+239");
            dictionary.Add("SV", "+503");
            dictionary.Add("SY", "+963");
            dictionary.Add("SZ", "+268");
            dictionary.Add("TA", "+290");
            dictionary.Add("TC", "+1-649");
            dictionary.Add("TD", "+235");
            dictionary.Add("TG", "+228");
            dictionary.Add("TH", "+66");
            dictionary.Add("TJ", "+992");
            dictionary.Add("TK", "+690");
            dictionary.Add("TL", "+670");
            dictionary.Add("TM", "+993");
            dictionary.Add("TN", "+216");
            dictionary.Add("TO", "+676");
            dictionary.Add("TR", "+90");
            dictionary.Add("TT", "+1-868");
            dictionary.Add("TV", "+688");
            dictionary.Add("TW", "+886");
            dictionary.Add("TZ", "+255");
            dictionary.Add("UA", "+380");
            dictionary.Add("UG", "+256");
            dictionary.Add("US", "+1");
            dictionary.Add("UY", "+598");
            dictionary.Add("UZ", "+998");
            dictionary.Add("VA", "+379");
            dictionary.Add("VC", "+1-784");
            dictionary.Add("VE", "+58");
            dictionary.Add("VG", "+1-284");
            dictionary.Add("VI", "+1-340");
            dictionary.Add("VN", "+84");
            dictionary.Add("VU", "+678");
            dictionary.Add("WF", "+681");
            dictionary.Add("WS", "+685");
            dictionary.Add("YE", "+967");
            dictionary.Add("YT", "+262");
            dictionary.Add("ZA", "+27");
            dictionary.Add("ZM", "+260");
            dictionary.Add("ZW", "+263");

            foreach (var entry in dictionary)
            {
                if (entry.Value == PhoneNumberPrefix)
                {
                    return entry.Key;
                }
            }

            return null;
        }
    }
}