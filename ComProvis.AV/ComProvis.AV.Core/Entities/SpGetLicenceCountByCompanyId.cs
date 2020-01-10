using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComProvis.AV.Core.Entities
{
    public class SpGetLicenceCountByCompanyId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Quantity { get; set; }
    }
}
