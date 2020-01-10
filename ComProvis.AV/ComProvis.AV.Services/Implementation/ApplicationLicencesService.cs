using ComProvis.AV.Core;
using ComProvis.AV.Services.Interfaces;

namespace ComProvis.AV.Services.Implementation
{
    public class ApplicationLicencesService : Service<ApplicationLicences>, IApplicationLicencesService
    {
        public ApplicationLicencesService(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
