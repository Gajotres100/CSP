using ComProvis.AV.ViewModel;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public interface IUiServices
    {
        Task<ILicencesVM> GetLicencesAsync(string ssoId);
        Task<IUserVM> GetUserAsync(string ssoId);
        Task<string> GetCustomerDownloadLinkAsync(string ssoId);
        void InitializeCustomer(string ssoId);
    }
}
