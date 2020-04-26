// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.SmallNoteViewModel
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System;
using System.ComponentModel;

namespace SmallNotesApplication
{
  public class SmallNoteViewModel : INotifyPropertyChanged
  {
    private NoteManager noteManager = new NoteManager();
    private string _showText = "";
    private string _editText = "";
    private string login;
    private int _zIndex;

    public SmallNoteViewModel(string login)
    {
      SmallNoteViewModel smallNoteViewModel = this;
      this.login = login;
      this.ZIndex = 1;
      this.ShowText = this.noteManager.Load(login);
      this.EditCommand = new Command((Action) (() =>
      {
        smallNoteViewModel.ShowText = smallNoteViewModel.noteManager.Load(login);
        smallNoteViewModel.EditText = smallNoteViewModel.ShowText;
        smallNoteViewModel.ZIndex = 0;
      }));
      this.SaveCommand = new Command((Action) (() =>
      {
        smallNoteViewModel.noteManager.Save(login, smallNoteViewModel.EditText);
        smallNoteViewModel.ShowText = smallNoteViewModel.noteManager.Load(login);
        smallNoteViewModel.ZIndex = 1;
      }));
    }

    public string ShowText
    {
      get
      {
        return this._showText;
      }
      set
      {
        this._showText = value;
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(nameof (ShowText)));
      }
    }

    public string EditText
    {
      get
      {
        return this._editText;
      }
      set
      {
        this._editText = value;
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(nameof (EditText)));
      }
    }

    public int ZIndex
    {
      get
      {
        return this._zIndex;
      }
      set
      {
        this._zIndex = value;
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(nameof (ZIndex)));
      }
    }

    public Command EditCommand { get; set; }

    public Command SaveCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
