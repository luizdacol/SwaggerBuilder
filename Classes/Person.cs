using System;
using System.Collections.Generic;

namespace YamlBuilder.Classes
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public double Height { get; set; }
        public DateTime Birthday { get; set; }
        public Adress[] Adresses { get; set; }
    }
}