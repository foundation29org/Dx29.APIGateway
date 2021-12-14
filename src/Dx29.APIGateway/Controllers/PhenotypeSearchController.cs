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
    public class PhenotypeSearchController : ControllerBase
    {
#if DEBUG
        
        const string ENDPOINT_TERM2 = "http://localhost:8080/api/v1";
#else
        const string ENDPOINT_TERM2 = "http://dx29-termsearch2:8080/api/v1";
#endif

        public PhenotypeSearchController(IHttpClientFactory clientFactory)
        {
            TermServices2 = new HttpServices(clientFactory, ENDPOINT_TERM2);
        }

        public HttpServices TermServices2 { get; }

        [HttpGet("api/v4/[controller]/terms")]
        public async Task<IActionResult> TermSearch2([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            var request = TermServices2.GETRequest($"SearchSymptoms?lang={lang}&rows={rows}&q={text}");
            (var content, var status) = await TermServices2.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<TermSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        [HttpGet("api/v4/[controller]/diseases")]
        public async Task<IActionResult> DiseasesSearch2([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            var request = TermServices2.GETRequest($"SearchDiseases?lang={lang}&rows={rows}&q={text}");
            (var content, var status) = await TermServices2.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DiseaseSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }
    }
}
