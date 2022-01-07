using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BinaryTreeUtil.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        [TestMethod()]
        public void TreeStructureTest()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root.Value = "Root";
            BinaryTreeNode<string> L = tree.SetLeft(tree.Root, "L", out _);
            Assert.AreSame(tree.SetLeft(L, "LL", out _), tree.Root.Left.Left);

            BinaryTreeNode<string> R = tree.SetRight(tree.Root, "R", out _);
            Assert.AreSame(tree.SetLeft(R, "RL", out _), tree.Root.Right.Left);
            Assert.AreSame(tree.SetRight(R, "RR", out _), tree.Root.Right.Right);
            tree.PrintToConsole(tree.Root);

            tree.Clear();
            Console.WriteLine();
            tree.PrintToConsole(tree.Root);
            Assert.IsNull(tree.Root.Left);
            Assert.IsNull(tree.Root.Right);
        }

        [TestMethod()]
        public void TreeHeightTest()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Root.Value = "Root";
            BinaryTreeNode<string> L = tree.SetLeft(tree.Root, "L", out _);
            BinaryTreeNode<string> LL = tree.SetLeft(L, "LL", out _);
            BinaryTreeNode<string> LLL = tree.SetLeft(LL, "LLL", out _);

            BinaryTreeNode<string> R = tree.SetRight(tree.Root, "R", out _);
            tree.SetLeft(R, "RL", out _);
            tree.SetRight(R, "RR", out _);
            tree.PrintToConsole(tree.Root);

            Assert.AreEqual(3, tree.GetHeight(tree.Root));

            tree.SetRight(LLL, "LLLR", out _);
            Console.WriteLine();
            tree.PrintToConsole(tree.Root);

            Assert.AreEqual(4, tree.GetHeight(tree.Root));
        }
    }
}