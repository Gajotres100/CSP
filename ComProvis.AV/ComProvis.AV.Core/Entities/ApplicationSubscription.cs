using ComProvis.AV.Core;
using System.Collections.Generic;

namespace ComProvis.AV
{
    public partial class ApplicationSubscription : IPrimaryKey
    {
        public ApplicationSubscription()
        {
            ApplicationLicences = new HashSet<ApplicationLicences>();
        }

        public int Id { get; set; }
        public string ServiceUrl { get; set; }
        public string SubscriptionId { get; set; }
        public string ProductName { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }
        public ICollection<ApplicationLicences> ApplicationLicences { get; set; }
        public string ExternalId { get; set; }
    }
}
