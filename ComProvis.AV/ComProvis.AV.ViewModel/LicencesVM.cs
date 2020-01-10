using System.Collections.Generic;
using ComProvis.AV.Core.Entities;

namespace ComProvis.AV.ViewModel
{
    public class LicencesVM : ILicencesVM
    {
        public List<SpGetLicenceCountByCompanyId> SpGetLicenceCountByCompanyId { get; set; }
    }
}
