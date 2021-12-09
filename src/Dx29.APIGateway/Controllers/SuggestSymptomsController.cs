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
    public class SuggestSymptomsController : ControllerBase
    {
        #if DEBUG
            const string ENDPOINT_SUGGESTSYMPTOMS = "http://dx29.suggestsymptoms:8080/api";
        #else
            const string ENDPOINT_SUGGESTSYMPTOMS = "http://dx29-suggestsymptoms:8080/api";
        #endif

        public SuggestSymptomsController(IHttpClientFactory clientFactory)
        {
            SuggestSymptomsServices = new HttpServices(clientFactory, ENDPOINT_SUGGESTSYMPTOMS);
        }

        public HttpServices SuggestSymptomsServices { get; }

        [HttpPost("api/v1/[controller]/getSuggestions")]
        public async Task<IActionResult> POSTSuggestSymptoms([FromBody] List<string> data)
        {
            return await SuggestSymptomsAsync(data);
        }

        private async Task<IActionResult> SuggestSymptomsAsync(List<string> data)
        {
            var request = SuggestSymptomsServices.POSTRequest($"suggestSymptoms",data);
            
            (var content, var status) = await SuggestSymptomsServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

    }
}
