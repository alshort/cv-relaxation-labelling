using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CVRelaxationLabelling
{
  internal class Program
  {
    private const int MAX_X = 5, MAX_Y = 5;
    private const int ITERATIONS = 2;

    // Label set {e, ne}
    private static List<string> labelSet = new List<string>() { "e", "ne" };

    private static List<Point> objectSet;

    private static List<double[,]> probs;

    private static int[,] currentCompatibilityCoefficients;
    private static List<int[,]> compatibilityCoefficients = new List<int[,]>()
    {
      new int[2, 2]
      {
        { 1, 1 },
        { 1, 1 },
      },
      new int[2, 2]
      {
        { 2, 1 },
        { 1, 1 },
      },
      new int[2, 2]
      {
        { 2, 1 },
        { 1, 1 },
      },
    };

    private static List<double[,]> initialProbabilities = new List<double[,]>()
    {
      // Configuration A
      new double[MAX_X, MAX_Y]
      {
        { 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.0, 0.1, 0.1, 0.1, 0.0 },
        { 0.0, 0.1, 0.9, 0.0, 0.0 },
        { 0.0, 0.1, 0.0, 0.0, 0.0 },
        { 0.0, 0.0, 0.0, 0.0, 0.0 }
      },
      // Configuration B
      new double[MAX_X, MAX_Y]
      {
        { 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.0, 0.0, 1.0, 0.0, 0.0 },
        { 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.0, 0.0, 0.0, 0.0, 0.0 }
      },
      // Configuration C
      new double[MAX_X, MAX_Y]
      {
        { 0.0, 0.0, 0.0, 0.0, 0.0 },
        { 0.0, 0.1, 0.1, 0.1, 0.0 },
        { 0.0, 0.1, 1.0, 0.1, 0.0 },
        { 0.0, 0.1, 0.1, 0.1, 0.0 },
        { 0.0, 0.0, 0.0, 0.0, 0.0 }
      }
    };

    private static void Main(string[] args)
    {
      // Create a total collection of objects
      objectSet = new List<Point>();

      for (int i = 0; i < MAX_X; i++)
        for (int j = 0; j < MAX_Y; j++)
          objectSet.Add(new Point(i, j));


      // Repeat for each configuration
      for (int i = 0; i < 3; i++)
      {
        // Compatiblity coefficients
        currentCompatibilityCoefficients = compatibilityCoefficients.ElementAt(i);

        // Probabilities per label set
        probs = new List<double[,]>()
        {
          initialProbabilities[i],                           // e
          InitialProbabilityNotEdge(initialProbabilities[i]) // ne
        };

        Console.WriteLine("Configuration {0}", (char)(65 + i));

        foreach (double[,] prob in probs)
          PrintDoubleArray(prob, 2);

        Console.WriteLine("Goes to\n");

        // Iterate ITERATIONS no of times
        for (int j = 0; j < ITERATIONS; j++)
          probs = IterateLabelling(probs);

        foreach (double[,] prob in probs)
          PrintDoubleArray(prob, 10);

        Console.WriteLine("--------------------------");
      }

      Console.Read();
    }

    private static double q(Point i, Point j, string labelL)
    {
      double q = 0;
      int labelIndex = labelSet.IndexOf(labelL);

      for (int k = 0; k < labelSet.Count; k++)
      {
        int r = currentCompatibilityCoefficients[labelIndex, k];
        double Pjk = probs.ElementAt(k)[j.Y, j.X];

        q += r * Pjk;
      }

      return q;
    }

    private static double Q(Point i, string labelL)
    {
      double Q = 0;

      for (int x = 0; x < objectSet.Count; x++)
      {
        Point j = objectSet.ElementAt(x);

        int Cij = CijWeight(i, j);
        double qj = q(i, j, labelL);

        Q += Cij * qj;
      }

      return Q;
    }

    private static List<double[,]> IterateLabelling(List<double[,]> oldProbs)
    {
      List<double[,]> newProbs = new List<double[,]>();
      foreach (string label in labelSet)
        newProbs.Add(new double[MAX_X, MAX_Y]);

      foreach (Point point in objectSet)
      {
        foreach (string label in labelSet)
        {
          int labelIndex = labelSet.IndexOf(label);

          double PQs = 0;

          for (int k = 0; k < labelSet.Count; k++)
            PQs += oldProbs.ElementAt(k)[point.Y, point.X] * Q(point, labelSet.ElementAt(k));

          newProbs.ElementAt(labelIndex)[point.Y, point.X] = (oldProbs.ElementAt(labelIndex)[point.Y, point.X] * Q(point, label)) / PQs;
        }
      }

      return newProbs;
    }

    private static int CijWeight(Point i, Point j)
    {
      int[,] Cij = new int[2, 1] {
        { 1 }, // if pixel i is adjacent, but not equal, to pixel j
        { 0 }  // otherwise
      };

      if (i.GetNeighbourhood(MAX_X, MAX_Y).Contains(j) && !i.Equals(j))
        return Cij[0, 0];
      else
        return Cij[1, 0];
    }

    private static void PrintDoubleArray(double[,] arr, int accuracy)
    {
      for (int i = 0; i < arr.GetLength(0); i++)
      {
        for (int j = 0; j < arr.GetLength(1); j++)
          Console.Write("{0, -" + (accuracy + 3) + "}", Math.Round(arr[i, j], accuracy));
        Console.WriteLine();
      }

      Console.WriteLine();
    }

    private static double[,] InitialProbabilityNotEdge(double[,] initProbs)
    {
      double[,] notEdgeProbs = new double[initProbs.GetLength(0), initProbs.GetLength(1)];

      for (int i = 0; i < initProbs.GetLength(0); i++)
        for (int j = 0; j < initProbs.GetLength(1); j++)
          notEdgeProbs[i, j] = 1 - initProbs[i, j];

      return notEdgeProbs;
    }
  }
}