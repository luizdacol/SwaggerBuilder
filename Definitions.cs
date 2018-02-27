using System;
using System.Collections.Generic;
using System.Text;

namespace YamlBuilder
{
    public class Definitions
    {
        private List<string> _classTextList;
        private List<ObjectDefinition> _objectDefinitions;
        public Definitions(string classText)
        {
            this._classTextList = new List<string>();
            this._classTextList.Add(classText);

            this._objectDefinitions = new List<ObjectDefinition>();
        }

        public Definitions(string[] classTexts)
        {
            this._classTextList = new List<string>(classTexts);
            this._objectDefinitions = new List<ObjectDefinition>();
        }

        public void Transform()
        {
            foreach (var text in this._classTextList)
            {
                var obj = new ObjectDefinition(text);
                obj.Transform();
                _objectDefinitions.Add(obj);
                //obj.Save();
            }
        }

        public string Save()
        {
            StringBuilder finalText = new StringBuilder("definitions:");
            foreach (var obj in this._objectDefinitions)
                finalText.Append(obj.Yaml);

            var path = AppDomain.CurrentDomain.BaseDirectory + "definitions";
            System.IO.File.WriteAllText(path, finalText.ToString());

            return path;
        }
    }
}