using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class AdditionalAttributes
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
