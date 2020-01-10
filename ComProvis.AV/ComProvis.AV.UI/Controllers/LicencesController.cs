using System.Threading.Tasks;
using ComProvis.AV.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ComProvis.AV.UI.Controllers
{
    [Route("api/[controller]")]
    public class LicencesController : Controller
    {
        readonly IUiServices _uiService;

        public LicencesController(IUiServices uiService)
        {
            _uiService = uiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string ssoId) => Ok(await _uiService.GetLicencesAsync(ssoId));

        [Authorize]
        [HttpGet("{ssoId}/GetCustomerDownloadLink")]
        public async Task<IActionResult> GetCustomerDownloadLink(string ssoId) => Ok(await _uiService.GetCustomerDownloadLinkAsync(ssoId));

        [HttpGet("{ssoId}/InitializeCustomer")]
        public IActionResult InitializeCustomer(string ssoId)
        {
            _uiService.InitializeCustomer(ssoId);
            return Ok();
        }
    }
}
