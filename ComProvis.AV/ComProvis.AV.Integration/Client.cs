using ComProvis.AV.Integration.Exceptions;
using ComProvis.AV.Integration.Model;
using ComProvis.AV.Integration.Params;
using ComProvis.AV.Integration.Response;
using ComProvis.AV.Integration.Results;
using ComProvis.AV.Params;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using I = ComProvis.AV.Integration.Model;

namespace ComProvis.AV.Integration
{
    public class Client : IClient
    {
        private const string ContentType = "application/json; charset=utf-8";
        private readonly IConfiguration _configuration;
        private readonly IAvAppSettings _settings;

        private RestClient _client;

        public Client(IConfiguration configuration, IAvAppSettings settings)
        {
            _configuration = configuration;
            _settings = settings;
            _configuration.GetSection("TrendMicro").Bind(_settings);
            _client = new RestClient(settings.ServiceLink);
        }

        public CreateCustomerResponse CreateCustomer(CreateCustomerParam param)
        {
            var request = new RestRequest(Url.CustomersUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var data = CreateNewCustomerParam(param);
            var jsonData = request.JsonSerializer.Serialize(data);

            request.AddBody(data);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, jsonData);

            var res = _client.Execute<CreateCustomerResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new CreateCustomerResponse
            {
                CustomerId = res.Data.customer_id,
                UserId = res.Data.user.id,
                LoginName = res.Data.user.login_name,
                Password = res.Data.user.password
            };
        }

        private object CreateNewCustomerParam(CreateCustomerParam param) => new
        {
            company = new
            {
                name = param.Customer.Name,
                state = param.Customer.State,
                country = param.Customer.Country,
                city = param.Customer.City,
                address = param.Customer.Address,
                postal_code = param.Customer.PostalCode,
                note = param.Customer.Note
            },
            user = new
            {
                login_name = param.User.LoginName,
                first_name = param.User.FirstName,
                last_name = param.User.LastName,
                email = param.User.Email,
                time_zone = param.User.TimeZone,
                language = param.User.Language,
                area_code = param.User.PhoneAreaCode,
                number = param.User.PhoneNumber,
                extension = param.User.PhoneExtension
            }
        };

        public GetCustomersResponse GetCustomers(GetCustomersParam param)
        {
            var request = new RestRequest("", Method.GET);

            var formatedStartDate = param.UserModifiedStart.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var formatedEndDate = param.UserModifiedEnd.ToString("yyyy-MM-ddTHH:mm:ssZ");

            request.AddParameter("user_modified_start", formatedStartDate);
            request.AddParameter("user_modified_end", formatedEndDate);

            if (param.Page.HasValue) request.AddParameter("page", param.Page.GetValueOrDefault());

            if (param.Limit.HasValue) request.AddParameter("limit", param.Limit.GetValueOrDefault());

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute<GetCustomersResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new GetCustomersResponse
            {
                PagingTotal = res.Data.paging.total,
                PagingLimit = res.Data.paging.limit,
                Page = res.Data.paging.page,
                Users = res.Data.users.Select(x => new I.User
                {
                    Id = x.user_id,
                    CustomerId = x.customer_id,
                    Status = x.status,
                    LoginName = x.login_name,
                    FirstName = x.first_name,
                    LastName = x.last_name,
                    Email = x.email,
                    TimeZone = x.time_zone,
                    Language = x.language,
                    PhoneAreaCode = x.phone.area_code,
                    PhoneNumber = x.phone.number,
                    PhoneExtension = x.phone.extension
                }).ToList()
            };
        }

        public GetUsersResponse GetUsers(GetUsersParam param)
        {
            var request = new RestRequest(Url.UsersUrl, Method.GET);

            request.AddUrlSegment("customer_id", param.CustomerId.ToString());

            if (param.Page.HasValue) request.AddParameter("page", param.Page.GetValueOrDefault());

            if (param.Limit.HasValue) request.AddParameter("limit", param.Limit.GetValueOrDefault());

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute<GetUsersResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new GetUsersResponse
            {
                PagingTotal = res.Data.paging.total,
                PagingLimit = res.Data.paging.limit,
                Page = res.Data.paging.page,
                Users = res.Data.users.Select(x => new I.User
                {
                    Id = x.user_id,
                    CustomerId = x.customer_id,
                    Status = x.status,
                    LoginName = x.login_name,
                    FirstName = x.first_name,
                    LastName = x.last_name,
                    Email = x.email,
                    TimeZone = x.time_zone,
                    Language = x.language,
                    PhoneAreaCode = x.phone.area_code,
                    PhoneNumber = x.phone.number,
                    PhoneExtension = x.phone.extension
                }).ToList()
            };
        }

        public CreateSubscriptionResponse CreateSubscription(CreateSubscriptionParam param)
        {
            var request = new RestRequest(Url.SubscriptionUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegment("customer_id", param.CustomerId.ToString());

            var data = CreateNewSubscriptionParam(param);
            var jsonData = request.JsonSerializer.Serialize(data);

            request.AddBody(data);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, jsonData);

            var res = _client.Execute<CreateSubscriptionResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new CreateSubscriptionResponse
            {
                SubscriptionId = res.Data.subscription_id,
                ProductName = res.Data.product_name,
                ServiceUrl = res.Data.service_url,
                Licenses = res.Data.licenses.Select(x => new License
                {
                    AcCode = x.ac_code,
                    ProductId = x.product_id,
                    Version = x.version,
                    LicenseStartDate = x.license_start_date,
                    LicenseExpirationDate = x.license_expiration_date,
                    StartChargeDate = x.start_charge_date,
                    Units = x.units
                }).ToList()
            };
        }

        private object CreateNewSubscriptionParam(CreateSubscriptionParam param) => new
        {
            service_plan_id = param.ServicePlanId,
            units_per_license = param.UnitsPerLicense,
            license_start_date = param.LicenseStartDate.HasValue ? param.LicenseStartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ") : ""
        };

        public GetSubscriptionResponse GetSubscription(GetSubscriptionParam param)
        {
            var request = new RestRequest(Url.GetSubscriptionUrl, Method.GET);

            request.AddParameter("customer_id", param.CustomerId.ToString());
            request.AddParameter("subscription_id", param.SubscriptionId.ToString());

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute<GetSubscriptionResult>(request);

            return new GetSubscriptionResponse
            {
                SubscriptionId = res.Data.subscription_id,
                Name = res.Data.name,
                Enabled = res.Data.enabled,
                IsAutoRenawal = res.Data.is_auto_renewal,
                AutoRenawalMonth = res.Data.auto_renawal_month,
                ExpirationNotification = res.Data.expiration_notification,
                ServiceUrl = res.Data.service_url,
                Licenses = res.Data.licenses.Select(x => new License
                {
                    AcCode = x.ac_code,
                    ProductId = x.product_id,
                    LicenseStartDate = x.license_start_date,
                    LicenseExpirationDate = x.license_expiration_date,
                    StartChargeDate = x.start_charge_date,
                    GracePeriod = x.grace_period,
                    Units = x.units,
                    Enabled = x.enabled
                }).ToList()
            };
        }

        public UpdateSubscriptionResponse UpdateSubscription(UpdateSubscriptionParam param)
        {
            var request = new RestRequest(Url.GetSubscriptionUrl, Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegment("customer_id", param.CustomerId.ToString());
            request.AddUrlSegment("subscription_id", param.SubscriptionId.ToString());

            var data = CreateUpdateSubscriptionParam(param);
            var jsonData = request.JsonSerializer.Serialize(data);

            request.AddBody(data);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, jsonData);

            var res = _client.Execute<UpdateSubscriptionResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new UpdateSubscriptionResponse
            {
                SubscriptionId = res.Data.subscription_id,
                Name = res.Data.name,
                Enabled = res.Data.enabled,
                IsAutoRenawal = res.Data.is_auto_renewal,
                AutoRenawalMonth = res.Data.auto_renawal_month,
                ExpirationNotification = res.Data.expiration_notification,
                ServiceUrl = res.Data.service_url,
                Licenses = res.Data.licenses.Select(x => new License
                {
                    AcCode = x.ac_code,
                    ProductId = x.product_id,
                    LicenseStartDate = x.license_start_date,
                    LicenseExpirationDate = x.license_expiration_date,
                    StartChargeDate = x.start_charge_date,
                    GracePeriod = x.grace_period,
                    Units = x.units,
                    Enabled = x.enabled
                }).ToList()
            };
        }

        private object CreateUpdateSubscriptionParam(UpdateSubscriptionParam param) => new
        {
            units_per_license = param.UnitsPerLicense,
            license_expiration_date = param.LicenseExpirationDate
        };

        public GetServicePlansResponse GetServicePlans()
        {
            var request = new RestRequest(Url.ServicePlans, Method.GET);

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute<List<GetServicePlansResult>>(request);

            return new GetServicePlansResponse
            {
                ServicePlans = res.Data.Select(x => new ServicePlan
                {
                    ServicePlanId = x.service_plan_id,
                    Name = x.name,
                    Product = x.product,
                    Version = x.version,
                    InitialLicensePeriodMonth = x.initial_license_period_month,
                    DataCenter = x.data_center,
                    DataCenterCode = x.data_center_code,
                    AutoRenewal = x.auto_renewal
                }).ToList()
            };
        }

        public SuspendSubscriptionResult SuspendSubscription(SuspendSubscriptionParam param)
        {
            var request = new RestRequest(Url.SuspendSubscriptionUrl, Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegment("customer_id", param.CustomerId.ToString());
            request.AddUrlSegment("subscription_id", param.SubscriptionId.ToString());

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new SuspendSubscriptionResult
            {
                Completed = true
            };
        }

        public SuspendSubscriptionResult UnspendSubscription(SuspendSubscriptionParam param)
        {
            var request = new RestRequest(Url.UnsuspendSubscriptionUrl, Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegment("customer_id", param.CustomerId.ToString());
            request.AddUrlSegment("subscription_id", param.SubscriptionId.ToString());

            //var data = createSuspendParam(param);
            //var jsonData = request.JsonSerializer.Serialize(data);

            //request.AddBody(data);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return new SuspendSubscriptionResult
            {
                Completed = true
            };
        }

        public string GetCustomerDownloadLink(string companyId)
        {
            var request = new RestRequest(Url.DownloadLinkUrl, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("cids", companyId);

            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute<GetCustomerDownloadLinkResult>(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return res.Data.customers.First().magic_link;
        }

        public bool InitializeCustomer(string companyId)
        {
            var request = new RestRequest(Url.InitializeCompanyUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegment("cids", companyId);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, String.Empty);

            var res = _client.Execute(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return true;
        }

        private object CreateSuspendParam(SuspendSubscriptionParam param)
        {
            return new
            {
                license_expiration_date = param.LicenseExpirationDate
            };
        }

        public bool ResetUserPassword(ResetUserPasswordParam param)
        {
            var request = new RestRequest(Url.ResetPasswordUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var data = createResetUserPasswordParam(param);
            var jsonData = request.JsonSerializer.Serialize(data);

            request.AddBody(data);
            var url = _client.BuildUri(request);

            request = ConstructRequestHeader(request, url.PathAndQuery, jsonData);

            var res = _client.Execute(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK) throw new TrendMicroApiException(res.StatusCode, res.StatusDescription, res.ErrorMessage, res.ErrorException);

            return true;
        }

        public object createResetUserPasswordParam(ResetUserPasswordParam param)
        {
            return new
            {

                login_name = param.LoginName,
                current_password = param.CurrentPassword,
                new_password = param.NewPassword
            };
        }

        #region UtilMethods

        private RestRequest ConstructRequestHeader(RestRequest request, string url, string content)
        {
            var unixTime = GetUnixTime();
            var method = request.Method.ToString();

            var signature = GetSignature(unixTime, method, url, content);
            var traceId = Guid.NewGuid().ToString();

            request.AddHeader("x-access-token", _settings.ApiAccessToken);
            request.AddHeader("x-posix-time", unixTime.ToString());
            request.AddHeader("x-signature", signature);
            request.AddHeader("x-traceid", traceId);
            request.AddHeader("content-type", ContentType);
            request.AddHeader("Date", DateTime.Now.ToUniversalTime().ToString("r"));

            return request;
        }

        private long GetUnixTime()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        private string GetSignature(long unixTime, string httpMethod, string requestUri, string content)
        {
            var encodedRequest = requestUri;
            var hashedContent = HashContent(content);

            var payload = $"{unixTime}{httpMethod}{encodedRequest}{hashedContent}";

            var signature = HashPayload(payload);

            return signature;
        }

        private string HashContent(string content)
        {
            if (String.IsNullOrEmpty(content)) return "";

            var hash = GenerateMD5Hash(content);

            return Convert.ToBase64String(hash);
        }

        private string HashPayload(string payload)
        {
            var hash = GenerateSHA256Hash(payload);

            return Convert.ToBase64String(hash);
        }

        private byte[] GenerateMD5Hash(string data)
        {
            var md5 = MD5.Create();

            var inputBytes = System.Text.Encoding.UTF8.GetBytes(data);

            return md5.ComputeHash(inputBytes);
        }

        private byte[] GenerateSHA256Hash(string data)
        {
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_settings.ApiSecretKey);

            using (var sha256 = new HMACSHA256(keyBytes))
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(data);

                return sha256.ComputeHash(inputBytes);
            }
        }

        #endregion
    }
}
