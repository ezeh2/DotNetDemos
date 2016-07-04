﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class Window1ViewModel : INotifyPropertyChanged
    {
        private People _myPeeps;
        private Person _selectedPerson;

        public event PropertyChangedEventHandler PropertyChanged;

        public People MyPeeps
        {
            get { return _myPeeps; }
            set
            {
                if (_myPeeps == value)
                {
                    return;
                }
                _myPeeps = value;
                RaisePropertyChanged("MyPeeps");
            }
        }

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson == value)
                {
                    return;
                }
                _selectedPerson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
