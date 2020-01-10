using System.Linq;

namespace SaaSApi
{
    public class AssignProductParams : BaseParam
    {
        public AdditionalAttributes[] AdditionalAttribute { get; set; }

        public override string ToString()
        {
            return TransactionId + "-" + string.Join('#', AdditionalAttribute.ToList());
        }
    }
}
