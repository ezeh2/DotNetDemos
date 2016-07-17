using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            Company company = new Company();

            this.DataContext = company;
        }
    }
}
