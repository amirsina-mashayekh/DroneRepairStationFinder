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

            BinaryTreeNode<string> R = tree.SetRight(tree.Root, "R", out _);
            Assert.AreSame(tree.SetLeft(L, "LL", out _), tree.Root.Left.Left);
            Assert.AreSame(tree.SetLeft(R, "RL", out _), tree.Root.Right.Left);
            Assert.AreSame(tree.SetRight(R, "RR", out _), tree.Root.Right.Right);
            tree.PrintToConsole(tree.Root);

            BinaryTreeNode<string> R1 = tree.SetRight(tree.Root, "R", out _);
            Console.WriteLine();
            tree.PrintToConsole(tree.Root);
            Assert.AreSame(R1, tree.Root.Right);
            Assert.AreNotSame(R, tree.Root.Right);
            Assert.IsNull(R.Tree);
            Assert.IsNull(R.Left.Tree);
            Assert.IsNull(R.Right.Tree);
            Assert.IsNull(R1.Left);
            Assert.IsNull(R1.Right);

            tree.Clear();
            Console.WriteLine();
            tree.PrintToConsole(tree.Root);
            Assert.IsNull(tree.Root.Left);
            Assert.IsNull(tree.Root.Right);
        }

        [TestMethod()]
        public void TreeHeightAndCountTest()
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            Assert.AreEqual(1, tree.Count);
            tree.Root.Value = "Root";
            BinaryTreeNode<string> L = tree.SetLeft(tree.Root, "L", out _);
            BinaryTreeNode<string> LL = tree.SetLeft(L, "LL", out _);
            BinaryTreeNode<string> LLL = tree.SetLeft(LL, "LLL", out _);

            BinaryTreeNode<string> R = tree.SetRight(tree.Root, "R", out _);
            tree.SetLeft(R, "RL", out _);
            tree.SetRight(R, "RR", out _);
            tree.PrintToConsole(tree.Root);

            Assert.AreEqual(3, tree.Height);
            Assert.AreEqual(7, tree.Count);

            tree.SetRight(LLL, "LLLR", out _);
            Console.WriteLine();
            tree.PrintToConsole(tree.Root);

            Assert.AreEqual(4, tree.Height);
            Assert.AreEqual(8, tree.Count);
        }

        [TestMethod()]
        public void GetParentTest()
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

            Assert.AreSame(tree.Root, tree.GetParentOf(L));
            Assert.AreSame(L, tree.GetParentOf(LL));
            Assert.AreSame(LL, tree.GetParentOf(LLL));
            Assert.AreSame(tree.Root, tree.GetParentOf(R));
            Assert.AreSame(R, tree.GetParentOf(R.Left));
            Assert.ThrowsException<ArgumentNullException>(() => tree.GetParentOf(null));
            Assert.ThrowsException<InvalidOperationException>(() => tree.GetParentOf(new BinaryTreeNode<string>("123")));
        }
    }
}