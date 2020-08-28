using CP.Common.Utilities;
using CP.Localisation.Loaders;
using CP.Localisation.Localisers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CP.Localisation
{
    public class LocaliseManager : SingletonBase<LocaliseManager>
    {
        public static string CurrentLanguage = "en";

        public static List<string> AvailableLanguages = new List<string>();

        public static ILoader Loader = new XMLLoader();

        public List<IBaseLocaliser> Localisers = new List<IBaseLocaliser>();
        public Dictionary<string, TranslationData> Localisations = new Dictionary<string, TranslationData>();

        protected override void Load()
        {
            Loader.ReadAllFiles();
            
            Localisers =  ClassLoader.Load<IBaseLocaliser>();
        }

        public string Localise(string textID, string languageID = "")
        {
            if (string.IsNullOrEmpty(textID))
            {
                throw new InvalidOperationException("Invalid textID, cannot be null or empty.");
            }

            foreach(IBaseLocaliser localise in Localisers)
            {
                if(localise.CanTranslate(textID))
                {
                    return localise.Translate(textID);
                }
            }

            return textID;
        }
    }
}
