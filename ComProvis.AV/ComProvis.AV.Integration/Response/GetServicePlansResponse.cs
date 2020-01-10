using ComProvis.AV.Integration.Model;
using System.Collections.Generic;

namespace ComProvis.AV.Integration.Response
{
    public class GetServicePlansResponse
    {
        public IList<ServicePlan> ServicePlans { get; set; }
    }
}
