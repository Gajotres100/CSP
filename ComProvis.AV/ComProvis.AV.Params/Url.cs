namespace ComProvis.AV.Params
{
    public static class Url
    {
        public const string CustomersUrl = "/customers";

        public const string SubscriptionUrl = "/customers/{customer_id}/subscriptions";

        public const string GetSubscriptionUrl = "/customers/{customer_id}/subscriptions/{subscription_id}";

        public const string SuspendSubscriptionUrl = "/customers/{customer_id}/subscriptions/{subscription_id}/suspend";

        public const string UnsuspendSubscriptionUrl = "/customers/{customer_id}/subscriptions/{subscription_id}/unsuspend";

        public const string ServicePlans = "/me/serviceplans";

        public const string UsersUrl = "/customers/{customer_id}/users";

        public const string DownloadLinkUrl = "/service/wfbss/api/domains";

        public const string ResetPasswordUrl = "/reset_password";

        public const string InitializeCompanyUrl = "service/wfbss/api/initialize?cids={cids}";
    }
}
