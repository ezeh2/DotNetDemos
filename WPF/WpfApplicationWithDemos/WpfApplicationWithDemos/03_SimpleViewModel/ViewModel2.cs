using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApplication2016_06_28
{
    public class ViewModel2 : INotifyPropertyChanged
    {
        public ViewModel2()
        {
            this.List1 = new ObservableCollection<string>();
            this.List1.Add("a");
            this.List1.Add("b");
            this.List1.Add("c");

            LabelText = "bla bla";

            _selectedItemL1 = this.List1[1];
        }

        public ObservableCollection<string> List1 { get; set; }

        private string _selectedItemL1;

        public string SelectedItemL1 { get {
                return _selectedItemL1; 
            } set {
                _selectedItemL1 = value;
                LabelText = _selectedItemL1;
            } }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _LabelText;

        public string LabelText { get {
                return _LabelText;
            } set {
                if (_LabelText != value)
                {
                    _LabelText = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChangedEventArgs pcea = new PropertyChangedEventArgs("LabelText");
                        PropertyChanged(this, pcea);
                    }
                    
                }
            } }
    }
}
