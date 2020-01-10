using System.Collections.Generic;

namespace ComProvis.AV.Integration.Results
{
    internal class GetCustomerDownloadLinkResult
    {
        public List<Customer> customers { get; set; }

        internal class Customer
        {
            public string magic_link { get; set; }
            public string server_location { get; set; }
            public string service_name { get; set; }
            public string id { get; set; }
            public int agent_mode { get; set; }
            public string colo { get; set; }
            public string company_key { get; set; }
            public int service_id { get; set; }
            public string activation_code { get; set; }
            public int domain_id { get; set; }
        }
    }
}
