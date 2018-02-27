using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YamlBuilder;

namespace YamlBuilder.Types
{
    public class SwaggerObject : SwaggerSchema
    {
        public List<SwaggerSchema> Properties { get; set; }

        public SwaggerObject(string nome, List<SwaggerSchema> properties)
        {
            base.Nome = nome;
            base.Type = "object";
            this.Properties = properties;
        }

        public string ToYaml()
        {
            var result = new StringBuilder();
            result.Append(base.Nome);
            result.Append(":");

            result.AppendLine("\t");
            result.Append("properties: ");

            foreach (var item in this.Properties)
            {
                if (item.Type == "array")
                    result.AppendLine(Regex.Replace((item as SwaggerArray).ToYaml(), "^", @"\t"));
                else
                    result.AppendLine(Regex.Replace((item as SwaggerPrimitive).ToYaml(), "^", @"\t"));
            }

            return result.ToString();
        }

    }
}