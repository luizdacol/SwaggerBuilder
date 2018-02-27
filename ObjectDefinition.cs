using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace YamlBuilder
{
    public class ObjectDefinition
    {
        private string _cSharp;
        private string _yaml;
        public string Yaml => this._yaml;
        public string CSharp => this._cSharp;
        public ObjectDefinition(string classText)
        {
            this._cSharp = classText;
            this._yaml = classText;
        }

        public void Transform()
        {
            this.RemoveNamespace();
            this.RemoveUsing();
            this.CreateObject();
            this.CreateProperties();
            this.ConvertTypes();
            this.RemoveBrackets();
            this.RemoveBlankLines();
        }

        public string Save(){
            var fileName = Regex.Match(this._cSharp, "public class (.+)").Groups[1].Value;
            var path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            System.IO.File.WriteAllText(path, this._yaml);

            return path;
        }

        private void CreateObject()
        {
            string pattern = ".+public class (.+)";
            string replacement = "\t$1:\n\t\ttype: object\n\t\tproperties:\n";
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
        }

        private void CreateProperties()
        {
            string pattern = ".+public ([a-zA-Z]+) ([a-zA-Z]+).+";
            string replacement = "\t\t\t$2:\n\t\t\t\ttype: $1\n";
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
        }

        private void ConvertTypes()
        {
            foreach (var mapType in this.MapTypes)
            {
                string pattern = mapType.Key;
                string replacement = mapType.Value;

                var format = this.MapFormats.GetValueOrDefault(mapType.Key);
                if(format != null)
                    replacement += "\n\t\t\t\tformat: " + format;

                this._yaml = Regex.Replace(this._yaml, pattern, replacement);
            }

        }

        private void RemoveNamespace(){
            string pattern = "namespace.+";
            string replacement = string.Empty;
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
        }

        private void RemoveUsing()
        {
            string pattern = "using.+;";
            string replacement = string.Empty;
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
        }
        public void RemoveBrackets(){
            string pattern = "{|}";
            string replacement = string.Empty;
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
        }

        private void RemoveBlankLines()
        {
            string pattern = "[\n ]*\n[\n ]*";
            string replacement = "\n";
            this._yaml = Regex.Replace(this._yaml, pattern, replacement);
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

        private Dictionary<string, string> MapFormats = new Dictionary<string, string>()
        {
            {"DateTime", "date-time"}
        };

        public void Print()
        {
            Console.WriteLine(this._yaml);
        }
    }
}
