using ComProvis.AV.AV;
using ComProvis.AV.Code.Validators;
using ComProvis.AV.Enums;
using ComProvis.AV.Params;
using ComProvis.AV.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaaSApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComProvis.AV.Code
{
    public class AvOperation : IAvOperation
    {
        #region Prop

        private IDataService _dataService { get; set; }
        private ITrendMicroManager _trendMicroManager { get; set; }
        private IConfiguration _configuration { get; set; }
        private IAvAppSettings _settings { get; set; }

        #endregion

        #region Ctor

        public AvOperation(IDataService dataService, ITrendMicroManager trendMicroManager, IConfiguration configuration, IAvAppSettings settings)
        {
            _dataService = dataService;
            _trendMicroManager = trendMicroManager;
            _configuration = configuration;
            _settings = settings;
            _configuration.GetSection("TrendMicro").Bind(_settings);
        }

        #endregion

        #region Company

        public async Task<GetCustomerResult> GetCompanyAsync(string companyId)
        {
            try
            {
                return (GetCustomerResult) await _dataService.CompanyService.ExternalGetAsync(companyId);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(GetCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, null, companyId);
                return null;
            }
        }

        public async Task<Result> RemoveCompanyAsync(string externalId, RemoveCustomerParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
                company.IsSuspended = true;
                company.IsDeleted = true;

                _dataService.CompanyService.Update(company);
                await _dataService.SaveChangesAsync();

                return await _trendMicroManager.SuspendSubscriptionAsync(externalId);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(RemoveCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> SuspendCompanyAsync(string externalId, SuspendCustomerParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
                company.IsSuspended = true;

                _dataService.CompanyService.Update(company);
                await _dataService.SaveChangesAsync();

                return await _trendMicroManager.SuspendSubscriptionAsync(externalId);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(SuspendCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> AddCompanyAsync(AddCustomerParams data)
        {
            try
            {
                var newCompany = data.GetCompany();

                var validator = new AddCompanyValidator(_dataService);
                var valResults = validator.Validate(newCompany);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                return await _dataService.CompanyService.AddCompany(newCompany);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(AddCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> ReactivateCompanyAsync(string externalId, ReactivateCustomerParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
                company.IsSuspended = false;

                _dataService.CompanyService.Update(company);
                await _dataService.SaveChangesAsync();

                return await _trendMicroManager.ReactivateSubscriptionAsync(externalId);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(ReactivateCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> UpdateCompanyAsync(string externalId, UpdateCustomerParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(externalId);
                company.Address = data.Address;
                company.ContactEmail = data.Email;
                company.ContactFirstName = data.FirstName;
                company.ContactLastName = data.LastName;
                company.Name = data.CompanyName;

                _dataService.CompanyService.Update(company);
                await _dataService.SaveChangesAsync();

                return new Result(true, null);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(UpdateCompanyAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        #endregion

        #region Assets

        public async Task<Result> AddAssetAsync(string customerId, AddAssetParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(customerId);

                if (_trendMicroManager.GetUsersCount(company.ApplicationCustomerID) == 0)
                {
                    var applicationCustomerId = await _trendMicroManager.CreateCustomerAsync(customerId);
                    return await _trendMicroManager.CreateSubscriptionAsync(applicationCustomerId, data.Quantity, customerId, _settings.ServicePlanId, data.AssetId);
                }
                else
                {
                    var subscription = await _trendMicroManager.GetSubscriptionAsync(customerId);
                    if (subscription.Licenses[0].LicenseExpirationDate < DateTime.Now)
                        await _trendMicroManager.ReactivateSubscriptionAsync(customerId);
                    var ret = await _trendMicroManager.UpdateSubscriptionAsync(data.Quantity, customerId, _settings.ServicePlanId);

                    var user = await _dataService.UserService.ExternalGetAsync(customerId);
                    var role = await _dataService.RolesService.GetAsync((int)Role.Admin);
                    var customer = await _dataService.CompanyService.ExternalGetAsync(customerId);
                    var userRole = await _dataService.UserRoleService.GetByUserIdAndRoleAsync(user.Id, role.Id);


                    if (userRole == null)
                    {
                        userRole = new UserRole
                        {
                            Quantity = data.Quantity,
                            RoleId = role.Id,
                            UserId = user.Id
                        };
                        _dataService.UserRoleService.Add(userRole);
                    }
                    else
                    {
                        userRole.Quantity = data.Quantity;
                        _dataService.UserRoleService.Update(userRole);
                    }

                    customer.IsDeleted = false;
                    customer.IsSuspended = false;
                    _dataService.CompanyService.Update(customer);

                    await _dataService.SaveChangesAsync();

                    return ret;
                }

            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(AddAssetAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> UpdateAssetAsync(string customerId, string assetId, UpdateAssetParams data)
        {
            try
            {
                var customer = await _dataService.CompanyService.ExternalGetAsync(customerId);
                var user = await _dataService.UserService.ExternalGetAsync(customerId);
                var role = await _dataService.RolesService.GetAsync((int)Role.Admin);
                var userRole = await _dataService.UserRoleService.GetByUserIdAndRoleAsync(user.Id, role.Id);

                var ret = await _trendMicroManager.UpdateSubscriptionAsync(data.Quantity, customerId, _settings.ServicePlanId);

                userRole.Quantity = data.Quantity;

                customer.IsDeleted = false;
                customer.IsSuspended = false;
                _dataService.CompanyService.Update(customer);

                _dataService.UserRoleService.Update(userRole);

                await _dataService.SaveChangesAsync();

                return ret;
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(UpdateAssetAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> RemoveAssetAsync(string customerId, string assetId, RemoveAssetParams data)
        {
            try
            {
                var customer = await _dataService.CompanyService.ExternalGetAsync(customerId);
                var user = await _dataService.UserService.ExternalGetAsync(customerId);
                var role = await _dataService.RolesService.GetAsync((int)Role.Admin);
                var userRole = await _dataService.UserRoleService.GetByUserIdAndRoleAsync(user.Id, role.Id);

                var ret = await _trendMicroManager.SuspendSubscriptionAsync(customerId);

                _dataService.UserRoleService.Delete(userRole);

                await _dataService.SaveChangesAsync();

                return ret;
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(RemoveAssetAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        #endregion

        #region User

        public async Task<GetUserResult> GetUserAsync(string customerId, string userId)
        {
            try
            {
                return (GetUserResult)(await _dataService.UserService.ExternalGetAsync(userId));
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(GetUserAsync), nameof(Enums.LogLevel.Error), ex.Message, null, userId);
                return null;
            }
        }

        public async Task<Result> AddUserAsync(string companyId, AddUserParams data)
        {
            try
            {
                var company = await _dataService.CompanyService.ExternalGetAsync(companyId);

                var ug = new UsernameGenerator(data.FirstName, data.LastName, company.Name, UsernameGenerator.UserType.User);

                var newUser = data.GetUser();
                newUser.CompanyId = company.Id;
                newUser.ApplicationLoginName = ug.Username;

                var validator = new AddUserValidator(_dataService);
                var valResults = validator.Validate(newUser);

                var validationSucceeded = valResults.IsValid;
                if (!validationSucceeded)
                {
                    var failures = valResults.Errors;
                    var message = failures.Aggregate(string.Empty, (current, failure) => current + (failure.ErrorMessage + "<br />"));
                    return new Result { IsCompleted = false, Success = false, Message = message };
                }

                return await _dataService.UserService.AddUserAsync(newUser);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(AddUserAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> UpdateUserAsync(string customerId, string userId, UpdateUserParams data)
        {
            try
            {
                var user = await _dataService.UserService.ExternalGetAsync(userId);
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.Address = data.Address;
                user.Email = data.Email;
                user.ContactInfo = data.ContactInfo;

                _dataService.UserService.Update(user);
                await _dataService.SaveChangesAsync();

                return new Result(true, null);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(UpdateUserAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> RemoveUserAsync(string customerId, string externalId, RemoveUserParams data)
        {
            try
            {
                var user = await _dataService.UserService.ExternalGetAsync(externalId);
                user.IsDeleted = true;

                _dataService.UserService.Update(user);
                await _dataService.SaveChangesAsync();

                return new Result(true, null);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(RemoveUserAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        #endregion

        #region Transaction

        public TransactionResult GetTransactionStatus(string transactionId)
        {
            try
            {
                return new TransactionResult { IsCompleted = true, Success = true};
            }
            catch (Exception ex)
            {
                _dataService.LogDataService.InsertLogoRecordAsync(nameof(GetTransactionStatus), nameof(Enums.LogLevel.Error), ex.Message, transactionId, transactionId);
                return new TransactionResult { IsCompleted = false, Success = false };
            }
        }

        #endregion

        #region Assignation

        public async Task<Result> AssignProductAsync(string customerId, string userId, string productId, AssignProductParams data)
        {
            try
            {
                var role = await _dataService.RolesService.ExternalGetAsync(productId);
                var user = await _dataService.UserService.ExternalGetAsync(userId);

                var userRole = new UserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                    ExternalId = data.TransactionId,
                    Quantity = 1 //Uvijek ide samo jedna licenca
                };

                _dataService.UserRoleService.Add(userRole);
                await _dataService.SaveChangesAsync();

                return new Result(true, null);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(AssignProductAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> RemoveProductAsync(string customerId, string userId, string productId, RemoveProductParams data)
        {
            try
            {
                var role = await _dataService.RolesService.ExternalGetAsync(productId);
                var user = await _dataService.UserService.ExternalGetAsync(userId);

                return await _dataService.UserRoleService.RemoveUserRole(user.Id, role.Id);
            }
            catch (Exception ex)
            {
                await _dataService.LogDataService.InsertLogoRecordAsync(nameof(RemoveProductAsync), nameof(Enums.LogLevel.Error), ex.Message, data.TransactionId, JsonConvert.SerializeObject(data));
                return new Result(false, ex.Message);
            }
        }

        #endregion

        #region Validation

        public async Task<ValidateResult> ValidateAssetAsync(string AssetId, int ActionTypeId, string ProductId, int Quantity, AdditionalAttributes[] AdditionalAttribute)
        {
            var ret = new ValidateResult
            {
                IsValid = true
            };

            var asset = await _dataService.ApplicationLicencesService.ExternalGetAsync(AssetId);

            if (ActionTypeId == (int)Enums.ActionTypeId.DeleteAsset && asset?.LicenceStartDate.GetValueOrDefault().Date == DateTime.Now.Date)
            {
                ret.IsValid = false;

                var massages = new List<Messages>();
                var massage = new Messages
                {
                    Language = "hr",
                    Message = "Licencu nije moguće obrisati na dan kada je kupljena"
                };
                massages.Add(massage);

                ret.Message = massages.ToArray();
            }
            return ret;
        }

        #endregion
    }
}
