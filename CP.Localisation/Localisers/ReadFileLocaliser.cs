using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Localisation.Localisers
{
    public class ReadFileLocaliser : IBaseLocaliser
    {
        public bool CanTranslate(string text_id, string languageId = "")
        {
            return LocaliseManager.Instance.Localisations.ContainsKey(text_id);
        }

        public string Translate(string text_id, string languageId = "")
        {
            return LocaliseManager.Instance.Localisations[text_id].TranslatedText;
        }
    }
}
