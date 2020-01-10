using ComProvis.AV;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class AddUserParams : BaseParam
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string ContactInfo { get; set; }

        public User GetUser() => new User
        {
            Address = Address,
            Email = Email,
            ContactInfo = ContactInfo,
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            ExternalId = UserId
        };
    }
}
