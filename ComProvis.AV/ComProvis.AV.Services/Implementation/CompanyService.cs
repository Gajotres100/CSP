using ComProvis.AV.Core;
using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Companys
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        public CompanyService(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<Result> AddCompany(Company company)
        {
            UnitOfWork.CompanyRepository.Add(company);
            await UnitOfWork.SaveChangesAsync();
            return new Result(true, null);
        }
    }
}
