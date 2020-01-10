using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ComProvis.AV.Enums;
namespace ComProvis.AV.UI.MVC.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("externalId")]
        public string externalId { get; set; }
        [JsonProperty("companyId")]
        public int? companyId { get; set; }
        [JsonProperty("username")]
        public string username { get; set; }

        public string roleId { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("contactInfo")]
        public string contactInfo { get; set; }
        [JsonProperty("isSuspended")]
        public bool? isSuspended { get; set; }
        [JsonProperty("isDeleted")]
        public bool? isDeleted { get; set; }
        [JsonProperty("createDate")]
        public DateTime? createDate { get; set; }
       
        public DateTime? lastChangeDate { get; set; }
        [JsonProperty("applicationLoginName")]
        public string applicationLoginName { get; set; }
        [JsonProperty("applicationPassword")]
        public string applicationPassword { get; set; }
        [JsonProperty("applicationID")]
        public Guid? applicationID { get; set; }

        public List<UserRole> userRole { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }
        public Role RoleId { get; set; }
    } 
}
