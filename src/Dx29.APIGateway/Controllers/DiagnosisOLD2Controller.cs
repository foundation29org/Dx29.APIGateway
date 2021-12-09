using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;
using System.Collections.Generic;

using System.Reflection;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class DiagnosisOLD2Controller : ControllerBase
    {
#if DEBUG
        const string ENDPOINT_DIAGNOSIS = "http://localhost:5007/api/v1";
#else
        const string ENDPOINT_DIAGNOSIS = "http://dx29-orphanet/api/v1";
#endif

        public DiagnosisOLD2Controller(IHttpClientFactory clientFactory)
        {
            DiagnosisServices = new HttpServices(clientFactory, ENDPOINT_DIAGNOSIS);
        }

        public HttpServices DiagnosisServices { get; }

        [HttpPost("api/v1/[controller]/calculate")]
        public async Task<IActionResult> POSTDiagnosis([FromBody] ScoreInfo data, [FromQuery] string lang = "en")
        {
            return await DiagnosisAsync(data, lang);
        }

        private async Task<IActionResult> DiagnosisAsync(ScoreInfo data, string lang)
        {
            var request = DiagnosisServices.POSTRequest($"Diagnosis/calculate?lang={lang}", data);

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
