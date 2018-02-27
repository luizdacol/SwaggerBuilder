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
            this.Format = DefineFormat(type);
        }

        private string DefineFormat(string type)
        {
            return MapFormats.GetValueOrDefault(type);
        }

        private Dictionary<string, string> MapFormats = new Dictionary<string, string>()
        {
            {"DateTime", "date-time"},
            {"int", "int32"},
            {"long", "int64"}
        };

        public string ToYaml()
        {
            var result = new StringBuilder();
            result.Append(base.Nome);
            result.Append(":");

            result.AppendLine("\t");
            result.Append("type: ");
            result.Append(base.Type);

            result.AppendLine("\t");
            result.Append("format: ");
            result.Append(this.Format);

            return result.ToString();
        }
    }
}