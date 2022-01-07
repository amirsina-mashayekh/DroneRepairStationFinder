using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneRepairStationFinder.BinaryTreeUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneRepairStationFinder.BinaryTreeUtil.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        [TestMethod()]
        public void TreeStructureTest()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root.Value = "Root";
            BinaryTreeNode<string> RL = tree.SetLeft(tree.Root, "L", out _);
            tree.SetLeft(RL, "LL", out _);

            BinaryTreeNode<string> RR = tree.SetRight(tree.Root, "R", out _);
            tree.SetLeft(RR, "RL", out _);
            tree.SetRight(RR, "RR", out _);
            tree.PrintToConsole(tree.Root);
            tree.Clear();
            Console.WriteLine("***");
            tree.PrintToConsole(tree.Root);
        }
    }
}