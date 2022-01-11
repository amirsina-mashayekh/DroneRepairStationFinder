﻿using BinaryTreeUtil;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private const double treeHorizontalDist = 150;
        private const double treeVerticalDist = 40;
        private const double treeLinesWidth = 2;

        private static List<GeoCoordinate> stations;
        private static GeoCoordinate origin = new GeoCoordinate(0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();
        }

        public void GenerateAndDrawTree()
        {
            if (TreeGrid is null)
            {
                return;
            }
            TreeGrid.Children.Clear();
            var tree = GetStationsTree(stations, origin);
            double top = 0;
            DrawStationsTree(tree, tree.Root, ref top);
        }

        private Border DrawStationsTree(
            BinaryTree<GeoCoordinate> tree,
            BinaryTreeNode<GeoCoordinate> root,
            ref double top,
            double left = 0)
        {
            string nodeText = "        ";
            string nodeToolTip = null;
            if (root != null)
            {
                GeoCoordinate val = root.Value;
                nodeText = $"{val.Latitude:F2} {val.Longitude:F2} {val.Altitude:F2}";
                nodeToolTip = $"{val.Latitude}\n{val.Longitude}\n{val.Altitude}";
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
                ToolTip = nodeToolTip,
                Child = new TextBlock()
                {
                    Text = nodeText,
                    FontSize = 16,
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

        private void RemoveStationButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in TreeGrid.Children)
            {
                if (element is Border node && node.Tag is BinaryTreeNode<GeoCoordinate>)
                {
                    node.Cursor = Cursors.Hand;
                    node.MouseLeftButtonDown += Node_MouseLeftButtonDown;
                }
            }

            MessageBox.Show(
                "Please click on the station which you want to remove.",
                "Remove Repair Station",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Node_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var node = (sender as Border).Tag as BinaryTreeNode<GeoCoordinate>;

            MessageBoxResult res = MessageBox.Show(
                "Are you sure you want to delete repair station at:\n" +
                $"{node.Value.Latitude} {node.Value.Longitude} {node.Value.Altitude}?",
                "Remove Repair Station",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);

            if (res == MessageBoxResult.Yes)
            {
                stations.Remove(node.Value);
                GenerateAndDrawTree();
            }
            else
            {
                foreach (UIElement element in TreeGrid.Children)
                {
                    if (element is Border nodeBorder && nodeBorder.Tag is BinaryTreeNode<GeoCoordinate>)
                    {
                        nodeBorder.Cursor = Cursors.Arrow;
                        nodeBorder.MouseLeftButtonDown -= Node_MouseLeftButtonDown;
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "XML file|*.xml",
                Multiselect = false,
                Title = "Load Repair Stations Data"
            };

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    stations = ReadStationsFromFile(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while reading file:\n" + ex.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                GenerateAndDrawTree();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "XML file|*.xml",
                Title = "Save Repair Stations Data",
                FileName = "Stations"
            };

            if ((bool)sfd.ShowDialog())
            {
                try
                {
                    WriteStationsToFile(sfd.FileName, stations);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while saving file:\n" + ex.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LocationSystem_Checked(object sender, RoutedEventArgs e)
        {
            XYZ = (sender as RadioButton) == XYZSystem;
            GenerateAndDrawTree();
        }
    }
}