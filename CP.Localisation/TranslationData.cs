using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CP.Localisation
{
    [XmlType("localise")]
    public class TranslationData
    {
        public TranslationData() { }

        public TranslationData(string translationId, string translatedText)
        {
            TranslationId = translationId;
            TranslatedText = translatedText;
        }

        public bool UpdateTranslation(string translationId, string translatedText)
        {
            if (!TranslationId.Equals(translationId))
                return false;

            TranslationId = translationId;
            TranslatedText = translatedText;

            return true;
        }

        [XmlAttribute("id")]
        public string TranslationId { get; set; }

        [XmlAttribute("text")]
        public string TranslatedText { get; set; }
    }
}
