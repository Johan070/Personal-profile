using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.Xna.Framework;

namespace HotelSimulatie.Utility.Tests
{
    [TestClass()]
    public class SimplePathTests
    {
        SimplePath path;
        [TestInitialize]
        public void TestInit()
        {
            path = new SimplePath();
        }
        [TestMethod()]
        public void AddNull_NoExceptions_Test()
        {
            path.Add(null);
        }

        [TestMethod()]
        public void Route_NoPossibleRoute_Test()
        {
            Node 一 = new Node(new Vector2(1, 1));
            Node 二 = new Node(new Vector2(4, 7));
            path.Add(一);
            path.Add(二);
            Stack<Node> expected = new Stack<Node>();
            Stack<Node> actual = path.Route(一, 二);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        [TestMethod()]
        public void Route_SimpleRoute_Test()
        {

            Node 一 = new Node(new Vector2(1, 1));
            Node 二 = new Node(new Vector2(4, 7));
            一.Edges.Add(二, 6);
            path.Add(一);
            path.Add(二);
            Stack<Node> expected = new Stack<Node>();
            expected.Push(二);
            Stack<Node> actual = path.Route(一, 二);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        [TestMethod()]
        public void Route_MultipleOptions_Test()
        {
            Node 一 = new Node(new Vector2(1, 1));
            Node 二 = new Node(new Vector2(4, 7));
            Node 三 = new Node(new Vector2(3, 7));
            Node 四 = new Node(new Vector2(4, 20));
            一.Edges.Add(二, 6);
            一.Edges.Add(三, 9);
            二.Edges.Add(三, 2);
            二.Edges.Add(四, 7);
            三.Edges.Add(四, 1);
            path.Add(一);
            path.Add(二);
            path.Add(三);
            path.Add(四);
            Stack<Node> expected = new Stack<Node>();
            expected.Push(四);
            expected.Push(三);
            expected.Push(二);
            Stack<Node> actual = path.Route(一, 四);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        [TestMethod()]
        public void Route_AlternateMultipleOptions_Test()
        {
            Node 一 = new Node(new Vector2(1, 1));
            Node 二 = new Node(new Vector2(4, 7));
            Node 三 = new Node(new Vector2(3, 7));
            Node 四 = new Node(new Vector2(4, 20));
            一.Edges.Add(二, 6);
            一.Edges.Add(三, 2);
            二.Edges.Add(三, 2);
            二.Edges.Add(四, 7);
            三.Edges.Add(四, 1);
            path.Add(一);
            path.Add(二);
            path.Add(三);
            path.Add(四);
            Stack<Node> expected = new Stack<Node>();
            expected.Push(四);
            expected.Push(三);
            Stack<Node> actual = path.Route(一, 四);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}