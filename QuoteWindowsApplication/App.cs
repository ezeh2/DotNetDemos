// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.App
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace QuoteWindowsApplication
{
  public class App : Application
  {
    public void InitializeComponent()
    {
      this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
    }

    [STAThread]
    public static void Main()
    {
        try
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);
        }
    }
  }
}
