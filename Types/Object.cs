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
            result.Append(base.Nome).Append(":");

            result.AppendLine();
            result.Append("\t").Append("properties: ");

            foreach (var item in this.Properties)
            {
                result.AppendLine();
                if (item.Type == "array")
                    result.Append("\t").Append(Regex.Replace((item as SwaggerArray).ToYaml(), "\n", "\n\t"));
                else
                    result.Append("\t").Append(Regex.Replace((item as SwaggerPrimitive).ToYaml(), "\n", "\n\t"));
            }

            return result.ToString();
        }

    }
}