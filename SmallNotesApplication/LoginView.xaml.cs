// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.LoginView
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SmallNotesApplication
{
  public partial class LoginView : Window
  {
    public LoginView()
    {
      this.InitializeComponent();
      this.webBrowser.NavigateToString("haha <strong>fett</strong>");
    }
  }
}
