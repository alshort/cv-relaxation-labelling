using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using CVRelaxationLabelling;

namespace Tests
{
  [TestClass]
  public class ExtensionMethodsTests
  {
    // Corner cases
    [TestMethod]
    public void GetNeighbourhoodTopLeftCornerCase()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(0, 0);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 3);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodTopRightCornerCase()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(1, 0);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 3);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodBottomLeftCornerCase()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(0, 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 3);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodBottomRightCornerCase()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(1, 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 3);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    // Centre case
    [TestMethod]
    public void GetNeighbourhoodCentreCase()
    {
      int maxX = 3, maxY = 3;

      Point point = new Point(1, 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 8);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    // Edge cases
    [TestMethod]
    public void GetNeighbourhoodTopEdgeCase()
    {
      int maxX = 3, maxY = 3;

      Point point = new Point(0, 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 5);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodLeftEdgeCase()
    {
      int maxX = 3, maxY = 3;

      Point point = new Point(1, 0);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 5);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodBottomEdgeCase()
    {
      int maxX = 3, maxY = 3;

      Point point = new Point(2, 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 5);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodRightEdgeCase()
    {
      int maxX = 3, maxY = 3;

      Point point = new Point(1, 2);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 5);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    // Invalid cases
    [TestMethod]
    public void GetNeighbourhoodInvalidCaseOne()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(-1, -1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 0);
      Assert.IsTrue(!neighbours.Contains(point));
    }

    [TestMethod]
    public void GetNeighbourhoodInvalidCaseTwo()
    {
      int maxX = 2, maxY = 2;

      Point point = new Point(maxX + 1, maxY + 1);
      List<Point> neighbours = point.GetNeighbourhood(maxX, maxY);

      Assert.AreEqual(neighbours.Count, 0);
      Assert.IsTrue(!neighbours.Contains(point));
    }
  }
}
