using System;

namespace ComProvis.AV.Integration.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Status { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExtension { get; set; }
    }
}
