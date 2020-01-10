using ComProvis.AV.Core;
using System.Collections.Generic;

namespace ComProvis.AV
{
    public partial class Roles : IPrimaryKey
    {
        public Roles()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ExternalId { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
