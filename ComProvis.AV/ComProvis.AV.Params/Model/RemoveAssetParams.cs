namespace SaaSApi
{
    public class RemoveAssetParams : BaseParam
    {
        public string ProductId { get; set; }
        public override string ToString()
        {
            return TransactionId + "-" + ProductId;
        }
    }
}
