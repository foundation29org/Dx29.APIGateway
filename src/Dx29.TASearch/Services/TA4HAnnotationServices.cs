using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Dx29.Data;

namespace Dx29.Services
{
    public class TA4HAnnotationServices
    {
        const string ENDPOINT = "https://f29-ta4health-app.azurewebsites.net";

        public TA4HAnnotationServices(IHttpClientFactory clientFactory)
        {
            HTTPServices = new HttpServices(clientFactory, ENDPOINT);
        }

        public HttpServices HTTPServices { get; }

        public async Task<TA4HAnnotationDocs> AnnotateTextAsync(string text)
        {
            var doc = new
            {
                documents = new object[] { new {
                    id = 1,
                    language = "en",
                    text = text
                } }
            };
            var request = HTTPServices.POSTRequest("text/analytics/v3.2-preview.1/entities/health", doc);
            (var content, var status) = await HTTPServices.SendRequestAsync(request);
            if (status == HttpStatusCode.OK)
            {
                return content.Deserialize<TA4HAnnotationDocs>();
            }
            throw new ServiceException(content);
        }
    }
}
