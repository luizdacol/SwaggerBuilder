using System.Collections.Generic;
using System.Text.RegularExpressions;
using YamlBuilder.Types;

namespace YamlBuilder.Interpreters
{
    public class ArrayInterpreter
    {
        private string _text;

        public ArrayInterpreter(string text)
        {
            this._text = text;
        }

        public List<SwaggerArray> Interpreter()
        {
            var properties = GetProperties();
            var arrays = new List<SwaggerArray>();
            for (int i = 0; i < properties.Count; i++)
            {
                var primitive = new SwaggerPrimitive("items", properties[i].Groups[1].Value);
                arrays.Add(
                    new SwaggerArray(properties[i].Groups[2].Value, primitive)
                );
            }

            return arrays;
        }

        private MatchCollection GetProperties()
        {
            // return Regex.Matches(this._text, @"public ([A-Za-z0-9]+)\[\] ([A-Za-z0-9]+)", RegexOptions.Singleline);
            return Regex.Matches(this._text, @"public (?:List)<([A-Za-z0-9]+)> ([A-Za-z0-9]+)", RegexOptions.Singleline);
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