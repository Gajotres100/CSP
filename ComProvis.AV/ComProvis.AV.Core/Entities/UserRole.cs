using ComProvis.AV.Core;

namespace ComProvis.AV
{
    public partial class UserRole : IPrimaryKey
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int Quantity { get; set; }

        public Roles Role { get; set; }
        public User User { get; set; }
        public string ExternalId { get; set; }
    }
}
