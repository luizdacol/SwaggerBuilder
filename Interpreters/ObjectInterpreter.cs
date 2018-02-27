using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YamlBuilder.Types;

namespace YamlBuilder.Interpreters
{
    public class ObjectInterpreter
    {
        private string _text;

        public ObjectInterpreter(string text)
        {
            this._text = text;
        }

        public SwaggerObject Interpreter()
        {
            var name = GetName();
            List<SwaggerSchema> properties = GetProperties(); ;

            return new SwaggerObject(name, properties);
        }

        private List<SwaggerSchema> GetProperties()
        {
            List<SwaggerSchema> properties = new List<SwaggerSchema>();

            var primitive = new PrimitiveInterpreter(this._text);
            var primitives = primitive.Interpreter();

            var array = new ArrayInterpreter(this._text);
            var arrays = array.Interpreter();

            properties.AddRange(primitives);
            properties.AddRange(arrays);

            return properties;
        }

        private string GetName()
        {
            var grupos = Regex.Match(this._text, @"public class ([A-Za-z0-9]+)[^\{]*\{", RegexOptions.Singleline);
            return grupos.Groups[1].Value;
        }
    }
}