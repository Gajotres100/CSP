
using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Companys
{
    public interface ICompanyService : IService<Company>
    {
        Task<Result> AddCompany(Company company);
    }
}
