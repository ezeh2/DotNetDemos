using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApplication2016_06_28
{
    public class Company
    {
        private ObservableCollection<Division> _divisions
            = new ObservableCollection<Division>();

        public Company()
        {
            _divisions = new ObservableCollection<Division>();
            _divisions.Add(new Division { Name = "div1" });
            _divisions.Add(new Division { Name = "div2" });
            _divisions[0].Groups.Add(new Group { Name = "grp1" });
            _divisions[0].Groups.Add(new Group { Name = "grp2" });
            _divisions[1].Groups.Add(new Group { Name = "grp3" });
            _divisions[1].Groups.Add(new Group { Name = "grp4" });
        }

        public ObservableCollection<Division> Divisions
        {
            get
            {
                return _divisions;
            }
        }
    }
}
