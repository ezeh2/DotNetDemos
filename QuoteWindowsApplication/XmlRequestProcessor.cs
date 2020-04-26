// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.XmlRequestProcessor
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuoteWindowsApplication
{
  public class XmlRequestProcessor
  {
    private CommandManager commandManager = new CommandManager();
    private QuoteManager quoteManager = new QuoteManager();

    internal string Process(string request)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(request);
      string innerText1 = ((XmlElement) xmlDocument.GetElementsByTagName("Command")[0]).GetElementsByTagName("Name")[0].InnerText;
      XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Parameter");
      List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
      foreach (XmlElement xmlElement in elementsByTagName)
      {
        string innerText2 = xmlElement.Attributes["name"].InnerText;
        string innerText3 = xmlElement.Attributes["value"].InnerText;
        parameters.Add(new KeyValuePair<string, string>(innerText2, innerText3));
      }
      string str;
      switch (innerText1)
      {
        case "Help":
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendLine("available commands:");
          foreach (KeyValuePair<string, string> allCommand in this.GetAllCommands())
            stringBuilder.AppendLine(allCommand.Key);
          str = stringBuilder.ToString();
          break;
        case "FindQuote":
          str = this.quoteManager.GetQuote(parameters);
          break;
        default:
          str = "unknown command: " + innerText1;
          break;
      }
      return str;
    }

    internal List<KeyValuePair<string, string>> GetAllCommands()
    {
      return this.commandManager.Load();
    }
  }
}
