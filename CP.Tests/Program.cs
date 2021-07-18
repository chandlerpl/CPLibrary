using CP.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Tests
{
    class Program
    {
        static CommandSystem CommandSystem;

        static void Main(string[] args)
        {

            CommandSystem = new CommandSystem();

            while (true)
            {
                Console.Write("Please enter a command: ");
                CommandSystem.CommandInterface(null, Console.ReadLine().Split(' '));
            }
        }
    }
}
