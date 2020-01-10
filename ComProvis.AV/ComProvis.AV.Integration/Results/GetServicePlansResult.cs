using System;

namespace ComProvis.AV.Integration.Results
{
    internal class GetServicePlansResult
    {
        public Guid service_plan_id { get; set; }
        public string name { get; set; }
        public string product { get; set; }
        public string version { get; set; }
        public int initial_license_period_month { get; set; }
        public string data_center { get; set; }
        public string data_center_code { get; set; }
        public bool auto_renewal { get; set; }
    }
}
