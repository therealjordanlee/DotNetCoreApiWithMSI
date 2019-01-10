using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MsiDemo.Services;

namespace MsiDemo.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IAzureKeyvaultService _azureKeyvaultService;

        public HomeController(IAzureKeyvaultService azureKeyvaultService)
        {
            _azureKeyvaultService = azureKeyvaultService;
        }

        public async Task<IActionResult> GetHome()
        {
            var result = await _azureKeyvaultService.GetSecret();

            return Ok(result);
        }
    }
}
