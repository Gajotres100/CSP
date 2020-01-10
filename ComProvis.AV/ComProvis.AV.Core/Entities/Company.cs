using ComProvis.AV.Core;
using System;
using System.Collections.Generic;

namespace ComProvis.AV
{
    public partial class Company : IPrimaryKey
    {
        public Company()
        {
            ApplicationSubscription = new HashSet<ApplicationSubscription>();
        }

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public string ApplicationCustomerID { get; set; }
        public string ApplicationSubscriptionId { get; set; }
        public ICollection<ApplicationSubscription> ApplicationSubscription { get; set; }
    }
}
