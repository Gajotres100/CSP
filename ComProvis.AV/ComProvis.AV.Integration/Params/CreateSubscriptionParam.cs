using System;

namespace ComProvis.AV.Integration.Params
{
    public class CreateSubscriptionParam
    {
        public Guid CustomerId { get; set; }
        public Guid ServicePlanId { get; set; }
        public int UnitsPerLicense { get; set; }
        public DateTime? LicenseStartDate { get; set; }
    }
}
