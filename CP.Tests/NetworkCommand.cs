using CP.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using CP.AILibrary.NeuralNetwork;

namespace CP.Tests
{
    class NetworkCommand : Command
    {
        public override bool Init()
        {
            Name = "Network";
            Desc = "This is a network command!";
            Aliases = new List<string>() { "network" };
            ProperUse = "network";

            return true;
        }

        public override bool Execute(object obj, List<string> args)
        {
            if (args.Count > 1)
                return false;


            int input = 16;
            int output = 4;
            int hiddenLayers = 3;
            int hiddenSize = 96;
            QLearningNetwork network = new QLearningNetwork(4, 2, 1, 24);

            int ival = 0;
            ival += (input + 1) * hiddenSize;
            ival += hiddenLayers * ((hiddenSize + 1) * hiddenSize);
            ival += (hiddenSize + 1) * output;
            Console.WriteLine((ival * 4) / 1000.0 + "kb");

            for(int i = 0; i < 21; i++)
            {
                //Console.WriteLine(i);
                network.Forward(new float[4] { 0.1f, 0.2f, 0.1f, 0.4f });

                network.Remember(false, 0, new float[4] { 0.1f, 0.2f, 0.1f, 0.4f }, new float[4] { 0.1f, 0.2f, 0.1f, 0.4f });
            }

            network.ExperienceReplay(20);

            return true;
        }
    }
}
