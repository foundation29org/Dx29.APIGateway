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
    public class DiagnosisOLDController : ControllerBase
    {
#if DEBUG
        const string ENDPOINT_DIAGNOSIS = "http://localhost:8106/api/v1";
#else
            const string ENDPOINT_DIAGNOSIS = "http://dx29-diagnosis:8080/api/v1";

#endif

        public DiagnosisOLDController(IHttpClientFactory clientFactory)
        {
            DiagnosisServices = new HttpServices(clientFactory, ENDPOINT_DIAGNOSIS);
        }

        public HttpServices DiagnosisServices { get; }

        [HttpPost("api/v0.1/Diagnosis/calculate")]
        public async Task<IActionResult> POSTDiagnosis([FromBody] ScoreInfo data, [FromQuery] string filterMatches = "false", [FromQuery] string filterConditions = "false")
        {
            return await DiagnosisAsync(filterMatches, filterConditions, data);
        }

        private async Task<IActionResult> DiagnosisAsync(string filterMatches, string filterConditions, ScoreInfo data)
        {
            var request = DiagnosisServices.POSTRequest($"calculate?filterConditions={filterConditions}&filterMatches={filterMatches}", data);

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
