using System;

namespace ComProvis.AV.Integration.Params
{
    public class GetCustomersParam
    {
        public DateTime UserModifiedStart { get; set; }
        public DateTime UserModifiedEnd { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
