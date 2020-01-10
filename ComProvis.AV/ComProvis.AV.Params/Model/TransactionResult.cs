using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class TransactionResult
    {
        [DataMember(Order = 0)]
        public bool IsCompleted { get; set; }
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        [DataMember(Order = 2)]
        public List<Messages> Message { get; set; }

        public TransactionResult()
        {

        }

        public TransactionResult(bool isCompleted, bool success, string message)
        {
            IsCompleted = isCompleted;
            Success = success;
            Message = new List<Messages>
            {
                new Messages("hr", message)
            };
        }

        public static TransactionResult GetTransactionDoesntExist()
        {
            return new TransactionResult(false, false, "Transakcija ne postoji!");
        }
    }
}
