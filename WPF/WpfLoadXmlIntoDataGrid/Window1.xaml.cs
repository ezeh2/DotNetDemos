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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly Window1ViewModel _viewModel;

        public Window1()
        {
            InitializeComponent();

            // http://stackoverflow.com/questions/7498278/wpf-binding-listbox-master-detail

            _viewModel = new Window1ViewModel();
            _viewModel.MyPeeps = new People();
            _viewModel.MyPeeps.Add(new Person("Fred"));
            _viewModel.MyPeeps.Add(new Person("Jack"));
            _viewModel.MyPeeps.Add(new Person("Jill"));
            DataContext = _viewModel;
        }
    }
}
