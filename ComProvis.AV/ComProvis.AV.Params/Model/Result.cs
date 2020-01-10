using System.Runtime.Serialization;

namespace SaaSApi
{
    [DataContract]
    public class Result
    {
        [DataMember(Order = 0)]
        public bool IsCompleted { get; set; }
        [DataMember(Order = 1)]
        public bool Success { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }

        public Result()
        {

        }

        public Result(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                Success = true;
                IsCompleted = true;
            }
            else
            {
                Success = false;
                IsCompleted = false;
            }

            Message = message;
        }
    }
}
