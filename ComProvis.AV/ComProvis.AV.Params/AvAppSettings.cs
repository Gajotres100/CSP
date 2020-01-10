

namespace ComProvis.AV.Params
{
    public class AvAppSettings : IAvAppSettings
    {
        public string ApiAccessToken { get; set; }
        public string ApiSecretKey { get; set; }
        public string ServicePlanId { get; set; }
        public string ServiceLink { get; set; }
    }
}
