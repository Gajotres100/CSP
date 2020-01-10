namespace SaaSApi
{
    public class UpdateCustomerParams : BaseParam
    {
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
