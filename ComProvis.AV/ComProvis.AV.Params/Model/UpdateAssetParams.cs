namespace SaaSApi
{
    public class UpdateAssetParams : BaseParam
    {
        public AdditionalAttributes[] AdditionalAttribute { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
