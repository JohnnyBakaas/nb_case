namespace WebApplication1.Contracts.ContactForm.v2
{
    public interface IFormData
    {
        string Applicant { get; set; }
        string Job { get; set; }
        DateTime Date { get; set; }
        string AboutTheJob { get; set; }
        string FirstName { get; set; }
        string MidleName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        int PhoneNumberPrefix { get; set; }
        int PhoneNumber { get; set; }
    }
}

/*
{
    "job": "bilmekaniker",
    "date": "2023-08-25T17:26:16.275Z",
    "aboutTheJob": "asdf",
    "firstName": "Johnny",
    "midleName": "",
    "lastName": "Bakaas",
    "email": "johnny@bakaas-cadconsult.no",
    "phoneNumber": [
        47,
        90732947
    ]
}
 */
