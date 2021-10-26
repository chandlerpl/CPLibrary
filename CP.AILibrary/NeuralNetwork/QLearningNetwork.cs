using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CP.AILibrary.NeuralNetwork
{
    public class Memory 
    {
        public bool done;
        public int action;
        public float[] states;
        public float[] prevStates;
    }

    public class QLearningNetwork
    {
        public double epsilon = 1.0;
        public float gamma = 0.95f;
        public int inputSize;
        public int outputSize;

        public int hiddenSize;
        public int hiddenLayers;

        private Random random = new Random();
        private List<NetworkLayer> layers = new List<NetworkLayer>();
        private Queue<Memory> memory = new Queue<Memory>();
        public QLearningNetwork(int inputSize, int outputSize, int hiddenLayers = 1, int hiddenSize = 24)
        {
            random = new Random();
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            this.hiddenLayers = hiddenLayers;
            this.hiddenSize = hiddenSize;

            layers.Add(new NetworkLayer(inputSize + 1, hiddenSize, ActivationFunction.ReLU));

            for (int i = 0; i < hiddenLayers; ++i)
                layers.Add(new NetworkLayer(hiddenSize + 1, hiddenSize, ActivationFunction.ReLU));

            layers.Add(new NetworkLayer(hiddenSize + 1, outputSize));
        }

        public int SelectAction(float[] states, float[] actionSpace)
        {
            float[] vals = Forward(states);

            if (random.NextDouble() > epsilon)
            {
                float maxVal = float.MinValue;
                int index = 0;

                for(int i = 0; i < vals.Length; ++i)
                {
                    if(vals[i] > maxVal)
                    {
                        maxVal = vals[i];
                        index = i;
                    }
                }

                return index;
            }
            else
            {
                return random.Next(0, actionSpace.Length);
            }
        }

        public float[] Forward(float[] states, bool rememberForBackprop = true)
        {
            float[] vals = (float[])states.Clone();

            int index = 0;

            foreach(NetworkLayer layer in layers)
            {
                vals = layer.Forward(vals, rememberForBackprop);
                index++;
            }

            return vals;
        }

        public void Remember(bool done, int action, float[] states, float[] prevStates)
        {
            memory.Enqueue(new Memory()
            {
                done = done,
                action = action,
                states = states,
                prevStates = prevStates
            });
        }

        public void Backward(float[] calculatedVals, float[] experimentalVals)
        {
            float[] delta = new float[calculatedVals.Length];
            for(int i = 0; i < calculatedVals.Length; ++i)
            {
                delta[i] = calculatedVals[i] - experimentalVals[i];
            }

            for (int j = layers.Count - 1; j >= 0; --j)
            {
                NetworkLayer layer = layers[j];
                delta = layer.Backward(delta);
            }
        }

        public void ExperienceReplay(int updateSize = 20)
        {
            if (memory.Count < updateSize) return;

            for(int i = 0; i < updateSize; ++i)
            {
                Memory mem = memory.Dequeue();

                float[] actionVals = Forward(mem.prevStates, true);
                float[] nextActionVals = Forward(mem.states, false);
                float[] experimentalVals = (float[])actionVals.Clone();

                if(mem.done)
                {
                    experimentalVals[mem.action] = -1;
                } else
                {
                    experimentalVals[mem.action] = 1 + gamma * nextActionVals.Max();
                }

                Backward(actionVals, experimentalVals);
            }
            epsilon = epsilon <= 0.01f ? 0.01f : epsilon * 0.997;

            foreach (NetworkLayer layer in layers)
            {
                layer.learningRate = layer.learningRate <= 0.0001 ? 0.0001f : layer.learningRate * 0.99f;
            }
        }
    }
}
