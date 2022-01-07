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
                new GeoCoordinate(0,0,0),
                new GeoCoordinate(1,1,1),
                new GeoCoordinate(2,2,2),
                new GeoCoordinate(3,3,3),
                new GeoCoordinate(4,4,4),
                new GeoCoordinate(5,5,5),
                new GeoCoordinate(6,6,6),
                new GeoCoordinate(7,7,7),
                new GeoCoordinate(8,8,8),
                new GeoCoordinate(9,9,9),
                new GeoCoordinate(10,10,10),
                new GeoCoordinate(11,11,11),
            };

            BinaryTree<GeoCoordinate> tree = GetStationsTree(list);
            tree.PrintToConsole(tree.Root);
        }
    }
}