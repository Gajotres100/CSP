using ComProvis.AV.Core;
using System;

namespace ComProvis.AV
{
    public partial class OrderDemand : IPrimaryKey
    {
        public int Id { get; set; }
        public string ExternalCompanyId { get; set; }
        public int OrderDemandStateId { get; set; }
        public int ProvisioningTypeId { get; set; }
        public string JsonData { get; set; }
        public DateTime CreateDate { get; set; }
        public string ExternalId { get; set; }
        public string Error { get; set; }
    }
}
