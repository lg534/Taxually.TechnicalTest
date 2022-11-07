using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.DTOModel;
using Taxually.TechnicalTest.VatNumber.Selector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            // can be requested/created as part of some dependency injection framework
            var registerSelector = new VatNumberRegisterSelector();

            if (!await registerSelector.RegisterVatNumberAsync(request))
                throw new Exception("Country not supported");

            return Ok();
        }
    }
}
