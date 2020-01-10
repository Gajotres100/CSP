using System;

namespace ComProvis.AV.Integration.Response
{
    public class CreateCustomerResponse
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
