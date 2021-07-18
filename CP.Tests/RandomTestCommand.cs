using CP.Common.Commands;
using CP.Common.Random;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.Tests
{
    class RandomTestCommand : Command
    {
        public override bool Init()
        {
            Name = "RandomTest";
            Desc = "This is a test for the Random function command!";
            Aliases = new List<string>() { "randomtest" };
            ProperUse = "randomtest";

            return true;
        }

        public override bool Execute(object obj, List<string> args)
        {
            int samples = args.Count > 1 ? int.Parse(args[1]) : 10;

            double[] times = new double[samples];

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("Cores: " + Environment.ProcessorCount);
            Console.WriteLine("64bit:" + Environment.Is64BitProcess + Environment.NewLine);

            for (int s = 0; s < samples; s++)
            {
                watch.Restart();
                RunTest();
                watch.Stop();
                times[s] = watch.Elapsed.TotalMilliseconds;
            }
            Console.WriteLine("Samples: " + samples + " - avg => " + times.Average() + " | best => " + times.Min() + " | worst => " + times.Max());

            return true;
        }

        public void RunTest()
        {
            RandomHash random = new RandomHash();
            random.Next(5, 10, 1, 5, 24);
        }
    }
}
