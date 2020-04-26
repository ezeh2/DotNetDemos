// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.Command
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System;
using System.Windows.Input;

namespace SmallNotesApplication
{
  public class Command : ICommand
  {
    private Action action;

    public Command(Action action)
    {
      this.action = action;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      this.action();
    }
  }
}
