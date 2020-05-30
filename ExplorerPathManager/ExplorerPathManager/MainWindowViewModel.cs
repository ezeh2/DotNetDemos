using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            string[] folders = File.ReadAllLines("folders.txt");
            foreach (string folder in folders)
            {
                if (!string.IsNullOrEmpty(folder))
                {
                    Items.Add(folder);
                }
            }

            OpenInFileExplorerCommand = new DelegateCommand((o) =>
            {
                string path = o as string;
                if (path != null)
                {
                    WindowsExplorerChangeLocation.WithoutPowerShellScript(path);
                }
            });

            AddCurrentFolderOfFileExplorerCommand = new DelegateCommand((o) =>
                {
                    List<string> fols = WindowsExplorerChangeLocation.GetFoldersOfFileExplorers();
                    foreach (string folder in fols)
                    {
                        if (!Items.Contains(folder))
                        {
                            Items.Add(folder);
                        }
                    }
                });
        }

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

        public DelegateCommand AddCurrentFolderOfFileExplorerCommand { get; set; }
        public DelegateCommand OpenInFileExplorerCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
