using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;

namespace Dx29.TASearch.Controllers
{
    [ApiController]
    public class TA4HController : ControllerBase
    {
        public TA4HController(TA4HAnnotationServices annotationServices)
        {
            AnnotationServices = annotationServices;
        }

        public TA4HAnnotationServices AnnotationServices { get; }

        [HttpGet("api/[controller]/search")]
        public async Task<string[]> Search(string text)
        {
            return await DoSearch(text);
        }

        [HttpPost("api/[controller]/search")]
        public async Task<string[]> POSTSearch([FromBody] string text)
        {
            return await DoSearch(text);
        }

        private async Task<string[]> DoSearch(string text)
        {
            var docs = await AnnotationServices.AnnotateTextAsync(text);
            return ExtractHPOs(docs).ToArray();
        }

        private static IEnumerable<string> ExtractHPOs(TA4HAnnotationDocs docs)
        {
            if (docs.Documents != null)
            {
                foreach (var doc in docs.Documents)
                {
                    if (doc.Entities != null)
                    {
                        foreach (var entity in doc.Entities)
                        {
                            if (entity.Links != null)
                            {
                                foreach (var link in entity.Links)
                                {
                                    if (link.DataSource == "HPO")
                                    {
                                        yield return link.Id;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
