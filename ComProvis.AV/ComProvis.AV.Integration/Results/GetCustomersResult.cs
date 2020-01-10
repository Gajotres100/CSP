using System;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Results
{
    internal class GetCustomersResult
    {
        public Paging paging { get; set; }
        public List<User> users { get; set; }

        internal class Paging
        {
            public int total { get; set; }
            public int limit { get; set; }
            public int page { get; set; }
        }
        internal class User
        {
            public Guid user_id { get; set; }
            public Guid customer_id { get; set; }
            public int status { get; set; }
            public string login_name { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string time_zone { get; set; }
            public string language { get; set; }
            public Phone phone { get; set; }
        }
        internal class Phone
        {
            public string area_code { get; set; }
            public string number { get; set; }
            public string extension { get; set; }
        }
    }
}
