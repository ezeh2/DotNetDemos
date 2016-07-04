using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApplication2016_06_28
{
    public class ViewModel3 : INotifyPropertyChanged
    {
        public ViewModel3()
        {
            this.List = new ObservableCollection<string>();
            this.List.Add("a");
            this.List.Add("b");
            this.List.Add("c");

            _addItem = new CommandHandler(() => Execute_AddItem(), true);
        }

        public void Execute_AddItem()
        {
            this.List.Add(ItemText);
            ItemText = "";

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ItemText"));
            }
        }

        public string ItemText
        { get; set; }

        public ObservableCollection<string> List { get; set; }

        private ICommand _addItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddItem
        {
            get
            {
                return _addItem;
            }
        }
    }
}
