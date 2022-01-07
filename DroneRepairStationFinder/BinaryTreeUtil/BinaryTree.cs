using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneRepairStationFinder.BinaryTreeUtil
{
    /// <summary>
    /// Represents a node in a <see cref="BinaryTree{T}"/>.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    public class BinaryTreeNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTree{T}"/>
        /// class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="BinaryTreeNode{T}"/>.</param>
        public BinaryTreeNode(T value)
        {
            Value = value;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTree{T}"/>
        /// class belonging to a binary tree, containing the specified value.
        /// </summary>
        /// <param name="tree">The <see cref="BinaryTree{T}"/> which this node belongs to.</param>
        /// <param name="value">The value to contain in the <see cref="BinaryTreeNode{T}"/>.</param>
        internal BinaryTreeNode(BinaryTree<T> tree, T value)
        {
            Tree = tree;
            Value = value;
        }

        /// <summary>
        /// Gets the <see cref="BinaryTree{T}"/> that the
        /// <see cref="BinaryTreeNode{T}"/> belongs to.
        /// </summary>
        public BinaryTree<T> Tree { get; internal set; }

        /// <summary>
        /// Gets the left child of node in the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTreeNode<T> Left { get; internal set; }

        /// <summary>
        /// Gets the right child of node in the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTreeNode<T> Right { get; internal set; }

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets wether the node is a leaf in a binary tree (has no children).
        /// </summary>
        public bool IsLeaf => Left is null && Right is null;

        public override string ToString() => $"{Value}";
    }

    /// <summary>
    /// Represents a binary tree.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    public class BinaryTree<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTree{T}"/> with an empty root
        /// which has no children.
        /// </summary>
        public BinaryTree()
        {
            Root = new BinaryTreeNode<T>(this, default);
        }

        /// <summary>
        /// Gets the root node of the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTreeNode<T> Root { get; }

        /// <summary>
        /// Gets the leftmost leaf of tree.
        /// </summary>
        public BinaryTreeNode<T> LeftMostLeaf
        {
            get
            {
                BinaryTreeNode<T> node = Root;
                while (!node.IsLeaf)
                {
                    node = node.Left ?? node.Right;
                }
                return node;
            }
        }

        /// <summary>
        /// Gets the rightmost leaf of tree.
        /// </summary>
        public BinaryTreeNode<T> RightMostLeaf
        {
            get
            {
                BinaryTreeNode<T> node = Root;
                while (!node.IsLeaf)
                {
                    node = node.Right ?? node.Left;
                }
                return node;
            }
        }

        /// <summary>
        /// Prints a subtree with specified root to standard output stream.
        /// </summary>
        /// <param name="root">The root of subtree to be printed.</param>
        /// <param name="indent">Initial indentation in console.</param>
        /// <param name="right">Whether the root is right child of another node.</param>
        public void PrintToConsole(BinaryTreeNode<T> root, string indent = "", bool right = true)
        {
            Console.Write(indent);
            if (right)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }

            if (root is null)
            {
                Console.WriteLine();
                return;
            }

            Console.Write(' ');
            Console.WriteLine(root.Value);

            if (!root.IsLeaf)
            {
                PrintToConsole(root.Left, indent, false);
                PrintToConsole(root.Right, indent, true);
            }
        }

        /// <summary>
        /// Performs the specified action on each node of the <see cref="BinaryTree{T}"/>
        /// using pre-order traversal.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{T}"/> delegate
        /// to perform on each element of the <see cref="BinaryTree{T}"/>.
        /// </param>
        /// <param name="root">
        /// The root of traversal.
        /// </param>
        public void PreOrderTraverse(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> root)
        {
            if (root is null)
            {
                return;
            }

            action(root);

            if (root.Left != null)
            {
                PreOrderTraverse(action, root.Left);
            }
            if (root.Right != null)
            {
                PreOrderTraverse(action, root.Right);
            }
        }

        /// <summary>
        /// Performs the specified action on each node of the <see cref="BinaryTree{T}"/>
        /// using in-order traversal.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{T}"/> delegate
        /// to perform on each element of the <see cref="BinaryTree{T}"/>.
        /// </param>
        /// <param name="root">
        /// The root of traversal.
        /// </param>
        public void InOrderTraverse(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> root)
        {
            if (root is null)
            {
                return;
            }

            if (root.Left != null)
            {
                InOrderTraverse(action, root.Left);
            }

            action(root);

            if (root.Right != null)
            {
                InOrderTraverse(action, root.Right);
            }
        }

        /// <summary>
        /// Performs the specified action on each node of the <see cref="BinaryTree{T}"/>
        /// using post-order traversal.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{T}"/> delegate
        /// to perform on each element of the <see cref="BinaryTree{T}"/>.
        /// </param>
        /// <param name="root">
        /// The root of traversal.
        /// </param>
        public void PostOrderTraverse(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> root)
        {
            if (root is null)
            {
                return;
            }

            if (root.Left != null)
            {
                PostOrderTraverse(action, root.Left);
            }
            if (root.Right != null)
            {
                PostOrderTraverse(action, root.Right);
            }

            action(root);
        }

        /// <summary>
        /// Sets the left child of specified node in the <see cref="BinaryTree{T}"/> to a new node.
        /// If the node has an existing left child, it will be detached.
        /// </summary>
        /// <param name="node">The <see cref="BinaryTreeNode{T}"/> to set its left child.</param>
        /// <param name="newNode">The <see cref="BinaryTreeNode{T}"/> to be added to tree.</param>
        /// <param name="detachedNode">The previous left child of <paramref name="node"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="node"/> is not in the current <see cref="BinaryTree{T}"/>
        /// or <paramref name="newNode"/> belongs to another <see cref="BinaryTree{T}"/>.
        /// </exception>
        public void SetLeft(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode, out BinaryTreeNode<T> detachedNode)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (node.Tree != this)
            {
                throw new InvalidOperationException("node is not in the current tree.");
            }
            if (newNode.Tree != null && newNode.Tree != this)
            {
                throw new InvalidOperationException("newNode belongs to another tree.");
            }

            detachedNode = node.Left;
            PostOrderTraverse(n => n.Tree = null, detachedNode);
            node.Left = newNode;
            PostOrderTraverse(n => n.Tree = this, newNode);
        }

        /// <summary>
        /// Creates a new node containing specified value and sets the left child of specified node
        /// in the <see cref="BinaryTree{T}"/> to the new node.
        /// If the node has an existing left child, it will be detached.
        /// </summary>
        /// <param name="node">The <see cref="BinaryTreeNode{T}"/> to set its left child.</param>
        /// <param name="value">The value of new left child of <paramref name="node"/>.</param>
        /// <param name="detachedNode">The previous left child of <paramref name="node"/>.</param>
        /// <returns>The new left child of <paramref name="node"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="node"/> is not in the current <see cref="BinaryTree{T}"/>.
        /// </exception>
        public BinaryTreeNode<T> SetLeft(BinaryTreeNode<T> node, T value, out BinaryTreeNode<T> detachedNode)
        {
            BinaryTreeNode<T> newNode = new BinaryTreeNode<T>(this, value);

            SetLeft(node, newNode, out detachedNode);

            return newNode;
        }

        /// <summary>
        /// Sets the right child of specified node in the <see cref="BinaryTree{T}"/> to a new node.
        /// If the node has an existing right child, it will be detached.
        /// </summary>
        /// <param name="node">The <see cref="BinaryTreeNode{T}"/> to set its right child.</param>
        /// <param name="newNode">The <see cref="BinaryTreeNode{T}"/> to be added to tree.</param>
        /// <param name="detachedNode">The previous right child of <paramref name="node"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="node"/> is not in the current <see cref="BinaryTree{T}"/>
        /// or <paramref name="newNode"/> belongs to another <see cref="BinaryTree{T}"/>.
        /// </exception>
        public void SetRight(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode, out BinaryTreeNode<T> detachedNode)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (node.Tree != this)
            {
                throw new InvalidOperationException("node is not in the current tree.");
            }
            if (newNode.Tree != null && newNode.Tree != this)
            {
                throw new InvalidOperationException("newNode belongs to another tree.");
            }

            detachedNode = node.Right;
            PostOrderTraverse(n => n.Tree = null, detachedNode);
            node.Right = newNode;
            PostOrderTraverse(n => n.Tree = this, newNode);
        }

        /// <summary>
        /// Creates a new node containing specified value and sets the right child of specified node
        /// in the <see cref="BinaryTree{T}"/> to the new node.
        /// If the node has an existing right child, it will be detached.
        /// </summary>
        /// <param name="node">The <see cref="BinaryTreeNode{T}"/> to set its right child.</param>
        /// <param name="value">The value of new right child of <paramref name="node"/>.</param>
        /// <param name="detachedNode">The previous right child of <paramref name="node"/>.</param>
        /// <returns>The new right child of <paramref name="node"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="node"/> is not in the current <see cref="BinaryTree{T}"/>.
        /// </exception>
        public BinaryTreeNode<T> SetRight(BinaryTreeNode<T> node, T value, out BinaryTreeNode<T> detachedNode)
        {
            BinaryTreeNode<T> newNode = new BinaryTreeNode<T>(this, value);

            SetRight(node, newNode, out detachedNode);

            return newNode;
        }

        /// <summary>
        /// Removes all nodes (except root) from tree and sets the value of root to a default value.
        /// </summary>
        public void Clear()
        {
            PostOrderTraverse(n => n.Tree = null, Root.Left);
            PostOrderTraverse(n => n.Tree = null, Root.Right);
            Root.Left = null;
            Root.Right = null;
            Root.Value = default;
        }
    }
}
