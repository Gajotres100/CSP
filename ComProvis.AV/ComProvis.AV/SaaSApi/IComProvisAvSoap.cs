using System.ServiceModel;
using System.Threading.Tasks;

namespace SaaSApi
{
    [ServiceContract(Name = "SaaS")]
    public interface IComProvisAvSoap
    {
        #region Customer/user

        [OperationContract]
        Task<Result> AddCustomer(AddCustomerParams data);

        [OperationContract]
        Task<Result> UpdateCustomer(string CustomerId, UpdateCustomerParams data);

        [OperationContract]
        Task<Result> RemoveCustomer(string CustomerId, RemoveCustomerParams data);

        [OperationContract]
        Task<Result> SuspendCustomer(string CustomerId, SuspendCustomerParams data);

        [OperationContract]
        Task<Result> ReactivateCustomer(string CustomerId, ReactivateCustomerParams data);

        [OperationContract]
        Task<GetCustomerResult> GetCustomer(string CustomerId);

        [OperationContract]
        Task<GetUserResult> GetUser(string CustomerId, string UserId);

        [OperationContract]
        Task<Result> AddUser(string CustomerId, AddUserParams data);

        [OperationContract]
        Task<Result> UpdateUser(string CustomerId, string UserId, UpdateUserParams data);

        [OperationContract]
        Task<Result> RemoveUser(string CustomerId, string UserId, RemoveUserParams data);

        #endregion

        #region Asset/product

        [OperationContract]
        Task<Result> AssignProduct(string CustomerId, string UserId, string ProductId, AssignProductParams data);

        [OperationContract]
        Task<Result> AddAsset(string CustomerId, AddAssetParams data);

        [OperationContract]
        Task<Result> UpdateAsset(string CustomerId, string AssetId, UpdateAssetParams data);

        [OperationContract]
        Task<Result> RemoveAsset(string CustomerId, string AssetId, RemoveAssetParams data);

        [OperationContract]
        Task<Result> RemoveProduct(string CustomerId, string UserId, string ProductId, RemoveProductParams data);

        #endregion

        [OperationContract]
        TransactionResult GetTransactionStatus(string TransactionId);

        [OperationContract]
        ValidateResult ValidateAttribute(string Name, string Value, string ProductId, string CustomerId);

        [OperationContract]
        Task<ValidateResult> ValidateAsset(string AssetId, int ActionTypeId, string ProductId, int Quantity, AdditionalAttributes[] AdditionalAttribute);
    }
}