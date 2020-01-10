using System;

namespace ComProvis.AV.Integration.Model
{
    public class License
    {
        public string AcCode { get; set; }
        public string ProductId { get; set; }
        public string Version { get; set; }
        public DateTime LicenseStartDate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime StartChargeDate { get; set; }
        public int GracePeriod { get; set; }
        public int Units { get; set; }
        public bool Enabled { get; set; }
    }
}
