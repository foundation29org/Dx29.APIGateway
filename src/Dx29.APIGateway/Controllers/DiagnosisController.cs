using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
#if DEBUG
        const string ENDPOINT_DIAGNOSIS = "http://localhost:5700/api/v1";
#else
        const string ENDPOINT_DIAGNOSIS = "http://dx29-bionet/api/v1";
#endif

        public DiagnosisController(IHttpClientFactory clientFactory)
        {
            DiagnosisServices = new HttpServices(clientFactory, ENDPOINT_DIAGNOSIS);
        }

        public HttpServices DiagnosisServices { get; }

        [HttpPost("api/v1/[controller]/calculate")]
        public async Task<IActionResult> POSTDiagnosis([FromBody] ScoreInfo data, [FromQuery] string lang = "en", [FromQuery] int count = 10)
        {
            return await DiagnosisAsync(data, lang, count);
        }

        private async Task<IActionResult> DiagnosisAsync(ScoreInfo data, string lang, int count)
        {
            var request = DiagnosisServices.POSTRequest($"Diagnosis/calculate?lang={lang}&count={count}&source=all", data);

            (var content, var status) = await DiagnosisServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }
    }
}
