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
        private const string FolderFile = "folders.txt";

        public MainWindowViewModel()
        {
            OpenInFileExplorerCommand = new DelegateCommand(OpenInFileExplorer);
            AddCurrentFolderOfFileExplorerCommand = new DelegateCommand(AddCurrentFolderOfFileExplorer);

            this.Load(FolderFile);
        }

        private void OpenInFileExplorer(object o)
        {
            string path = o as string;
            if (path != null)
            {
                WindowsExplorerChangeLocation.WithoutPowerShellScript(path);
            }
        }

        private void AddCurrentFolderOfFileExplorer(object o)
        {
            List<string> fols = WindowsExplorerChangeLocation.GetFoldersOfFileExplorers();
            foreach (string folder in fols)
            {
                if (!Items.Contains(folder))
                {
                    Items.Add(folder);
                }
            }
            Save(FolderFile);
        }

        private void Load(string path)
        {
            string[] folders = File.ReadAllLines(path);
            foreach (string folder in folders)
            {
                if (!string.IsNullOrEmpty(folder))
                {
                    Items.Add(folder);
                }
            }
        }

        private void Save(string path)
        {
            string dt = DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss");

            File.Copy(path,$"{path}_{dt}");

            using (FileStream fs = File.Open(path, FileMode.Truncate,FileAccess.Write,FileShare.Write))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                foreach (string item in Items)
                {
                    sw.WriteLine(item);
                }
            }
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
