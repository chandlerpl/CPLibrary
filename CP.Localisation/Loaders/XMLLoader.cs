using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CP.Localisation.Loaders
{
    public class XMLLoader : ILoader
    {
        public bool CanOpenFile(string file)
        {
            return file.EndsWith(".xml");
        }
        
        public bool ReadAllFiles()
        {
            foreach(string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Localisation"))
            {
                if(CanOpenFile(file))
                {
                    ReadFile(file);
                }
            }

            return true;
        }

        public void ReadFile(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlNode node = doc.DocumentElement.FirstChild;

            while (true)
            {
                if (node.Attributes != null)
                {
                    var langAttribute = node.Attributes["lang"];
                    if (langAttribute != null)
                    {
                        if (!LocaliseManager.AvailableLanguages.Contains(langAttribute.Value))
                        {
                            LocaliseManager.AvailableLanguages.Add(langAttribute.Value);
                        }

                        if (LocaliseManager.CurrentLanguage == langAttribute.Value)
                        {
                            XmlNode childNode = node.FirstChild;
                            while (true)
                            {
                                XmlNode IDAttribute = null;
                                XmlNode TextAttribute = null;

                                if (childNode.Attributes != null)
                                {
                                    IDAttribute = childNode.Attributes["id"];
                                    TextAttribute = childNode.Attributes["text"];
                                }

                                if (IDAttribute == null)
                                {

                                    throw new InvalidOperationException("Attribute 'id' not found.");
                                }
                                else if (TextAttribute == null)
                                {
                                    throw new InvalidOperationException("Attribute 'text' not found.");
                                }
                                else
                                {
                                    TranslationData data = new TranslationData(IDAttribute.Value, TextAttribute.Value);

                                    LocaliseManager.Instance.Localisations.Add(IDAttribute.Value, data);
                                }

                                if ((childNode = childNode.NextSibling) == null)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Attribute 'lang' not found.");
                    }
                }

                if ((node = node.NextSibling) == null)
                    break;
            }
        }

        public void WriteFile()
        {
            List<TranslationData> list = new List<TranslationData>();
            list.Add(new TranslationData("test", "Test"));
            list.Add(new TranslationData("name", "Chandler"));
            list.Add(new TranslationData("age", "21"));


            using (var writer = new FileStream("Localisation/english.xml", FileMode.Create))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                XmlWriter xmlWriter = XmlWriter.Create(writer, settings);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("localisation");
                xmlWriter.WriteAttributeString("lang", LocaliseManager.CurrentLanguage);

                foreach (TranslationData data in list)
                {
                    xmlWriter.WriteStartElement("localise");
                    xmlWriter.WriteAttributeString("id", data.TranslationId);
                    xmlWriter.WriteAttributeString("text", data.TranslatedText);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }
    }
}
