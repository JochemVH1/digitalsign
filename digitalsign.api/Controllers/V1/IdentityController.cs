using digitalsign_api.Controllers.Contracts.V1;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace digitalsign_api.Controllers.V1
{
    [ApiController]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        [HttpGet(template: ApiRoutes.Identity.Token)]
        public async Task<IActionResult> TokenAsync()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5050");
            if (disco.IsError)
            {
                return BadRequest();
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "DigitalSignApi",
                ClientSecret = "secret",

                Scope = "DigitalSign"
            });

            if (tokenResponse.IsError)
            {
                return BadRequest();
            }

            return Ok(tokenResponse.AccessToken);
        }
    }
}
