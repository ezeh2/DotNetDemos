// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.Properties.Settings
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SmallNotesApplication.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
