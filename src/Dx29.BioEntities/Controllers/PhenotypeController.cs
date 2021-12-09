using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Dx29.Data;
using Dx29.Services;

namespace Dx29.BioEntities.Controllers
{
    [ApiController]
    public class PhenotypeController : ControllerBase
    {
        public PhenotypeController(EntityServices entityServices)
        {
            EntityServices = entityServices;
        }

        public EntityServices EntityServices { get; }

        [HttpGet("api/[controller]/describe")]
        public IDictionary<string, IList<Term>> Describe([FromQuery] string[] id)
        {
            return DescribeTerms(id);
        }

        [HttpPost("api/[controller]/describe")]
        public IDictionary<string, IList<Term>> POSTDescribe([FromBody] string[] id)
        {
            return DescribeTerms(id);
        }

        private IDictionary<string, IList<Term>> DescribeTerms(string[] ids)
        {
            var terms = new Dictionary<string, IList<Term>>();
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    terms[id] = EntityServices.GetHpoTerms(id);
                }
            }
            return terms;
        }
    }
}
