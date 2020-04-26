// Decompiled with JetBrains decompiler
// Type: SmallNotesApplication.NoteManager
// Assembly: SmallNotesApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AC52E6C5-84BE-4851-A1E8-B663E5D2616A
// Assembly location: C:\projects\DotNetDemos2\SmallNotesApplication\SmallNotesApplication\bin\Debug\SmallNotesApplication.exe

using System.IO;
using System.Xml;

namespace SmallNotesApplication
{
  public class NoteManager
  {
    private readonly string filename = "notes.xml";

    public string Load(string login)
    {
      string str = "";
      if (File.Exists(this.filename))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load("notes.xml");
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//" + login);
        if (xmlNode != null)
          str = xmlNode.InnerXml;
      }
      return str;
    }

    public void Save(string login, string text)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (File.Exists(this.filename))
        xmlDocument.Load("notes.xml");
      else
        xmlDocument.LoadXml("<notes></notes>");
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//" + login);
      string str = "<![CDATA[" + text + "]]>";
      if (xmlNode != null)
      {
        xmlNode.InnerXml = text;
      }
      else
      {
        XmlElement element = xmlDocument.CreateElement(login);
        element.InnerXml = text;
        xmlDocument.DocumentElement.AppendChild((XmlNode) element);
      }
      xmlDocument.Save(this.filename);
    }
  }
}
