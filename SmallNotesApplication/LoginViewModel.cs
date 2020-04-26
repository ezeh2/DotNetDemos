// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.LoginViewModel
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System;
using System.ComponentModel;
using System.Xml;

namespace SmallNotesApplication
{
  internal class LoginViewModel : INotifyPropertyChanged
  {
    private XmlDocument users = new XmlDocument();
    private string _errorText;

    public LoginViewModel()
    {
      this.users.Load("Users.xml");
      this.LoginCommand = new Command((Action) (() =>
      {
        string login = this.Login;
        string password = this.Password;
        XmlNode xmlNode = this.users.SelectSingleNode("//User[@login='" + this.Login + "']");
        if (xmlNode != null)
        {
          string str = xmlNode.Attributes["password"].Value;
          if (password == str)
            new SmallNoteView()
            {
              DataContext = ((object) new SmallNoteViewModel(login))
            }.ShowDialog();
          else
            this.ErrorText = "falsches Passwort für Benutzer " + login + " !";
        }
        else
          this.ErrorText = "Benutzer " + login + " ist unbekannt!";
      }));
    }

    public string Login { get; set; } = "";

    public string Password { get; set; } = "";

    public Command LoginCommand { get; set; }

    public string ErrorText
    {
      get
      {
        return this._errorText;
      }
      set
      {
        this._errorText = value;
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(nameof (ErrorText)));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
