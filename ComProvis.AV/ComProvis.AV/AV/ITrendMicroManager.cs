using ComProvis.AV.Integration.Response;
using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.AV
{
    public interface ITrendMicroManager
    {
        Task<string> CreateCustomerAsync(string ExternalId);

        Task<Result> CreateSubscriptionAsync(string applicationcustomerID, int quantity, string externalId, string servicePlanId, string assetId);

        Task<Result> UpdateSubscriptionAsync(int quantity, string externalId, string servicePlanId);

        Task<Result> SuspendSubscriptionAsync(string externalId);

        Task<Result> ReactivateSubscriptionAsync(string externalId);

        Task<GetSubscriptionResponse> GetSubscriptionAsync(string externalId);
        int GetUsersCount(string ApplicationCustomerID);
    }
}