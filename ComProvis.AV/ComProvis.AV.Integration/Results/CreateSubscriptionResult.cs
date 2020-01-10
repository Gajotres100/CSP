using System;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Results
{
    internal class CreateSubscriptionResult
    {
        public Guid subscription_id { get; set; }
        public string product_name { get; set; }
        public string service_url { get; set; }
        public List<License> licenses { get; set; }

        internal class License
        {
            public string ac_code { get; set; }
            public string product_id { get; set; }
            public string version { get; set; }
            public DateTime license_start_date { get; set; }
            public DateTime license_expiration_date { get; set; }
            public DateTime start_charge_date { get; set; }
            public int units { get; set; }
        }
    }
}
