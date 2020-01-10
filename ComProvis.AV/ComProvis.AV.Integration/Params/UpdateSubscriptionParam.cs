using System;

namespace ComProvis.AV.Integration.Params
{
    public class UpdateSubscriptionParam
    {
        public Guid CustomerId { get; set; }
        public Guid SubscriptionId { get; set; }
        public int? UnitsPerLicense { get; set; }
        public DateTime? LicenseExpirationDate { get; set; }
    }
}
