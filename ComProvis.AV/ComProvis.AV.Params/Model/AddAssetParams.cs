using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class AddAssetParams : BaseParam
    {
        [DataMember]
        public string AssetId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public AdditionalAttributes[] AdditionalAttribute { get; set; }
    }
}
