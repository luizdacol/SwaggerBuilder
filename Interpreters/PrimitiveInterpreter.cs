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
                primitives.Add(
                    new SwaggerPrimitive(properties[i].Groups[2].Value, properties[i].Groups[1].Value)
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
    }
}