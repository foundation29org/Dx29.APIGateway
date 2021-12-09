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
        const string ENDPOINT_TA4H = "http://dx29.tasearch/api";
        //const string ENDPOINT_TERM = "http://dx29.termsearch:8080/api";
        const string ENDPOINT_TERM = "http://localhost:8080/api";
        const string ENDPOINT_TERM2 = "http://localhost:8080/api/v1";
#else
        const string ENDPOINT_TA4H = "http://dx29-tasearch/api";
        const string ENDPOINT_TERM = "http://dx29-termsearch:8080/api";
        const string ENDPOINT_TERM2 = "http://dx29-termsearch2:8080/api/v1";
#endif

        public PhenotypeSearchController(IHttpClientFactory clientFactory)
        {
            TA4HServices = new HttpServices(clientFactory, ENDPOINT_TA4H);
            TermServices = new HttpServices(clientFactory, ENDPOINT_TERM);
            TermServices2 = new HttpServices(clientFactory, ENDPOINT_TERM2);
        }

        public HttpServices TA4HServices { get; }
        public HttpServices TermServices { get; }
        public HttpServices TermServices2 { get; }

        [HttpGet("api/[controller]/ta4h")]
        public async Task<IActionResult> TA4HSearch([FromQuery] string text)
        {
            return await TA4HSearchAsync(text);
        }

        [HttpPost("api/[controller]/ta4h")]
        public async Task<IActionResult> POSTTA4HSearch([FromBody] string text)
        {
            return await TA4HSearchAsync(text);
        }

        private async Task<IActionResult> TA4HSearchAsync(string text)
        {
            var request = TA4HServices.POSTRequest("ta4h/search", text);
            (var content, var status) = await TA4HServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        [HttpGet("api/v1/[controller]/terms")]
        public async Task<IActionResult> TermSearch([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await TermSearchAsync(text, lang, rows, fullSearch);
        }

        [HttpGet("api/v1/[controller]/diseases")]
        public async Task<IActionResult> DiseasesSearch([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await DiseasesSearchAsync(text, lang, rows, fullSearch);
        }

        private async Task<IActionResult> TermSearchAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchSymptoms?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<TermSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        private async Task<IActionResult> DiseasesSearchAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchDiseases?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DiseaseSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        [HttpGet("api/v2/[controller]/terms")]
        public async Task<IActionResult> TermSearchImprove([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await TermSearchImproveAsync(text, lang, rows, fullSearch);
        }

        [HttpGet("api/v2/[controller]/diseases")]
        public async Task<IActionResult> DiseasesSearchImprove([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await DiseasesSearchImproveAsync(text, lang, rows, fullSearch);
        }

        private async Task<IActionResult> TermSearchImproveAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchSymptomsImprove?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<TermSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        private async Task<IActionResult> DiseasesSearchImproveAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchDiseasesImprove?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DiseaseSearchResult[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        [HttpGet("api/v3/[controller]/terms")]
        public async Task<IActionResult> TermSearchSource([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await TermSearchSourceAsync(text, lang, rows, fullSearch);
        }

        [HttpGet("api/v3/[controller]/diseases")]
        public async Task<IActionResult> DiseasesSearchSource([FromQuery] string text, string lang = "en", int rows = 10, string fullSearch = "false")
        {
            return await DiseasesSearchSourceAsync(text, lang, rows, fullSearch);
        }

        private async Task<IActionResult> TermSearchSourceAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchSymptomsSource?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<TermSearchResultSource[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

        private async Task<IActionResult> DiseasesSearchSourceAsync(string text, string lang, int rows, string fullSearch)
        {
            var request = TermServices.GETRequest($"searchDiseasesSource?lang={lang}&rows={rows}&q={text}&fullSearch={fullSearch}");
            (var content, var status) = await TermServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<DiseaseSearchResultSource[]>(content);
                return Ok(res);
            }
            throw new ServiceException(content);
        }

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
