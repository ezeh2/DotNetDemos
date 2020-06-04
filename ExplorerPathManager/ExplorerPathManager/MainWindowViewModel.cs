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
using NLog;

namespace ExplorerPathManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string FolderFile;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MainWindowViewModel()
        {
            logger.Debug("MainViewModel, ctor, begin");
            FolderFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "folders.txt");

            try
            {
                OpenInFileExplorerCommand = new DelegateCommand(OpenInFileExplorer);
                AddCurrentFolderOfFileExplorerCommand = new DelegateCommand(AddCurrentFolderOfFileExplorer);

                this.Load(FolderFile);
            }
            catch (Exception e)
            {
                logger.Fatal(e);
                throw;
            }
        }

        private void OpenInFileExplorer(object o)
        {
            logger.Debug("OpenInFileExplorer, begin");
            try
            {
                string path = o as string;
                if (path != null)
                {
                    logger.Debug(path);
                    WindowsExplorerChangeLocation.WithoutPowerShellScript(path);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e);
                throw;
            }
        }

        private void AddCurrentFolderOfFileExplorer(object o)
        {
            logger.Debug("AddCurrentFolderOfFileExplorer, begin");
            try
            {
                List<string> fols = WindowsExplorerChangeLocation.GetFoldersOfFileExplorers();
                foreach (string folder in fols)
                {
                    logger.Debug(folder);
                    if (!Items.Contains(folder))
                    {
                        logger.Debug(folder);
                        Items.Add(folder);
                    }
                }
                Save(FolderFile);
            }
            catch (Exception e)
            {
                logger.Fatal(e);
                throw;
            }
        }

        private void Load(string path)
        {
            logger.Debug("Load, begin");
            logger.Debug(path);

            try
            {
                if (File.Exists(path))
                {
                    string[] folders = File.ReadAllLines(path);
                    foreach (string folder in folders)
                    {
                        logger.Debug(folder);
                        if (!string.IsNullOrEmpty(folder))
                        {
                            logger.Debug(folder);
                            Items.Add(folder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e);
                throw;
            }
        }

        private void Save(string path)
        {
            logger.Debug("Save, begin");
            logger.Debug(path);
            string dt = DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss");

            if (File.Exists(path))
            {
                File.Copy(path, $"{path}_{dt}");
            }

            using (FileStream fs = File.Open(path, FileMode.Create,FileAccess.Write,FileShare.Write))
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
