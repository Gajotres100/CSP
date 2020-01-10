using ComProvis.AV.Code;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SaaSApi
{
    [ServiceContract]
    public class ComProvisAvSoap : IComProvisAvSoap
    {
        private IAvOperation _avOperation { get; set; }
        public ComProvisAvSoap(IAvOperation avOperation)
        {
            _avOperation = avOperation;
        }

        public async Task<Result> AddAsset(string CustomerId, AddAssetParams data) => await _avOperation.AddAssetAsync(CustomerId, data);

        public async Task<Result> AddCustomer(AddCustomerParams data) => await _avOperation.AddCompanyAsync(data);

        public async Task<Result> AddUser(string CustomerId, AddUserParams data) => await _avOperation.AddUserAsync(CustomerId, data);

        public async Task<Result> AssignProduct(string CustomerId, string UserId, string ProductId, AssignProductParams data) => await _avOperation.AssignProductAsync(CustomerId, UserId, ProductId, data);

        public async Task<GetCustomerResult> GetCustomer(string CustomerId) => await _avOperation.GetCompanyAsync(CustomerId);

        public TransactionResult GetTransactionStatus(string TransactionId) => _avOperation.GetTransactionStatus(TransactionId);

        public async Task<GetUserResult> GetUser(string CustomerId, string UserId) => await _avOperation.GetUserAsync(CustomerId, UserId);

        public async Task<Result> ReactivateCustomer(string CustomerId, ReactivateCustomerParams data) => await _avOperation.ReactivateCompanyAsync(CustomerId, data);

        public async Task<Result> RemoveAsset(string CustomerId, string AssetId, RemoveAssetParams data) => await _avOperation.RemoveAssetAsync(CustomerId, AssetId, data);

        public async Task<Result> RemoveCustomer(string CustomerId, RemoveCustomerParams data) => await _avOperation.RemoveCompanyAsync(CustomerId, data);

        public async Task<Result> RemoveProduct(string CustomerId, string UserId, string ProductId, RemoveProductParams data) => await _avOperation.RemoveProductAsync(CustomerId, UserId, ProductId, data);

        public async Task<Result> RemoveUser(string CustomerId, string UserId, RemoveUserParams data) => await _avOperation.RemoveUserAsync(CustomerId, UserId, data);

        public async Task<Result> SuspendCustomer(string CustomerId, SuspendCustomerParams data) => await _avOperation.SuspendCompanyAsync(CustomerId, data);

        public async Task<Result> UpdateAsset(string CustomerId, string AssetId, UpdateAssetParams data) => await _avOperation.UpdateAssetAsync(CustomerId, AssetId, data);

        public async Task<Result> UpdateCustomer(string CustomerId, UpdateCustomerParams data) => await _avOperation.UpdateCompanyAsync(CustomerId, data);

        public async Task<Result> UpdateUser(string CustomerId, string UserId, UpdateUserParams data) => await _avOperation.UpdateUserAsync(CustomerId, UserId, data);

        public async Task<ValidateResult> ValidateAsset(string AssetId, int ActionTypeId, string ProductId, int Quantity, AdditionalAttributes[] AdditionalAttribute)
             => await _avOperation.ValidateAssetAsync(AssetId, ActionTypeId, ProductId, Quantity, AdditionalAttribute);

        public ValidateResult ValidateAttribute(string Name, string Value, string ProductId, string CustomerId)
        {
            var ret = new ValidateResult
            {
                IsValid = true
            };

            var massages = new List<Messages>();
            var massage = new Messages
            {
                Language = "hr",
                Message = "Kloc"
            };
            massages.Add(massage);

            ret.Message = massages.ToArray();

            return ret;
        }
    }
}
