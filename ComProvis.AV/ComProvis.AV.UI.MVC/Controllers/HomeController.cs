using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComProvis.AV.UI.MVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using HtmlAgilityPack;
using System.Net.Http;
using Newtonsoft.Json;
using ComProvis.AV.UI.MVC.Helper;
using ComProvis.AV.UI.Models;
using ComProvis.AV.Enums;
using Microsoft.AspNetCore.Localization;

namespace ComProvis.AV.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
       
        private IConfiguration _configuration;
        private string ApiService = string.Empty;
        
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;

            
        }
        public async Task<IActionResult> Index()
        {
            ApiService = _configuration["ApiService"];
            var urltry = ApiService + "tryLogin";

            return Redirect(urltry);
           

        }

        public async Task<IActionResult> About(string ssoID, string token, string culture)
        {
            var model = new HelpModel();
            var def = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture2 = def.RequestCulture.Culture;
            ViewData["Message"] = " ";


            ApiService = _configuration["ApiUrl"];

            using (var clientapi = new HttpClient())
            {
                // user data
                var url = ApiService + "api/user?ssoId=" + ssoID;
                //add header bearer Authorization
                clientapi.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
                var uriUser = new Uri(url);

                var responseUser = await clientapi.GetAsync(uriUser);
                var jsonUser = await responseUser.Content.ReadAsStringAsync(); //This is working
                                                                               // var Userdata = JsonConvert.DeserializeObject<User>(json);
                var user = JsonConvert.DeserializeObject<RootObject>(jsonUser);
                model.Fulluser = user.User.firstName + " " + user.User.lastName;
                model.ssoId = ssoID;
                model.token = token;
                model.Culture = culture.ToString();
                model.clientUrl = _configuration["MKPLink"];

            }


                return View(model);
        }
        public async Task<IActionResult> Home()
        {
            ApiService = _configuration["ApiService"];

            var urlLogin = ApiService + "login";

            return Redirect(urlLogin);


        }
        public ActionResult UnAuthorized()
        {
            return View();
        }
        public async Task<IActionResult> HomePage(string ssoId,string token,string culture)
        {
            var model = new HomeModel();
            if (!string.IsNullOrEmpty(ssoId))
            {
               
                var def = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                // Culture contains the information of the requested culture
                var culture2 = def.RequestCulture.Culture;

                ApiService = _configuration["ApiUrl"];

                var urlLicences = ApiService + "api/Licences?ssoId=" + ssoId;

                using (var clientapi = new HttpClient())
                {
                    // user data
                    var url = ApiService + "api/user?ssoId=" + ssoId;
                    //add header bearer Authorization
                    clientapi.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
                    var uriUser = new Uri(url);

                    var responseUser = await clientapi.GetAsync(uriUser);
                    var jsonUser = await responseUser.Content.ReadAsStringAsync(); 
                    
                    //This is working
                                                                            // var Userdata = JsonConvert.DeserializeObject<User>(json);
                    var user = JsonConvert.DeserializeObject<RootObject>(jsonUser);

                    if (user.User.isSuspended==true || user.User.isDeleted ==true || user.User.userRole.Count==0)
                    {
                        return RedirectToAction("UnAuthorized");
                    }
                    // data
                    var uri = new Uri(urlLicences);

                    var response = await clientapi.GetAsync(uri);
                    var json = await response.Content.ReadAsStringAsync(); //This is working

                    var obj = JsonConvert.DeserializeObject<HomeObject>(json);

                    //set home model
                    model.selfServiceLicenses = obj.List.FirstOrDefault(x => x.roleId == 2) == null ? 0 : obj.List.FirstOrDefault(x => x.roleId == 2).quantity;
                   // model.selfServiceLicenses = obj.List.FirstOrDefault(x => x.roleId == 2)?.quantity;
                 //   model.monthlyLicenses = obj.List.FirstOrDefault(x => x.roleId == 1)?.quantity;
                    model.monthlyLicenses = obj.List.FirstOrDefault(x => x.roleId == 1) == null ? 0 : obj.List.FirstOrDefault(x => x.roleId == 1).quantity;
                    model.fullName = user.User.firstName+" "+user.User.lastName;
                    model.ssoId = ssoId;
                    model.clientUrl = _configuration["MKPLink"];
                    model.IsAdmin = user.User.userRole.Count() > 1 ? true : (user.User.userRole.Count() == 1 && user.User.userRole.Select(x=>x.RoleId).FirstOrDefault()==Role.User) ? false : true;
                    model.Token = token;
                    model.Culture = culture2.ToString();
                    return View(model);
                }
            }
            model.selfServiceLicenses = 0;
            model.monthlyLicenses = 0;
            model.fullName = "";
            model.ssoId = "";
            return View(model);


        }
        public async Task<IActionResult> Login(string ssoId, string token)
        {
            //get user data TODO
            var loginPage = _configuration["LoginPage"];
            var PostPage = _configuration["PostPage"];
             
            ApiService = _configuration["ApiUrl"];           
           
            var url = ApiService+"api/user?ssoId="+ssoId;
            var model = new TrendMicroLoginModel();



            using (var clientapi = new HttpClient())
            {
                var uri = new Uri(url);
                clientapi.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));

                var response = await clientapi.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync(); //This is working
               // var Userdata = JsonConvert.DeserializeObject<User>(json);
                var obj = JsonConvert.DeserializeObject<RootObject>(json);
                model.username = obj.User.applicationLoginName;
                model.password = obj.User.applicationPassword;

            }


            var client = new WebClientEx();
            var data = client.DownloadString(loginPage);

            var doc = new HtmlDocument();
            doc.LoadHtml(data);

            var AuthState = doc.GetElementbyId("AuthState").GetAttributeValue("value", "");
           
            var cookies = CookieContainerExtension.GetAllCookies(client.CookieContainer).ToList();

            foreach (var c in cookies)
            {
                //var cookie = new HttpCookie(c.Name, c.Value);
                var option = new CookieOptions();
                option.Domain = c.Domain;
                option.Expires = DateTime.Now.AddMinutes(5);

                Response.Cookies.Append(c.Name, c.Value, option);
               // Response.Cookies.Add(cookie);
            }

            model.LoginPage = loginPage;
            model.PostPage = PostPage;
            model.AuthState = AuthState;
            
            return View(model);
        }
        //download clinet
        public async Task<IActionResult> Download(string ssoId,string token)
        {
            ApiService = _configuration["ApiUrl"];
            using (var clientapi = new HttpClient())
            {
                var urlClient = ApiService + "api/Licences/" + ssoId + "/GetCustomerDownloadLink";

                clientapi.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
                var uriClientUrl = new Uri(urlClient);

                var responseClientUrl = await clientapi.GetAsync(uriClientUrl);
                var jsonClientUrl = await responseClientUrl.Content.ReadAsStringAsync();

                return Json(jsonClientUrl);
            }
               

            


        }
        public IActionResult Logout()
        {
            var logoutLink = _configuration["MKPLogoutLink"];
            return Redirect(logoutLink);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        class RootObject
        {
            [JsonProperty("user")]
            public User User { get; set; }
        }
        class HomeObject
        {
            [JsonProperty("spGetLicenceCountByCompanyId")]
            public List<ObjectClass> List { get; set; }
        }
        class ObjectClass
        {
            [JsonProperty("roleId")]
            public int roleId { get; set; }
            [JsonProperty("userId")]
            public string userId { get; set; }
            [JsonProperty("quantity")]
            public int quantity { get; set; }

        }
        


    }
}
