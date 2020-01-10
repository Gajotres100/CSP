using I = ComProvis.AV.Integration.Model;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Response
{
    public class GetUsersResponse
    {
        public int PagingTotal { get; set; }
        public int PagingLimit { get; set; }
        public int Page { get; set; }
        public IList<I.User> Users { get; set; }
    }
}
