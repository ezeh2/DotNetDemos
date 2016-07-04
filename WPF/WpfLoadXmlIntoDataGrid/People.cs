using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class Person
    {
        public string Name { get; set; }

        public Person(string newName)
        {
            Name = newName;
        }
    }

    public class People : List<Person>
    {
    }
}
