using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace WpfApplication1
{
    public class DogPackages
    {
        public DogPackages()
        {
            OldDogPackages = new ObservableCollection<DogPackage>();
            NewDogPackages = new ObservableCollection<DogPackage>();
            OldNewPackages = new ObservableCollection<OldNewPackage>();
            UnmatchedOldDogPackages = new ObservableCollection<DogPackage>();
        }

        private DogPackage _selectOldSccmPackage;

        public ObservableCollection<DogPackage> OldDogPackages { get; set; }
        public ObservableCollection<DogPackage> NewDogPackages { get; set; }
        public ObservableCollection<OldNewPackage> OldNewPackages { get; set; }
        public ObservableCollection<DogPackage> UnmatchedOldDogPackages { get; set; }

        public void Load()
        {
            Load(@"06_LoadXmlIntoDataGrid\c.xml", OldDogPackages);
            Load(@"06_LoadXmlIntoDataGrid\d.xml", NewDogPackages);

            foreach(DogPackage oldSccmPackage in OldDogPackages)
            {
                var found = NewDogPackages.Where(it => Match(it.PackageTitle) == Match(oldSccmPackage.PackageTitle));
                if (found.Count() == 1)
                {
                    OldNewPackage oldNewPackageId = new OldNewPackage();
                    oldNewPackageId.OldPackage = oldSccmPackage;
                    oldNewPackageId.NewPackage = found.First();
                    OldNewPackages.Add(oldNewPackageId);
                }
                else
                {
                    UnmatchedOldDogPackages.Add(oldSccmPackage);
                }
            }

            Dump(OldNewPackages);
            Dump(UnmatchedOldDogPackages);
        }

        private void Dump(ObservableCollection<OldNewPackage> OldNewPackages)
        {
            using (StreamWriter sw = new StreamWriter("OldNewPackages.csv"))
            {
                sw.WriteLine("OldPackage.PackageID;OldPackage.PackageTitle;NewPackage.PackageID;NewPackage.PackageTitle;");
                foreach(OldNewPackage oldNewPackage in OldNewPackages)
                {
                    sw.WriteLine("{0};{1};{2};{3};"
                        , oldNewPackage.OldPackage.PackageID, oldNewPackage.OldPackage.PackageTitle
                        , oldNewPackage.NewPackage.PackageID, oldNewPackage.NewPackage.PackageTitle);
                }
                sw.Flush();
            }
        }

        private void Dump(ObservableCollection<DogPackage> UnmatchedOldSccmPackages)
        {
            using (StreamWriter sw = new StreamWriter("UnmatchedOldSccmPackages.csv"))
            {
                sw.WriteLine("UnmatchedOldSccmPackage.PackageID;UnmatchedOldSccmPackage.PackageTitle;");
                foreach (DogPackage unmatchedOldSccmPackages in UnmatchedOldSccmPackages)
                {
                    sw.WriteLine("{0};{1};"
                        , unmatchedOldSccmPackages.PackageID, unmatchedOldSccmPackages.PackageTitle);
                }
                sw.Flush();
            }
        }

        private bool Match(string oldPackageTitle, string newPackageTitle)
        {
            return Match(oldPackageTitle) == Match(newPackageTitle);
        }

        private string Match(string input)
        {
            string ret = input;
            if (ret != null)
            {
                ret = ret.ToLower();
                ret = ret.Replace(" ","");
                ret = ret.Replace("(", "");
                ret = ret.Replace(")", "");
                ret = ret.Replace("_", "");
                ret = ret.Replace("@", "");
            }

            return ret;
        }

        private static void Load(string path, ObservableCollection<DogPackage> target)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(path);
            XmlNodeList xnl = xd.SelectNodes("//CatalogItems");
            foreach (XmlNode xn in xnl)
            {
                DogPackage sp = new DogPackage();

                sp.PackageID = xn.SelectSingleNode("PackageID").InnerText;
                sp.PackageTitle = xn.SelectSingleNode("PackageTitle").InnerText;
                sp.PackageDesc = xn.SelectSingleNode("PackageDesc").InnerText;
                target.Add(sp);
            }
        }

        public DogPackage SelectOldSccmPackage
        {
            get
            {
                return _selectOldSccmPackage;
            }
            set
            {
                _selectOldSccmPackage = value;
                NewDogPackages.RemoveAt(0);
            }
        }
    }
}
