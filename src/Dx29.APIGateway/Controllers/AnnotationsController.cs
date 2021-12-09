using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Services;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class AnnotationsController : ControllerBase
    {
#if DEBUG
        const string ENDPOINT_ANNOTATIONS = "http://dx29-annotations/api";
#else
        const string ENDPOINT_ANNOTATIONS = "http://dx29-annotations/api";
#endif

        public AnnotationsController(IHttpClientFactory clientFactory)
        {
            AnnotationsServices = new HttpServices(clientFactory, ENDPOINT_ANNOTATIONS);
        }

        public HttpServices AnnotationsServices { get; }

        [HttpPost("api/[controller]/process")]
        public async Task<IActionResult> POSTAnalyseAnnotations()
        {
            return await AnalyseAnnotationsAsync(Request.Body);
        }

        private async Task<IActionResult> AnalyseAnnotationsAsync(Stream data)
        {
            var request = AnnotationsServices.POSTRequest($"Annotations/process", data);
            (var content, var status) = await AnnotationsServices.SendRequestAsync(request);
            return ReturnResult(content, status);
        }

        [HttpGet("api/[controller]/status")]
        public async Task<IActionResult> GETStatusAnnotations([FromQuery] string token)
        {
            return await StatusAnnotationsAsync(token);
        }

        private async Task<IActionResult> StatusAnnotationsAsync(string token)
        {
            var request = AnnotationsServices.GETRequest($"Annotations/status?token={token}");
            (var content, var status) = await AnnotationsServices.SendRequestResultJSON(request);
            return ReturnResult(content, status);
        }

        [HttpGet("api/[controller]/results")]
        public async Task<IActionResult> GETResultsAnnotations([FromQuery] string token)
        {
            return await ResultsAnnotationsAsync(token);
        }

        private async Task<IActionResult> ResultsAnnotationsAsync(string token)
        {
            var request = AnnotationsServices.GETRequest($"Annotations/results?token={token}");
            (var content, var status) = await AnnotationsServices.SendRequestResultJSON(request);
            return ReturnResult(content, status);
        }

        private IActionResult ReturnResult(string content, HttpStatusCode status)
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
