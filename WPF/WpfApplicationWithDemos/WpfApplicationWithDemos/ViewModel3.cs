using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApplication2016_06_28
{
    public class ViewModel3
    {
        public ViewModel3()
        {
            this.List = new ObservableCollection<string>();
            this.List.Add("a");
            this.List.Add("b");
            this.List.Add("c");
        }

        public ObservableCollection<string> List { get; set; }

        // TODO: add command which is invoked from Button in window3

    }
}
