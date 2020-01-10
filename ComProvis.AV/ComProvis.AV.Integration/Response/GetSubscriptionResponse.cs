using ComProvis.AV.Integration.Model;
using System;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Response
{
    public class GetSubscriptionResponse
    {
        public Guid SubscriptionId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool IsAutoRenawal { get; set; }
        public int AutoRenawalMonth { get; set; }
        public int ExpirationNotification { get; set; }
        public string ServiceUrl { get; set; }
        public IList<License> Licenses { get; set; }
    }
}
