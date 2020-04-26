// Decompiled with JetBrains decompiler
// Type: MelodyServerConsoleApp.ProduceQuotesXml
// Assembly: MelodyServerConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 61A9CBE1-8B7D-48AC-BF17-6ECEA7D68AE0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteConsoleApp\bin\Debug\MelodyServerConsoleApp.exe

using System.IO;

namespace MelodyServerConsoleApp
{
  public class ProduceQuotesXml
  {
    public static void Main()
    {
      using (StreamWriter text = File.CreateText("quotes.xml"))
      {
        text.WriteLine("<quotes>");
        using (StreamReader streamReader = File.OpenText("quotes_html.xml"))
        {
          string str1 = (string) null;
          string str2 = (string) null;
          string str3;
          do
          {
            str3 = streamReader.ReadLine();
            if (str3 != null)
            {
              if (str3.Contains("<div class=\"quoteText\">"))
                str1 = streamReader.ReadLine();
              else if (str3.Contains("<span class=\"authorOrTitle\">"))
                str2 = streamReader.ReadLine();
              if (str1 != null && str2 != null)
              {
                string str4 = str2.Trim();
                string str5 = str1.Replace("“", "").Replace("”", "").Trim();
                text.WriteLine("<quote><text>" + str5 + "</text><author>" + str4 + "</author></quote>");
                str1 = (string) null;
                str2 = (string) null;
              }
            }
          }
          while (str3 != null);
        }
        text.WriteLine("</quotes>");
      }
    }
  }
}
