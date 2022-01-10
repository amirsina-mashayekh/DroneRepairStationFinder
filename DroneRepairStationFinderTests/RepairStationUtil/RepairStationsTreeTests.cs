using DroneRepairStationFinder.RepairStationUtil;
using BinaryTreeUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Device.Location;
using static DroneRepairStationFinder.RepairStationUtil.RepairStationsTree;
using System.Linq;

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

            var tree = GetStationsTree(list, new GeoCoordinate(0, 0, 0));
            tree.PrintToConsole(tree.Root);
        }

        [TestMethod()]
        public void GetNearestStationTest()
        {
            var list = new List<GeoCoordinate>
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

            var tree = GetStationsTree(list, new GeoCoordinate(0, 0, 0));
            tree.PrintToConsole(tree.Root);
            var nearest = GetNearestStation(tree, new GeoCoordinate(3.9, 3, 1.7), out var checkedStations);

            Assert.AreEqual(list[7], nearest.Value);
            Assert.AreEqual(checkedStations[0].Value, list[5]);
            Assert.AreEqual(checkedStations[1].Value, list[2]);
            Assert.AreEqual(checkedStations[2].Value, list[0]);
            Assert.AreEqual(checkedStations[3].Value, list[7]);
            Assert.AreEqual(checkedStations[4].Value, list[6]);
            Assert.AreEqual(checkedStations[5].Value, list[3]);
            Assert.AreEqual(checkedStations[6].Value, list[1]);
        }
    }
}