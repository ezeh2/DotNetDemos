// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.MainWindow
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace QuoteWindowsApplication
{
  public partial class MainWindow : Window
  {
    private XmlRequestProcessor xmlRequestProcessor = new XmlRequestProcessor();

    public MainWindow()
    {
      this.InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);
      this.comboBox.ItemsSource = (IEnumerable) this.xmlRequestProcessor.GetAllCommands();
    }

    private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.requestTextBox.Text = ((KeyValuePair<string, string>) this.comboBox.SelectedItem).Value;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      string text = this.requestTextBox.Text;
      if (!string.IsNullOrEmpty(text))
      {
                bool dontShowCommandInResponse = this.checkBox.IsChecked.GetValueOrDefault(false);
            this.responseTextBox.Text = this.xmlRequestProcessor.Process(text, dontShowCommandInResponse);
      }
    }
  }
}
