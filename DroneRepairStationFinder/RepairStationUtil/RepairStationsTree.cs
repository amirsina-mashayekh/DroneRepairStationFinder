using BinaryTreeUtil;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace DroneRepairStationFinder.RepairStationUtil
{
    public static class RepairStationsTree
    {
        /// <summary>
        /// Calculates distance between two <see cref="GeoCoordinate"/>s
        /// based on latitude, longitude and altitude.
        /// </summary>
        /// <param name="p1">The first <see cref="GeoCoordinate"/>.</param>
        /// <param name="p2">The second <see cref="GeoCoordinate"/>.</param>
        /// <returns>The distance between <paramref name="p1"/> and <paramref name="p2"/> in meters.</returns>
        private static double DistanceBetween(GeoCoordinate p1, GeoCoordinate p2)
        {
            return Math.Sqrt(
                    Math.Pow(p1.GetDistanceTo(p2), 2)
                    + Math.Pow(p1.Altitude - p2.Altitude, 2));
        }

        /// <summary>
        /// Returns a sorted list of <see cref="GeoCoordinate"/>s based on distance
        /// from the specified <see cref="GeoCoordinate"/> (near first, far last).
        /// </summary>
        /// <param name="origin">The <see cref="GeoCoordinate"/> to sort list based on distance from it.</param>
        /// <param name="locations">List of <see cref="GeoCoordinate"/>s.</param>
        /// <returns>A new list which contains <paramref name="locations"/> members sorted.</returns>
        private static List<GeoCoordinate> SortByDistance(GeoCoordinate origin, List<GeoCoordinate> locations)
        {
            var result = new List<GeoCoordinate>();

            int cnt = locations.Count;

            for (int j = 0; j < cnt; j++)
            {
                GeoCoordinate nearest = locations[0];

                double min = DistanceBetween(nearest, origin);

                for (int i = 1; i < locations.Count; i++)
                {
                    double d = DistanceBetween(origin, locations[i]);
                    if (d < min)
                    {
                        min = d;
                        nearest = locations[i];
                    }
                }
                result.Add(nearest);
                locations.Remove(nearest);
            }

            return result;
        }

        /// <summary>
        /// Generates a binary tree of repair station locations based on distance from the specified origin.
        /// </summary>
        /// <param name="stations">The list of repair station locations.</param>
        /// <param name="origin">The <see cref="GeoCoordinate"/> which contains location of the origin.</param>
        /// <returns>A <see cref="BinaryTree{T}"/> of repair station locations.</returns>
        public static BinaryTree<GeoCoordinate> GetStationsTree(List<GeoCoordinate> stations, GeoCoordinate origin)
        {
            if (stations.Count < 1)
            {
                return null;
            }

            var tree = new BinaryTree<GeoCoordinate>();
            var queue = new Queue<BinaryTreeNode<GeoCoordinate>>();

            stations = new List<GeoCoordinate>(stations);
            stations = SortByDistance(origin, stations);
            tree.Root.Value = stations[0];
            stations.RemoveAt(0);
            queue.Enqueue(tree.Root);

            while (stations.Count > 0)
            {
                BinaryTreeNode<GeoCoordinate> node = queue.Dequeue();
                stations = SortByDistance(node.Value, stations);
                queue.Enqueue(tree.SetLeft(node, stations[0], out _));
                stations.RemoveAt(0);
                if (stations.Count > 0)
                {
                    queue.Enqueue(tree.SetRight(node, stations[0], out _));
                    stations.RemoveAt(0);
                }
            }

            return tree;
        }

        /// <summary>
        /// Finds the nearest repair station to drone.
        /// </summary>
        /// <param name="stationsTree">The <see cref="BinaryTree{T}"/> containing location of repair stations.</param>
        /// <param name="droneLocation">The <see cref="GeoCoordinate"/> which represents the location of the drone.</param>
        /// <param name="checkedStations">When the method returns, this list contains the nodes which are checked.</param>
        /// <returns>The <see cref="BinaryTreeNode{T}"/> which represents the location of nearest station to the drone.</returns>
        public static BinaryTreeNode<GeoCoordinate> GetNearestStation(
            BinaryTree<GeoCoordinate> stationsTree,
            GeoCoordinate droneLocation,
            out List<BinaryTreeNode<GeoCoordinate>> checkedStations)
        {
            checkedStations = new List<BinaryTreeNode<GeoCoordinate>>
            {
                stationsTree.Root
            };

            if (stationsTree.Root.IsLeaf)
            {
                return stationsTree.Root;
            }

            var n1 = stationsTree.Root.Left;
            var n2 = stationsTree.Root.Right;

            var check = new List<BinaryTreeNode<GeoCoordinate>>
            {
                stationsTree.Root, n1, n2
            };

            while (true)
            {
                if (n1 != null)
                {
                    check.Add(n1.Left);
                    check.Add(n1.Right);
                }
                if (n2 != null)
                {
                    check.Add(n2.Left);
                    check.Add(n2.Right);
                }

                check.RemoveAll(n => n is null || n.Value is null);
                foreach (BinaryTreeNode<GeoCoordinate> l in check)
                {
                    if (!checkedStations.Contains(l))
                    {
                        checkedStations.Add(l);
                    }
                }

                List<GeoCoordinate> sorted =
                    SortByDistance(droneLocation, check.Select(n => n.Value).ToList());
                int cnt = sorted.Count;
                if (n1.Value == sorted[0] && cnt > 1 && n2.Value == sorted[1])
                {
                    return check.Find(n => n.Value == sorted[0]);
                }

                n1 = check.Find(n => n.Value == sorted[0]);
                if (cnt > 1)
                {
                    n2 = check.Find(n => n.Value == sorted[1]);
                }
                check.Clear();
                check.Add(n1);
                check.Add(n2);
            }
        }
    }
}