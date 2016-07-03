using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2016_06_28
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.label2.Content = "xsxs";
            Binding b = new Binding();
            b.ElementName = "slider";
            b.Path = new PropertyPath("Value");
            this.label2.SetBinding(Label.ContentProperty, b);
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
