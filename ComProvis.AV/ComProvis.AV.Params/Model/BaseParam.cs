using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public abstract class BaseParam
    {
        [DataMember]
        public string TransactionId { get; set; }
    }
}
