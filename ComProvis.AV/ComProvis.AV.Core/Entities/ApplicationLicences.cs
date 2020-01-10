using ComProvis.AV.Core;
using System;

namespace ComProvis.AV
{
    public partial class ApplicationLicences : IPrimaryKey
    {
        public int Id { get; set; }
        public string LicenceKey { get; set; }
        public int SubscriptionId { get; set; }
        public string Version { get; set; }
        public int? GracePeriod { get; set; }
        public int? Units { get; set; }
        public DateTime? LicenceStartDate { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }
        public DateTime? StartChargeDate { get; set; }

        public ApplicationSubscription Subscription { get; set; }
        public string ExternalId { get; set; }
    }
}
