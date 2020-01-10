using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;

namespace ComProvis.AV.Data.Repositories
{
    class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IComProvisAvDbContext context) : base(context)
        {

        }
    }
}
