using System;

namespace ComProvis.AV.Integration.Results
{
    internal class CreateCustomerResult
    {
        public Guid customer_id { get; set; }
        public User user { get; set; }

        internal class User
        {
            public Guid id { get; set; }
            public string login_name { get; set; }
            public string password { get; set; }
        }
    }
}
