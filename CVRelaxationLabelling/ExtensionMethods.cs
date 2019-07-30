using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRelaxationLabelling
{
  public static class ExtensionMethods
  {
    // Courtesy of kprobst of http://stackoverflow.com/questions/5383498/shuffle-rearrange-randomly-a-liststring
    /// <summary>
    /// Extension method on a List to randomly shuffle contents (based on Fisher-Yates shuffle)
    /// </summary>
    /// <typeparam name="T">Generic type parameter</typeparam>
    /// <param name="list">List on which to shuffle elements</param>
    public static void Shuffle<T>(this IList<T> list)
    {
      int n = list.Count;
      Random rnd = new Random();
      while (n > 1)
      {
        int k = (rnd.Next(0, n) % n);
        n--;
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    public static List<Point> GetNeighbourhood(this Point p, int maxX, int maxY)
    {
      List<Point> neighbours = new List<Point>();

      if ((p.X >= 0 && p.X < maxX) && (p.Y >= 0 && p.Y < maxY))
      {
        if (p.X - 1 >= 0 && p.Y - 1 >= 0)
          neighbours.Add(new Point(p.X - 1, p.Y - 1));
        if (p.X - 1 >= 0)
          neighbours.Add(new Point(p.X - 1, p.Y));
        if (p.Y - 1 >= 0)
          neighbours.Add(new Point(p.X, p.Y - 1));

        if (p.X - 1 >= 0 && p.Y + 1 < maxY)
          neighbours.Add(new Point(p.X - 1, p.Y + 1));
        if (p.X + 1 < maxX && p.Y - 1 >= 0)
          neighbours.Add(new Point(p.X + 1, p.Y - 1));

        if (p.X + 1 < maxX)
          neighbours.Add(new Point(p.X + 1, p.Y));
        if (p.Y + 1 < maxY)
          neighbours.Add(new Point(p.X, p.Y + 1));
        if (p.X + 1 < maxX && p.Y + 1 < maxY)
          neighbours.Add(new Point(p.X + 1, p.Y + 1));
      }

      return neighbours;
    }
  }
}
