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

namespace WpfApplication2016_06_28
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();

            ViewModel3 viewModel3 = new ViewModel3();
            this.DataContext = viewModel3;

            // CTRL-Q also executes a Find-comand
            ApplicationCommands.Find.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            // execute find-command right now
            // ApplicationCommands.Find.Execute(null, this);

            CommandBinding cb = new CommandBinding();
            cb.Command = ApplicationCommands.Find;
            cb.Executed += Cb_Executed;
            this.CommandBindings.Add(cb);
        }

        private void Cb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("sxxs");
        }
    }
}
