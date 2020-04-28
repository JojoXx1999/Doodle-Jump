using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class NeuralNetwork
{
    NeuralNetwork net;

    public float bestFitness = 0f;
    public float fitness = 0f;

    public int[] layers;
    public float[][] neurons;
    public float[][][] weights;

    int trainingIndex = 1;

    private System.Random random;

    private int counter;

    public NeuralNetwork(int[] layers)
    {
        this.layers = new int[layers.Length];

        for (int i = 0; i < layers.Length; i++)
            this.layers[i] = layers[i];

        random = new System.Random();

        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork copyNetwork)
    {
        this.layers = new int[copyNetwork.layers.Length];

        for (int i = 0; i < copyNetwork.layers.Length; i++)
            this.layers[i] = copyNetwork.layers[i];

        InitNeurons();
        CopyWeights(copyNetwork.weights);
    }

    private void CopyWeights(float[][][] copyWeights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = (float)copyWeights[i][j][k];
                }
            }
        }
    }

    private void InitNeurons()
    {
        List<float[]> neuronList = new List<float[]>();

        for (int i = 0; i < layers.Length; i++)
        {
            neuronList.Add(new float[layers[i]]);
        }

        neurons = neuronList.ToArray();
    }

    private void InitWeights()
    {
        List<float[][]> weightList = new List<float[][]>();

        for (int i = 1; i < layers.Length; i++)
        {
            List<float[]> layerWeightList = new List<float[]>();

            int neuronsInPreviousLayer = layers[i - 1];

            for (int j = 0; j < neurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];

                for (int k = 0; k < neuronsInPreviousLayer; k++)
                {
                    neuronWeights[k] = Random.Range(-0.5f, 0.5f);
                }

                layerWeightList.Add(neuronWeights);
            }
            weightList.Add(layerWeightList.ToArray());
        }
        weights = weightList.ToArray();
    }

    public float[] Output(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            neurons[0][i] = inputs[i];
        }

        for (int i = 1; i < inputs.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                float value = 0.5f;

                for (int k = 0; k < neurons[i-1].Length; k++)
                {
                    value += weights[i - 1][j][k] * neurons[i - 1][k];
                }

                neurons[i][j] = (float)System.Math.Tanh(value);
            }
        }
        return neurons[neurons.Length - 1];
    }





    public void Save(string path)
    {
        File.Create(path).Close();
        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(fitness);

        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    writer.WriteLine(weights[i][j][k]);
                }
            }
        }
        writer.Close();
    }

    public void Load(string path)
    {
        TextReader textReader = new StreamReader(path);
        int NumberOfLines = (int)new FileInfo(path).Length;
        string[] ListLines = new string[NumberOfLines];
        int index = 1;

        for (int i = 1; i < NumberOfLines; i++)
        {
            ListLines[i] = textReader.ReadLine();
        }
        textReader.Close();

        if (new FileInfo(path).Length > 0)
        {
            bestFitness = float.Parse(ListLines[index]);
            index++;

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = float.Parse(ListLines[index]);
                        index++;
                    }
                }
            }
        }
    }
}
