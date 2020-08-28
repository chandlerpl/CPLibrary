using CP.Common.Commands;
using CP.Localisation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CP.Tests
{
    class LocalisationTest : CPCommand
    {
        public override bool Init()
        {
            Name = "Localise";
            Desc = "This is a test command!";
            Aliases = new List<string>() { "local" };
            ProperUse = "localise";

            return true;
        }

        public override bool Execute(object obj, List<string> args)
        {
            if (args.Count > 1)
                return false;

            _ = LocaliseManager.Instance;

            Console.Write("Languages currently supported: ");
            Console.WriteLine(string.Join(", ", LocaliseManager.AvailableLanguages));

            Console.WriteLine("name: " + LocaliseManager.Instance.Localise("name"));
            Console.WriteLine("test1: " + LocaliseManager.Instance.Localise("test1"));

            return true;
        }
    }
}
