// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.App
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace SmallNotesApplication
{
  public class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      while (true)
      {
        try
        {
          LoginView loginView = new LoginView();
          loginView.DataContext = (object) new LoginViewModel();
          loginView.ShowDialog();
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main()
    {
      new App().Run();
    }
  }
}
