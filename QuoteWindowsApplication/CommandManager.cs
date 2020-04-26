// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.CommandManager
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System.Collections.Generic;
using System.IO;

namespace QuoteWindowsApplication
{
  public class CommandManager
  {
    internal List<KeyValuePair<string, string>> Load()
    {
      List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>();
      foreach (string file in Directory.GetFiles("Commands"))
      {
        Path.GetFileNameWithoutExtension(file);
        string str = File.ReadAllText(file);
        keyValuePairList.Add(new KeyValuePair<string, string>(file, str));
      }
      return keyValuePairList;
    }
  }
}
