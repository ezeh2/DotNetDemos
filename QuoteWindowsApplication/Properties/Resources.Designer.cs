// Decompiled with JetBrains decompiler
// Type: QuoteWindowsApplication.Properties.Resources
// Assembly: QuoteWindowsApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74E3F68D-BF41-4091-B909-DB5854CDCAC0
// Assembly location: C:\projects\DotNetDemos2\QuoteSolution\QuoteWindowsApplication\bin\Debug\QuoteWindowsApplication.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace QuoteWindowsApplication.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (QuoteWindowsApplication.Properties.Resources.resourceMan == null)
          QuoteWindowsApplication.Properties.Resources.resourceMan = new ResourceManager("QuoteWindowsApplication.Properties.Resources", typeof (QuoteWindowsApplication.Properties.Resources).Assembly);
        return QuoteWindowsApplication.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return QuoteWindowsApplication.Properties.Resources.resourceCulture;
      }
      set
      {
        QuoteWindowsApplication.Properties.Resources.resourceCulture = value;
      }
    }
  }
}
