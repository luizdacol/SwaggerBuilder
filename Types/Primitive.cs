using System.Collections.Generic;
using System.Text;

namespace YamlBuilder.Types
{
    public class SwaggerPrimitive : SwaggerSchema
    {
        public string Format { get; set; }
        public SwaggerPrimitive(string name, string type, string format)
        {
            base.Nome = name;
            base.Type = type;
            this.Format = format;
        }

        public SwaggerPrimitive(string name, string type)
        {
            base.Nome = name;
            base.Type = type;
        }

        public string ToYaml()
        {
            var result = new StringBuilder();
            result.Append(base.Nome).Append(":");

            result.AppendLine("");
            result.Append("\t").Append("type: ").Append(base.Type);

            result.AppendLine();
            result.Append("\t").Append("format: ").Append(this.Format);

            return result.ToString();
        }
    }
}