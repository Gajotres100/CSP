using SaaSApi;
using System.Threading.Tasks;

namespace ComProvis.AV.Code
{
    public interface IAvOperation
    {
        Task<Result> AddAssetAsync(string customerId, AddAssetParams data);
        Task<Result> AddCompanyAsync(AddCustomerParams data);
        Task<Result> AddUserAsync(string companyId, AddUserParams data);
        Task<Result> AssignProductAsync(string customerId, string userId, string productId, AssignProductParams data);
        Task<GetCustomerResult> GetCompanyAsync(string companyId);
        TransactionResult GetTransactionStatus(string transactionId);
        Task<GetUserResult> GetUserAsync(string customerId, string userId);
        Task<Result> ReactivateCompanyAsync(string externalId, ReactivateCustomerParams data);
        Task<Result> RemoveAssetAsync(string customerId, string assetId, RemoveAssetParams data);
        Task<Result> RemoveCompanyAsync(string externalId, RemoveCustomerParams data);
        Task<Result> RemoveProductAsync(string customerId, string userId, string productId, RemoveProductParams data);
        Task<Result> RemoveUserAsync(string customerId, string externalId, RemoveUserParams data);
        Task<Result> SuspendCompanyAsync(string externalId, SuspendCustomerParams data);
        Task<Result> UpdateAssetAsync(string customerId, string assetId, UpdateAssetParams data);
        Task<Result> UpdateCompanyAsync(string externalId, UpdateCustomerParams data);
        Task<Result> UpdateUserAsync(string customerId, string userId, UpdateUserParams data);
        Task<ValidateResult> ValidateAssetAsync(string AssetId, int ActionTypeId, string ProductId, int Quantity, AdditionalAttributes[] AdditionalAttribute);
    }
}