using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class ApplicationLicencesRepository :  Repository<ApplicationLicences>, IApplicationLicencesRepository
    {
        public ApplicationLicencesRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
