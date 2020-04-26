// Decompiled with JetBrains decompiler
// Type: MelodyServerConsoleApp.Program
// Assembly: MelodyServerConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 61A9CBE1-8B7D-48AC-BF17-6ECEA7D68AE0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteConsoleApp\bin\Debug\MelodyServerConsoleApp.exe

using System;
using System.Xml;

namespace MelodyServerConsoleApp
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("BadLight.xml");
      string innerText1 = xmlDocument.DocumentElement.InnerText;
      string innerText2 = xmlDocument.SelectSingleNode("/Names/Name").InnerText;
      string innerXml = xmlDocument.SelectSingleNode("/Names/Name").InnerXml;
      foreach (XmlNode xmlNode in ((XmlElement) xmlDocument.GetElementsByTagName("Names")[0]).GetElementsByTagName("Name"))
      {
        Console.WriteLine(xmlNode.InnerText);
        Console.WriteLine("###");
      }
    }
  }
}
