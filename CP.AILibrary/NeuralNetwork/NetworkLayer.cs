using System;
using System.Collections.Generic;
using System.Text;

namespace CP.AILibrary.NeuralNetwork
{
    public enum ActivationFunction
    {
        Linear,
        ReLU,
    }

    public class NetworkLayer
    {
        int inputSize;
        int outputSize;
        float[,] weights;

        ActivationFunction activationFunction;
        public float learningRate;

        private Random random;

        private float[] backwardStoreIn;
        private float[] backwardStoreOut;
        public NetworkLayer(int inputSize, int outputSize, ActivationFunction activation = ActivationFunction.Linear, float learningRate = 0.001f)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;

            weights = new float[inputSize, outputSize];
            for (int i = 0; i < inputSize; ++i)
            {
                for (int j = 0; j < outputSize; ++j)
                {
                    weights[i, j] = Uniform(-0.5f, 0.5f);
                }
            }

            this.activationFunction = activation;
            this.learningRate = learningRate;
        }

        public float[] Forward(float[] vals, bool rememberForBackprop)
        {
            float[] input = new float[inputSize];
            input[inputSize - 1] = 1;
            for (int y = 0; y < inputSize - 1; ++y)
            {
                input[y] = vals[y];
            }
            float[] unactivated = Dot(input, weights);
            
            float[] output = unactivated;
            if (activationFunction == ActivationFunction.ReLU)
            {
                output = ReLU(output);
            }

            if(rememberForBackprop)
            {
                backwardStoreIn = input;
                backwardStoreOut = (float[])unactivated.Clone();
            }

            return output;
        }

        public float[] Backward(float[] gradientFromAbove)
        {
            float[] adjustedMul = gradientFromAbove;

            if (activationFunction == ActivationFunction.ReLU)
            {
                float[] reluDer = ReLUDerivative(backwardStoreOut);

                for (int i = 0; i < adjustedMul.Length; ++i)
                {
                    adjustedMul[i] = reluDer[i] * gradientFromAbove[i];
                }
            }
            float[,] backwardStore = new float[1, backwardStoreIn.Length];
            for (int i = 0; i < backwardStoreIn.Length; ++i)
            {
                backwardStore[0, i] = backwardStoreIn[i];
            }
            backwardStore = Transpose(backwardStore, 1, inputSize);

            float[,] adjustedMul2 = new float[1, adjustedMul.Length];
            for (int i = 0; i < adjustedMul.Length; ++i)
            {
                adjustedMul2[0, i] = adjustedMul[i];
            }

            float[] delta_i = Dot(adjustedMul, Transpose(weights, inputSize, outputSize));
            float[] delta_j = new float[delta_i.Length - 1];
            for(int i = 0; i < delta_j.Length; ++i)
            {
                delta_j[i] = delta_i[i];
            }

            UpdateWeights(Dot(backwardStore, adjustedMul2));

            return delta_j;
        }

        public void UpdateWeights(float[,] gradient)
        {
            for (int y = 0; y < inputSize; ++y)
            {
                for (int i = 0; i < outputSize; ++i)
                {
                    weights[y, i] -= learningRate * gradient[y, i];
                }
            }
        }

        private float[] ReLU(float[] vals) {
            for(int i = 0; i < vals.Length; ++i)
            {
                vals[i] = vals[i] * (vals[i] > 0 ? 1 : 0);
            }

            return vals;
        }

        private float[] ReLUDerivative(float[] vals)
        {
            for (int i = 0; i < vals.Length; ++i)
            {
                vals[i] = (vals[i] > 0 ? 1 : 0);
            }

            return vals;
        }

        private static float[,] Transpose(float[,] input, int inputSize, int outputSize)
        {
            float[,] newArray = new float[outputSize, inputSize];
            for (int j = 0; j < outputSize; j++)
                for (int r = 0; r < inputSize; r++)
                    newArray[j, r] = input[r, j];

            return newArray;
        }

        private float[,] Dot(float[,] input1, float[,] input2)
        {
            float[,] output = new float[inputSize,  outputSize];

            for (int i = 0; i < inputSize; ++i)
            {
                for (int j = 0; j < outputSize; ++j)
                {
                    output[i, j] = input1[i, 0] * input2[0, j];
                }
            }

            return output;
        }
        
        private static float[] Dot(float[] input, float[,] weights)
        {
            int inSize = weights.GetLength(0);
            int outSize = weights.GetLength(1);

            bool swap = false;
            if(outSize < inSize)
            {
                int t = inSize;
                inSize = outSize;
                outSize = t;
                swap = true;
            }

            float[] output = new float[outSize];

            for (int i = 0; i < outSize; ++i)
            {
                for (int y = 0; y < inSize; ++y)
                {
                    _ = input[y];
                    _ = (swap ? weights[i, y] : weights[y, i]);
                    _ = output[y];
                    output[y] += input[y] * (swap ? weights[i, y] : weights[y, i]);
                }
            }

            return output;
        }
        
        private float Uniform(float a, float b)
        {
            if (random == null)
                random = new Random();

            return (float)(a + (b - a) * random.NextDouble());
        }
    }
}
