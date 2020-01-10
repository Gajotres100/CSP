using System;

namespace ComProvis.AV.UI.Models
{
    public class AuthenticatedUserInfoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
        public string LanguageCode { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Issued { get; set; }
        public string Settings { get; set; }
    }
}
