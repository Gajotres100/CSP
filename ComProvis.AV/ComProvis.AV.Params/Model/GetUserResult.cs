using ComProvis.AV;

namespace SaaSApi
{
    public class GetUserResult
    {
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }

        public static explicit operator GetUserResult(User user) => new GetUserResult
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            ContactInfo = user.ContactInfo,
            Email = user.Email,
            UserId = user.ExternalId
        };
    }
}
