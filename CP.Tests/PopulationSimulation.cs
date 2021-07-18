using CP.Common.Commands;
using CP.Common.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Tests
{
    class PopulationSimulation : Command
    {
        public override bool Init()
        {
            Name = "PopulationSimulation";
            Desc = "";
            Aliases = new List<string>() { "PopulationSimulation", "ps" };
            ProperUse = "PopulationSimulation";

            return true;
        }

        public override bool Execute(object obj, List<string> args)
        {
            if (args.Count > 1)
                return false;

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Time.Update();

                Console.WriteLine(Time.DeltaTime + "; " + Time.FrameRate);
            }

            return true;
        }
    }
}