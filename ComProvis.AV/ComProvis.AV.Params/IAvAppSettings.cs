namespace ComProvis.AV.Params
{
    public interface IAvAppSettings
    {
        string ApiAccessToken { get; set; }
        string ApiSecretKey { get; set; }
        string ServiceLink { get; set; }
        string ServicePlanId { get; set; }        
    }
}