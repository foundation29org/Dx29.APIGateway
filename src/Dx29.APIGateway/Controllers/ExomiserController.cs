using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Services;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class ExomiserController : ControllerBase
    {
        #if DEBUG
            const string ENDPOINT_EXOMISER = "https://f29svc.northeurope.cloudapp.azure.com/api/exomiser";
        #else
            const string ENDPOINT_EXOMISER = "https://f29svc.northeurope.cloudapp.azure.com/api/exomiser";
        #endif

        public ExomiserController(IHttpClientFactory clientFactory)
        {
            ExomiserServices = new HttpServices(clientFactory, ENDPOINT_EXOMISER);
        }

        public HttpServices ExomiserServices { get; }

        [HttpPost("api/[controller]/analyse")]
        public async Task<IActionResult> POSTAnalyseExomiser([FromBody] object data)
        {
            return await AnalyseExomiserAsync(data);
        }

        private async Task<IActionResult> AnalyseExomiserAsync(object data)
        {
            var request = ExomiserServices.POSTRequest($"analyse",data);
            (var content, var status) = await ExomiserServices.SendRequestResultJSON(request);
            return returnResult(content, status);
        }

        [HttpGet("api/[controller]/status")]
        public async Task<IActionResult> GETStatusExomiser([FromQuery] string token)
        {
            return await StatusExomiserAsync(token);
        }

        private async Task<IActionResult> StatusExomiserAsync(string token)
        {
            var request = ExomiserServices.GETRequest($"status?token={token}");
            (var content, var status) = await ExomiserServices.SendRequestResultJSON(request);
            return returnResult(content, status);
        }

        [HttpGet("api/[controller]/results")]
        public async Task<IActionResult> GETResultsExomiser([FromQuery] string token)
        {
            return await ResultsExomiserAsync(token);
        }

        private async Task<IActionResult> ResultsExomiserAsync(string token)
        {
            var request = ExomiserServices.GETRequest($"results?token={token}");
            (var content, var status) = await ExomiserServices.SendRequestResultJSON(request);
            return returnResult(content, status);
        }

        [HttpPost("api/[controller]/statusDescription")]
        public async Task<IActionResult> POSTStatusDescriptionExomiser([FromBody] object data, [FromQuery] string lang = "en")
        {
            return await StatusDescriptionExomiserAsync(lang, data);
        }

        private async Task<IActionResult> StatusDescriptionExomiserAsync(string lang, object data)
        {
            var request = ExomiserServices.POSTRequest($"StatusDescription?lan={lang}",data);
            (var content, var status) = await ExomiserServices.SendRequestResultJSON(request);
            return returnResult(content, status);
        }

        [HttpPost("api/[controller]/errorDescription")]
        public async Task<IActionResult> POSTErrorDescriptionExomiser([FromBody] object data,[FromQuery] string lang = "en")
        {
            return await ErrorDescriptionExomiserAsync(lang, data);
        }

        private async Task<IActionResult> ErrorDescriptionExomiserAsync(string lang, object data)
        {
            var request = ExomiserServices.POSTRequest($"ErrorDescription?lan={lang}",data);
            (var content, var status) = await ExomiserServices.SendRequestResultJSON(request);
            return returnResult(content, status);
        }

        private IActionResult returnResult(string content, HttpStatusCode status)
        {
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(content);
            if (status == HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else if (status == HttpStatusCode.NotFound)
            {
                return NotFound(res);
            }
            else if (status == HttpStatusCode.BadRequest)
            {
                return BadRequest(res);
            }
            else if (status == HttpStatusCode.Unauthorized)
            {
                return Unauthorized(res);
            }
            throw new ServiceException(content);
        }

    }
}
