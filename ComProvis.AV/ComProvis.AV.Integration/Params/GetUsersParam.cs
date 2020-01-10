using System;

namespace ComProvis.AV.Integration.Params
{
    public class GetUsersParam
    {
        public Guid CustomerId { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
