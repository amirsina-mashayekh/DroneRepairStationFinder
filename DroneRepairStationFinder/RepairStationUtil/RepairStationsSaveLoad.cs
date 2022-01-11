using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DroneRepairStationFinder.RepairStationUtil
{
    public static partial class RepairStationsTree
    {
        public static void WriteStationsToFile(string path, List<GeoCoordinate> coordinates)
        {
            var stringBuilder = new StringBuilder();
            TextWriter writer = new StringWriter(stringBuilder);
            var serializer = new XmlSerializer(typeof(List<GeoCoordinate>));

            serializer.Serialize(writer, coordinates);

            var lines = new List<string>(
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
                var serializer = new XmlSerializer(typeof(List<GeoCoordinate>));
                return serializer.Deserialize(reader) as List<GeoCoordinate>;
            }
        }
    }
}
