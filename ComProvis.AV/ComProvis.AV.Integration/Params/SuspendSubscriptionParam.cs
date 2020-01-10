using System;

namespace ComProvis.AV.Integration.Params
{
    public class SuspendSubscriptionParam
    {
        public Guid CustomerId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime? LicenseExpirationDate { get; set; }
    }
}
