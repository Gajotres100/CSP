using I = ComProvis.AV.Integration.Model;

namespace ComProvis.AV.Integration.Params
{
    public class CreateCustomerParam
    {
        public I.Customer Customer { get; set; }
        public I.User User { get; set; }
    }
}
