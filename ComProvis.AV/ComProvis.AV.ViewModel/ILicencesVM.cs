using ComProvis.AV.Core.Entities;
using System.Collections.Generic;

namespace ComProvis.AV.ViewModel
{
    public interface ILicencesVM
    {
        List<SpGetLicenceCountByCompanyId> SpGetLicenceCountByCompanyId { get; set; }
    }
}