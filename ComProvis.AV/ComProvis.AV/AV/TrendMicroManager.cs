using I = ComProvis.AV.Integration.Model;
using ComProvis.AV.Integration;
using ComProvis.AV.Integration.Params;
using ComProvis.AV.Services;
using ComProvis.AV.Integration.Response;
using System;
using SaaSApi;
using System.Linq;
using ComProvis.AV.Enums;
using System.Threading.Tasks;

namespace ComProvis.AV.AV
{
    public class TrendMicroManager : ITrendMicroManager
    {

        private IClient _client;
        private IDataService _dataService { get; set; }

        public TrendMicroManager(IClient client, IDataService dataService)
        {
            _client = client;
            _dataService = dataService;
        }

        public async Task<string> CreateCustomerAsync(string externalId)
        {
            var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
            var user = await _dataService.UserService.ExternalGetAsync(externalId);

            var param = new CreateCustomerParam
            {
                Customer = new I.Customer
                {
                    Name = company.Name,
                    State = "HR",
                    Country = "HR",
                    City = "Zagreb",
                    Address = company.Address,
                    PostalCode = 10000.ToString(),
                    Note = ""
                },
                User = new I.User
                {
                    LoginName = user.ApplicationLoginName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    TimeZone = "UTC",
                    Language = "en-US"
                }
            };

            var res = _client.CreateCustomer(param);

            company.ApplicationCustomerID = res.CustomerId.ToString();
            company.IsDeleted = false;

            user.ApplicationLoginName = res.LoginName;
            user.ApplicationPassword = res.Password;
            user.ApplicationID = res.UserId.ToString();

            _dataService.CompanyService.Update(company);
            _dataService.UserService.Update(user);

            await _dataService.SaveChangesAsync();

            return res.CustomerId.ToString();
        }

        public async Task<Result> CreateSubscriptionAsync(string applicationcustomerID, int quantity, string externalId, string servicePlanId, string assetId)
        {
            var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
            var user = await _dataService.UserService.ExternalGetAsync(externalId);
            var role = await _dataService.RolesService.GetAsync((int)Role.Admin);

            var param = new CreateSubscriptionParam
            {
                CustomerId = new Guid(applicationcustomerID),
                ServicePlanId = new Guid(servicePlanId),
                UnitsPerLicense = quantity,
                LicenseStartDate = DateTime.Now
            };

            var res = _client.CreateSubscription(param);

            company.ApplicationSubscriptionId = res.SubscriptionId.ToString();

            _dataService.CompanyService.Update(company);

            var applicationLicences = res.Licenses.Select(x => new ApplicationLicences
            {
                LicenceKey = x.AcCode,
                GracePeriod = x.GracePeriod,
                LicenceExpirationDate = x.LicenseExpirationDate,
                LicenceStartDate = x.LicenseStartDate,
                StartChargeDate = x.StartChargeDate,
                Units = x.Units,
                Version = x.Version,
                ExternalId = assetId
            }).ToList();

            var subscription = new ApplicationSubscription
            {
                ServiceUrl = res.ServiceUrl,
                SubscriptionId = res.SubscriptionId.ToString(),
                ProductName = res.ProductName,
                CompanyId = company.Id,
                ApplicationLicences = applicationLicences,
                ExternalId = assetId
            };

            _dataService.ApplicationSubscriptionService.Add(subscription);

            var userRole = new UserRole
            {
                Quantity = quantity,
                Role = role,
                User = user
            };
            _dataService.UserRoleService.Add(userRole);

            await _dataService.SaveChangesAsync();

            return new Result(true, null);
        }

        public async Task<Result> UpdateSubscriptionAsync(int quantity, string externalId, string servicePlanId)
        {
            var customer = await _dataService.CompanyService.ExternalGetAsync(externalId);

            var param = new UpdateSubscriptionParam
            {
                CustomerId = new Guid(customer.ApplicationCustomerID),
                SubscriptionId = new Guid(customer.ApplicationSubscriptionId),
                UnitsPerLicense = quantity,
            };

            _client.UpdateSubscription(param);

            return new Result(true, null);
        }

        public async Task<Result> SuspendSubscriptionAsync(string externalId)
        {
            var customer = await _dataService.CompanyService.ExternalGetAsync(externalId);

            var param = new SuspendSubscriptionParam
            {
                CustomerId = new Guid(customer.ApplicationCustomerID),
                SubscriptionId = new Guid(customer.ApplicationSubscriptionId),
                LicenseExpirationDate = null,
            };

            _client.SuspendSubscription(param);
            return new Result(true, null);
        }

        public async Task<Result> ReactivateSubscriptionAsync(string externalId)
        {
            var customer = await _dataService.CompanyService.ExternalGetAsync(externalId);

            var param = new SuspendSubscriptionParam
            {
                CustomerId = new Guid(customer.ApplicationCustomerID),
                SubscriptionId = new Guid(customer.ApplicationSubscriptionId),
                LicenseExpirationDate = null,
            };

            _client.UnspendSubscription(param);

            return new Result(true, null);
        }

        public async Task<GetSubscriptionResponse> GetSubscriptionAsync(string externalId)
        {
            var customer = await _dataService.CompanyService.ExternalGetAsync(externalId);

            var param = new GetSubscriptionParam
            {
                CustomerId = new Guid(customer.ApplicationCustomerID),
                SubscriptionId = new Guid(customer.ApplicationSubscriptionId),

            };

            return _client.GetSubscription(param);
        }

        public int GetUsersCount(string ApplicationCustomerID)
        {
            if (ApplicationCustomerID == null) return 0;

            var param = new GetUsersParam
            {
                CustomerId = new Guid(ApplicationCustomerID)
            };

            var res = _client.GetUsers(param);
            return res.Users.Count();
        }
    }
}
