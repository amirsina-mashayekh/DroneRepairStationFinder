using BinaryTreeUtil;
using System;
using System.Collections.Generic;
using System.Device.Location;

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
            List<GeoCoordinate> result = new List<GeoCoordinate>();

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
        /// Generates a binary tree of repair station locations based on distance from the origin.
        /// </summary>
        /// <param name="Stations">The list of repair station locations.</param>
        /// <returns>A <see cref="BinaryTree{T}"/> of repair station locations.</returns>
        public static BinaryTree<GeoCoordinate> GetStationsTree(List<GeoCoordinate> Stations)
        {
            if (Stations.Count < 1)
            {
                return null;
            }

            BinaryTree<GeoCoordinate> tree = new BinaryTree<GeoCoordinate>();
            Queue<BinaryTreeNode<GeoCoordinate>> queue = new Queue<BinaryTreeNode<GeoCoordinate>>();
            GeoCoordinate tehran = new GeoCoordinate(35.715298, 51.404343, 1000);

            Stations = SortByDistance(tehran, Stations);
            tree.Root.Value = Stations[0];
            Stations.RemoveAt(0);
            queue.Enqueue(tree.Root);

            while (Stations.Count > 0)
            {
                BinaryTreeNode<GeoCoordinate> node = queue.Dequeue();
                Stations = SortByDistance(node.Value, Stations);
                queue.Enqueue(tree.SetLeft(node, Stations[0], out _));
                Stations.RemoveAt(0);
                if (Stations.Count > 0)
                {
                    queue.Enqueue(tree.SetRight(node, Stations[0], out _));
                    Stations.RemoveAt(0);
                }
            }

            return tree;
        }
    }
}