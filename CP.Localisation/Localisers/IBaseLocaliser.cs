using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Localisation.Localisers
{
    public interface IBaseLocaliser
    {
        bool CanTranslate(string text_id, string languageId = "");

        string Translate(string text_id, string languageId = "");
    }
}
