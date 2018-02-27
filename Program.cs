using System;
using System.Collections.Generic;
using System.IO;

namespace YamlBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"A:\Projetos\YamlBuilder\Classes");
            List<string> texts = new List<string>();
            foreach (var file in files)
                texts.Add(File.ReadAllText(file));

            // // var classText = File.ReadAllText(@"A:\Projetos\YamlBuilder\Classes\Person.cs");
            // var definitions = new Definitions(texts.ToArray());
            // definitions.Transform();
            // definitions.Save();

            foreach (var item in texts)
            {
                var o = new YamlBuilder.Interpreters.ObjectInterpreter(item);
                var obj = o.Interpreter();

                Console.WriteLine(obj.ToYaml());
            }

            // var definitions = new Definitions(texts.ToArray());
            // definitions.Transform();
            // definitions.Save();
        }
    }
}
