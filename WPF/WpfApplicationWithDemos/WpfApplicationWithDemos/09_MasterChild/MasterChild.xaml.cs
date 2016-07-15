using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2016_06_28.MasterChild
{
    /// <summary>
    /// Interaction logic for MasterChild.xaml
    /// </summary>
    public partial class MasterChild : Window
    {
        public MasterChild()
        {
            InitializeComponent();

            ObservableCollection<Division> divs = new ObservableCollection<Division>();
            divs.Add(new Division { Name = "div1"});
            divs.Add(new Division { Name = "div2" });
            divs[0].Groups.Add(new Group { Name = "grp1" });
            divs[0].Groups.Add(new Group { Name = "grp2" });
            divs[1].Groups.Add(new Group { Name = "grp3" });
            divs[1].Groups.Add(new Group { Name = "grp4" });

            this.DataContext = divs;
        }
    }
}
