using BinaryTreeUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Device.Location;
using static DroneRepairStationFinder.RepairStationUtil.RepairStationsTree;

namespace DroneRepairStationFinder.RepairStationUtil.Tests
{
    [TestClass()]
    public class RepairStationsTreeTests
    {
        [TestMethod()]
        public void GetStationsTreeTest()
        {
            List<GeoCoordinate> list = new List<GeoCoordinate>
            {
                new GeoCoordinate(-2, 0, 1),
                new GeoCoordinate(-9, 5, 6),
                new GeoCoordinate(2, -2, 7),
                new GeoCoordinate(6, 3, 1),
                new GeoCoordinate(5, 6, 2),
                new GeoCoordinate(1, 0, 1),
                new GeoCoordinate(-3, -3, 2),
                new GeoCoordinate(4, 2, 1),
            };

            BinaryTree<GeoCoordinate> tree = GetStationsTree(list, new GeoCoordinate(0, 0, 0));
            tree.PrintToConsole(tree.Root);
        }
    }
}