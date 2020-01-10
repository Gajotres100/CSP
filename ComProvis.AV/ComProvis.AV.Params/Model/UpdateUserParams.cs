namespace SaaSApi
{
    public class UpdateUserParams : BaseParam
    {
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
