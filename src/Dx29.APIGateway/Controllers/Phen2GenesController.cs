using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;
using System.Collections;
using System.Collections.Generic;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class Phen2GenesController : ControllerBase
    {
        #if DEBUG
            const string ENDPOINT_PHEN2GENES = "https://f29bio.northeurope.cloudapp.azure.com/api";
        #else
            const string ENDPOINT_PHEN2GENES = "https://f29bio.northeurope.cloudapp.azure.com/api";
        #endif

        public Phen2GenesController(IHttpClientFactory clientFactory)
        {
            Phen2GenesServices = new HttpServices(clientFactory, ENDPOINT_PHEN2GENES);
        }

        public HttpServices Phen2GenesServices { get; }

        [HttpPost("api/v1/[controller]/calculate")]
        public async Task<IActionResult> POSTCalculePhen2Genes([FromBody] List<string> data)
        {
            return await CalculePhen2GenesAsync(data);
        }

        private async Task<IActionResult> CalculePhen2GenesAsync(List<string> data)
        {
            var request = Phen2GenesServices.POSTRequest($"Phen2Gene/calc",data);
            
            (var content, var status) = await Phen2GenesServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

    }
}
