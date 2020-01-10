using ComProvis.AV.Integration;
using ComProvis.AV.ViewModel;
using System;
using System.Threading.Tasks;

namespace ComProvis.AV.Services
{
    public class UiServices : IUiServices
    {
        readonly IDataService _dataService;
        readonly ILicencesVM _licencesVM;
        readonly IUserVM _userVM;
        readonly IClient _client;
        public UiServices(IDataService dataService, IUserVM userVM, ILicencesVM licencesVM, IClient client)
        {
            _dataService = dataService;
            _userVM = userVM;
            _licencesVM = licencesVM;
            _client = client;
        }
        public async Task<ILicencesVM> GetLicencesAsync(string ssoId)
        {
            try
            {
                var user = await _dataService.UserService.ExternalGetAsync(ssoId);

                _licencesVM.SpGetLicenceCountByCompanyId = await _dataService.UserRoleService.GetLicenceCountByCompanyIdAsync(user.CompanyId.GetValueOrDefault());
                return _licencesVM;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IUserVM> GetUserAsync(string ssoId)
        {
            try
            {
                _userVM.User = await _dataService.UserService.ExternalGetAsync(ssoId);

                var company = await _dataService.CompanyService.GetAsync(_userVM.User.CompanyId.GetValueOrDefault());

                _userVM.User.UserRole = await _dataService.UserRoleService.GetByUserIDAsync(_userVM.User.Id);

                if (company?.IsSuspended == true)
                    _userVM.User.IsSuspended = true;

                if (company?.IsDeleted == true)
                    _userVM.User.IsDeleted = true;

                return _userVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async void InitializeCustomer(string ssoId)
        {
            try
            {
                var user = await _dataService.UserService.ExternalGetAsync(ssoId);
                var company = await _dataService.CompanyService.GetAsync(user.CompanyId.GetValueOrDefault());

                _client.InitializeCustomer(company.ApplicationCustomerID);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<string> GetCustomerDownloadLinkAsync(string ssoId)
        {
            try
            {
                var user = await _dataService.UserService.ExternalGetAsync(ssoId);
                var company = await _dataService.CompanyService.GetAsync(user.CompanyId.GetValueOrDefault());

                return _client.GetCustomerDownloadLink(company.ApplicationCustomerID);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
