using ComProvis.AV.Integration.Params;
using ComProvis.AV.Integration.Response;

namespace ComProvis.AV.Integration
{
    public interface IClient
    {
        CreateCustomerResponse CreateCustomer(CreateCustomerParam param);
        object createResetUserPasswordParam(ResetUserPasswordParam param);
        CreateSubscriptionResponse CreateSubscription(CreateSubscriptionParam param);
        string GetCustomerDownloadLink(string companyId);
        GetCustomersResponse GetCustomers(GetCustomersParam param);
        GetServicePlansResponse GetServicePlans();
        GetSubscriptionResponse GetSubscription(GetSubscriptionParam param);
        GetUsersResponse GetUsers(GetUsersParam param);
        bool InitializeCustomer(string companyId);
        bool ResetUserPassword(ResetUserPasswordParam param);
        SuspendSubscriptionResult SuspendSubscription(SuspendSubscriptionParam param);
        SuspendSubscriptionResult UnspendSubscription(SuspendSubscriptionParam param);
        UpdateSubscriptionResponse UpdateSubscription(UpdateSubscriptionParam param);
    }
}