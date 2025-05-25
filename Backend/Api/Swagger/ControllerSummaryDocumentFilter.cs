using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml.Linq;


namespace Api.Swagger;

public class ControllerSummaryDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        if (!File.Exists(xmlPath)) return;

        XDocument xmlDoc = XDocument.Load(xmlPath);

        foreach (OpenApiTag tag in swaggerDoc.Tags)
        {
            string controllerName = tag.Name.Replace("Controller", string.Empty);

            string? comment = xmlDoc.Descendants("member")
                .FirstOrDefault(x => x.Attribute("name")?.Value == $"T:Api.Controllers.{controllerName}Controller")
                ?.Element("summary")?.Value.Trim();

            if (!string.IsNullOrWhiteSpace(comment))
            {
                tag.Description = comment;
            }
        }
    }
}
