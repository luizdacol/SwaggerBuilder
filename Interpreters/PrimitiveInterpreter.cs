using System.Collections.Generic;
using System.Text.RegularExpressions;
using YamlBuilder.Types;

namespace YamlBuilder.Interpreters
{
    public class PrimitiveInterpreter
    {
        private string _text;
        public PrimitiveInterpreter(string text)
        {
            this._text = text;
        }

        public List<SwaggerPrimitive> Interpreter()
        {
            var properties = GetProperties();
            var primitives = new List<SwaggerPrimitive>();
            for (int i = 0; i < properties.Count; i++)
            {
                var typeConverted = this.MapTypes.GetValueOrDefault(properties[i].Groups[1].Value) ?? properties[i].Groups[1].Value;
                var formatConverted = this.MapFormats.GetValueOrDefault(properties[i].Groups[1].Value) ?? properties[i].Groups[1].Value;
                primitives.Add(
                    new SwaggerPrimitive(properties[i].Groups[2].Value, typeConverted, formatConverted)
                );
            }

            return primitives;
        }

        private MatchCollection GetProperties()
        {
            return Regex.Matches(this._text, @"public (?!class)([A-Za-z0-9]+) ([A-Za-z0-9]+)", RegexOptions.Singleline);
        }
        private Dictionary<string, string> MapTypes = new Dictionary<string, string>()
        {
            {"int", "integer"},
            {"float", "number"},
            {"decimal", "number"},
            {"double", "number"},
            {"bool", "boolean"},
            {"DateTime", "string"}
        };

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
    }
}