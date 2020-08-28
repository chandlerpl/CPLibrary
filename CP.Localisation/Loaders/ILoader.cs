using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Localisation.Loaders
{
    public interface ILoader
    {
        bool CanOpenFile(string file);

        void ReadFile(string file);

        bool ReadAllFiles();
    }
}
