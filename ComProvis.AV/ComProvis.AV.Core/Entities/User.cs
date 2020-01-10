using ComProvis.AV.Core;
using System;
using System.Collections.Generic;

namespace ComProvis.AV
{
    public partial class User : IPrimaryKey
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int? CompanyId { get; set; }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public bool? IsSuspended { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public string ApplicationLoginName { get; set; }
        public string ApplicationPassword { get; set; }
        public string ApplicationID { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
