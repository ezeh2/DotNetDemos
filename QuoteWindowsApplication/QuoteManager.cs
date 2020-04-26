// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.QuoteManager
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuoteWindowsApplication
{
  public class QuoteManager
  {
    private XmlDocument xmlDocument = new XmlDocument();

    public QuoteManager()
    {
      this.xmlDocument.LoadXml(File.ReadAllText("quotes.xml"));
    }

    public string GetQuote(List<KeyValuePair<string, string>> parameters)
    {
      IEnumerable<KeyValuePair<string, string>> source = parameters.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (it => it.Key == "quote_contains"));
      string str = (string) null;
      if (source.Count<KeyValuePair<string, string>>() == 1)
        str = source.First<KeyValuePair<string, string>>().Value;
      XmlNodeList xmlNodeList = (XmlNodeList) null;
      if (!string.IsNullOrEmpty(str))
        xmlNodeList = this.xmlDocument.SelectNodes("/quotes/quote[contains(text,'" + str + "')]");
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(string.Format("{0} quotes found", (object) xmlNodeList.Count));
      stringBuilder.AppendLine();
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        string innerText1 = xmlElement.GetElementsByTagName("text")[0].InnerText;
        string innerText2 = xmlElement.GetElementsByTagName("author")[0].InnerText;
        stringBuilder.AppendLine(innerText1);
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("by " + innerText2);
        stringBuilder.AppendLine("");
      }
      return stringBuilder.ToString();
    }
  }
}
