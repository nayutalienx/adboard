using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.API.Options
{
    public class SecurityRequirementsDocumentFilterCustom : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>()
        {
            new Dictionary<string, IEnumerable<string>>()
            {
                { "Bearer", new string[]{ } },
                { "Basic", new string[]{ } },
            }
        };
        }
    }
}
