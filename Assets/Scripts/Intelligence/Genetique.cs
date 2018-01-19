using System;
using System.IO;

public class Genetique
{
    private int tailleADN;
    private float indiceMutation;
    private int taillePopulation;
    private Squad[] population;
    private int[] bestIndices;
    private int cursor;
    private int gen;

    public Genetique(int p_taillePopulation, int p_tailleADN, float p_indiceMutation)
    {
        gen = 0;
        cursor = 0;
        indiceMutation = p_indiceMutation;
        bestIndices = new int[p_taillePopulation / 2];

        population = new Squad[p_taillePopulation];
        for (int p = 0; p < population.Length; p++)
        {
            population[p] = new Squad(p_tailleADN);
        }
    }

    public void makeNextGeneration()
    {
        gen++;

        // Select N/2 meilleurs
        for (int i = 0; i < bestIndices.Length; i++)
            bestIndices[i] = -1;
        int bestIndice;
        for (int bi = 0; bi < bestIndices.Length; bi++)
        {
            bestIndice = 0;
            for (int p = 0; p < population.Length; p++)
            {
                if (!Array.Exists(bestIndices, e => e == p) && population[p].score >= population[bestIndice].score)
                    bestIndice = p;
            }
            bestIndices[bi] = bestIndice;
        }

        // Write the bests in a file to see how it evolves
        writeBests(bestIndices);

        // Make them reproduce (mutate & crossover => new one)

        for (int p = 0; p < population.Length; p++)
        {
            if (!Array.Exists(bestIndices, e => e == p))
            {
                Squad male = population[bestIndices[UnityEngine.Random.Range(0, bestIndices.Length)]];
                Squad female = population[bestIndices[UnityEngine.Random.Range(0, bestIndices.Length)]];
                population[p] = Squad.crossover(male, female);
                population[p].mutate(indiceMutation);
            }
        }

        cursor = 0;
    }

    public Squad nextSquad()
    {
        if (cursor >= population.Length)
        {
            return null;
        }
        return population[cursor++];
    }

    public bool hasNext()
    {
        return cursor < population.Length;
    }

    public void writeBests(int[] bests)
    {
        using (StreamWriter sw = new StreamWriter(File.Open("bests.txt", FileMode.Append)))
        {
            sw.WriteLine("Generation : " + gen);
            foreach (int b in bests)
            {
                sw.Write("pop " + b + " ; " + population[b]);
            }
        }
    }
}
