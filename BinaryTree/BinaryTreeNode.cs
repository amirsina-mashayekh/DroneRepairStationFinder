namespace BinaryTreeUtil
{
    /// <summary>
    /// Represents a node in a <see cref="BinaryTree{T}"/>.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    public class BinaryTreeNode<T>
    {
        internal BinaryTree<T> tree;
        internal BinaryTreeNode<T> left;
        internal BinaryTreeNode<T> right;

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
            this.tree = tree;
            Value = value;
        }

        /// <summary>
        /// Gets the <see cref="BinaryTree{T}"/> that the
        /// <see cref="BinaryTreeNode{T}"/> belongs to.
        /// </summary>
        public BinaryTree<T> Tree => tree;

        /// <summary>
        /// Gets the left child of node in the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTreeNode<T> Left => left;

        /// <summary>
        /// Gets the right child of node in the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTreeNode<T> Right => right;

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets wether the node is a leaf in a binary tree (has no children).
        /// </summary>
        public bool IsLeaf => Left is null && Right is null;

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
