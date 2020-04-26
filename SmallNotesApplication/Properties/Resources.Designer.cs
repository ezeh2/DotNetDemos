// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.Properties.Resources
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SmallNotesApplication.Properties
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
        if (SmallNotesApplication.Properties.Resources.resourceMan == null)
          SmallNotesApplication.Properties.Resources.resourceMan = new ResourceManager("SmallNotesApplication.Properties.Resources", typeof (SmallNotesApplication.Properties.Resources).Assembly);
        return SmallNotesApplication.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return SmallNotesApplication.Properties.Resources.resourceCulture;
      }
      set
      {
        SmallNotesApplication.Properties.Resources.resourceCulture = value;
      }
    }
  }
}
