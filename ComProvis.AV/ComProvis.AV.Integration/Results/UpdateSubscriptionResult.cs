using System;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Results
{
    internal class UpdateSubscriptionResult
    {
        public Guid subscription_id { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public bool is_auto_renewal { get; set; }
        public int auto_renawal_month { get; set; }
        public int expiration_notification { get; set; }
        public string service_url { get; set; }
        public List<License> licenses { get; set; }

        internal class License
        {
            public string ac_code { get; set; }
            public string product_id { get; set; }
            public DateTime license_start_date { get; set; }
            public DateTime license_expiration_date { get; set; }
            public DateTime start_charge_date { get; set; }
            public int grace_period { get; set; }
            public int units { get; set; }
            public bool enabled { get; set; }
        }
    }
}
