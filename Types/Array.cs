using System.Text;
using System.Text.RegularExpressions;

namespace YamlBuilder.Types
{
    public class SwaggerArray : SwaggerSchema
    {
        public SwaggerSchema ItemsType { get; set; }

        public SwaggerArray(string name, SwaggerSchema items)
        {
            base.Nome = name;
            base.Type = "array";
            this.ItemsType = items;
        }

        public string ToYaml(){
            var result = new StringBuilder();
            result.Append(base.Nome);
            result.Append(":");
            
            result.AppendLine("\t");
            result.Append("type: ");
            result.Append(base.Type);

            var primitive = (this.ItemsType as SwaggerPrimitive).ToYaml();
            result.Append(Regex.Replace(primitive, "^", @"\t"));

            return result.ToString();
        }
    }
}