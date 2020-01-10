using ComProvis.AV.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComProvis.AV.UI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly IUiServices _uiService;
        public UserController(IUiServices uiService)
        {
            _uiService = uiService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string ssoId) => Ok(await _uiService.GetUserAsync(ssoId));
    }
}
