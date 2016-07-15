using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApplication2016_06_28.MasterChild
{
    public class Division
    {
        public Division()
        {
            Groups = new ObservableCollection<Group>();
        }

        public string Name { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
    }

    public class Group
    {
        public Group()
        {
            Persons = new ObservableCollection<Person>();
        }

        public string Name { get;
            set; }
        public ObservableCollection<Person> Persons { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }

}
