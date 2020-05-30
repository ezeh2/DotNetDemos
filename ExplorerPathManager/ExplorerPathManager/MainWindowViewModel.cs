using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExplorerPathManager.Annotations;
using Shell32;

namespace ExplorerPathManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Items.Add("C:\\software_downloads\\Since_2019_Nov");
            Items.Add("C:\\");

            OpenInFileExplorerCommand = new DelegateCommand((o) =>
            {
                string path = o as string;
                if (path != null)
                {
                    WindowsExplorerChangeLocation.WithoutPowerShellScript(path);
                }
            });
        }

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public DelegateCommand OpenInFileExplorerCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
