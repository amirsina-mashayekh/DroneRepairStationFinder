using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DroneRepairStationFinder.RepairStationUtil
{
    public static partial class RepairStationsTree
    {
        public static void WriteStationsToFile(string path, List<GeoCoordinate> coordinates)
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter writer = new StringWriter(stringBuilder);
            XmlSerializer serializer = new XmlSerializer(typeof(List<GeoCoordinate>));

            serializer.Serialize(writer, coordinates);

            List<string> lines = new List<string>(
                stringBuilder.ToString().Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries));

            lines.RemoveAll(line =>
                line.Contains("HorizontalAccuracy")
                || line.Contains("VerticalAccuracy")
                || line.Contains("Speed")
                || line.Contains("Course"));

            File.WriteAllLines(path, lines);
        }

        public static List<GeoCoordinate> ReadStationsFromFile(string path)
        {
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<GeoCoordinate>));
                return serializer.Deserialize(reader) as List<GeoCoordinate>;
            }
        }
    }
}