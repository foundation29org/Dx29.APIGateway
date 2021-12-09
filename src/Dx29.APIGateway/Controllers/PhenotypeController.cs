using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class PhenotypeController : ControllerBase
    {
#if DEBUG
        const string ENDPOINT = "http://dx29.bioentities/api";
#else
        const string ENDPOINT = "http://dx29-bioentities/api";
#endif

        public PhenotypeController(IHttpClientFactory clientFactory)
        {
            HTTPServices = new HttpServices(clientFactory, ENDPOINT);
        }

        public HttpServices HTTPServices { get; }

        [HttpGet("api/[controller]/describe")]
        public async Task<IActionResult> Describe([FromQuery] string[] id)
        {
            return await DescribeTerms(id);
        }

        [HttpPost("api/[controller]/describe")]
        public async Task<IActionResult> POSTDescribe([FromBody] string[] id)
        {
            return await DescribeTerms(id);
        }

        private async Task<IActionResult> DescribeTerms(string[] id)
        {
            var request = HTTPServices.POSTRequest("phenotype/describe", id);
            (var content, var status) = await HTTPServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, Term[]>>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }
    }
}
