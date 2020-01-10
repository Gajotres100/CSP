using System;

namespace ComProvis.AV.Integration.Model
{
    public class ServicePlan
    {
        public Guid ServicePlanId { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public int InitialLicensePeriodMonth { get; set; }
        public string DataCenter { get; set; }
        public string DataCenterCode { get; set; }
        public bool AutoRenewal { get; set; }
    }
}
