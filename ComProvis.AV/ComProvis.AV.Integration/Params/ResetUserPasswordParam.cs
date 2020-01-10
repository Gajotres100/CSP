
namespace ComProvis.AV.Integration.Params
{
    public class ResetUserPasswordParam
    {
        public string LoginName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
