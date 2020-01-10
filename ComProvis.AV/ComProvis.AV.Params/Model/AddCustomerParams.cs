using ComProvis.AV;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class AddCustomerParams : BaseParam
    {
        [DataMember]
        public string CompanyId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        public Company GetCompany() => new Company
        {
            Name = CompanyName,
            Address = Address,
            ContactEmail = Email,
            ContactFirstName = FirstName,
            ContactLastName = LastName,
            ExternalId = CompanyId
        };
    }
}
