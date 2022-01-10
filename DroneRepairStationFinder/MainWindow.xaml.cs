using BinaryTreeUtil;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static DroneRepairStationFinder.RepairStationUtil.RepairStationsTree;

namespace DroneRepairStationFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double treeHorizontalDist = 100;
        private const double treeVerticalDist = 40;
        private const double treeLinesWidth = 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private Border DrawStationsTree(
            BinaryTree<GeoCoordinate> tree,
            BinaryTreeNode<GeoCoordinate> root,
            ref double top,
            double left = 0)
        {
            string nodeText = "        ";
            if (root != null)
            {
                GeoCoordinate val = root.Value;
                nodeText = $"{val.Latitude} {val.Longitude} {val.Altitude}";
            }

            var node = new Border
            {
                Tag = root,
                Background = Brushes.SkyBlue,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(left, top, 0, 0),
                CornerRadius = new CornerRadius(5),
                ClipToBounds = true,
                Child = new TextBlock()
                {
                    Text = nodeText,
                    FontSize = 18,
                    Margin = new Thickness(5)
                }
            };
            Panel.SetZIndex(node, 2);
            TreeGrid.Children.Add(node);
            node.UpdateLayout();

            if (root is null)
            {
                return node;
            }

            if (!root.IsLeaf)
            {
                Point st = new Point(node.Margin.Left + node.ActualWidth / 2, node.Margin.Top + node.ActualHeight / 2);

                var lNode = DrawStationsTree(tree, root.Left, ref top, left + treeHorizontalDist);
                lNode.UpdateLayout();
                Point le = new Point(lNode.Margin.Left + 5, lNode.Margin.Top + lNode.ActualHeight / 2);
                TreeGrid.Children.Add(new Line()
                {
                    X1 = st.X,
                    Y1 = st.Y,
                    X2 = le.X,
                    Y2 = le.Y,
                    StrokeThickness = treeLinesWidth,
                    Stroke = Brushes.Black,
                    SnapsToDevicePixels = true
                });

                top += treeVerticalDist;
                var rNode = DrawStationsTree(tree, root.Right, ref top, left + treeHorizontalDist);
                rNode.UpdateLayout();
                Point rm = new Point(st.X, rNode.Margin.Top + rNode.ActualHeight / 2);
                Point re = new Point(rNode.Margin.Left + 5, rm.Y);
                TreeGrid.Children.Add(new Line()
                {
                    X1 = st.X,
                    Y1 = st.Y,
                    X2 = rm.X,
                    Y2 = rm.Y,
                    StrokeThickness = treeLinesWidth,
                    Stroke = Brushes.Black,
                    SnapsToDevicePixels = true
                });
                TreeGrid.Children.Add(new Line()
                {
                    X1 = rm.X,
                    Y1 = rm.Y,
                    X2 = re.X,
                    Y2 = re.Y,
                    StrokeThickness = treeLinesWidth,
                    Stroke = Brushes.Black,
                    SnapsToDevicePixels = true
                });
            }

            return node;
        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}