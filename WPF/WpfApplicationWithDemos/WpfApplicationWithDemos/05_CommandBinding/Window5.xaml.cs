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

namespace WpfApplication2016_06_28._05_CommandBinding
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        bool enabled;

        public Window5()
        {
            InitializeComponent();

            CommandBinding cb1 = new CommandBinding();
            cb1.Command = ApplicationCommands.Copy;
            cb1.Executed += Cb1_Executed;
            cb1.CanExecute += Cb1_CanExecute;
            this.CommandBindings.Add(cb1);
        }

        private void Cb1_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            object o = e.Source;

            // without this button b3 is always disabled
            e.CanExecute = enabled;
        }

        private void Cb1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            object o = e.Source;

            MessageBox.Show("CommandBinding_Executed");
        }

        /// <summary>
        /// declared in Windows5.xaml in <Window.CommandBindings>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button b = (Button)e.Source;

            MessageBox.Show("CommandBinding_Executed " + b.Content);

            enabled = !enabled;
        }
    }
}
