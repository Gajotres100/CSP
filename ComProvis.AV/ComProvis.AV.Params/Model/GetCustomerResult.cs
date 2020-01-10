using ComProvis.AV;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/SaaSApi")]
    public class GetCustomerResult
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string CompanyId { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }

        public static explicit operator GetCustomerResult(Company company) => new GetCustomerResult
        {
            CompanyName = company.Name,
            CompanyId = company.ExternalId,
            FirstName = company.ContactFirstName,
            LastName = company.ContactLastName,
            Address = company.Address,
            Email = company.ContactEmail
        };
    }
}
