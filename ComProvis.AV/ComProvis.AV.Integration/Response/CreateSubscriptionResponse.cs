using ComProvis.AV.Integration.Model;
using System;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Response
{
    public class CreateSubscriptionResponse
    {
        public Guid SubscriptionId { get; set; }
        public string ProductName { get; set; }
        public string ServiceUrl { get; set; }
        public IList<License> Licenses { get; set; }
    }
}
