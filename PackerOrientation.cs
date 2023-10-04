using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramTetrisCalcUI
{
    public class PackerOrientation
    {
        /// <summary>
        /// Stores coordinate offsets for checking neighboring slots for pellets or connectors
        /// </summary>
        public PackerOrientation(string name, Coordinates pelletFace1, Coordinates pelletFace2, Coordinates pelletFace3) 
        {
            Name = name;
            // Three sides connect to pellets
            PelletFaces = new Coordinates[] { pelletFace1, pelletFace2, pelletFace3 };
            // Three sides connect to connectosr
            List<Coordinates> connectorFaceList = new();
            if (PelletFaces.Contains(Coordinates.Self))
            {
                connectorFaceList.Add(Coordinates.Self);
            }
            else
            {
                foreach (Coordinates coordsToCheck in Coordinates.AllDirections)
                {
                    if (!PelletFaces.Contains(coordsToCheck))
                    {
                        connectorFaceList.Add(coordsToCheck);
                    }
                }
            }

            ConnectorFaces = connectorFaceList.ToArray();
        }
        public string Name { get; }
        public Coordinates[] PelletFaces { get; }
        public Coordinates[] ConnectorFaces { get; }

        public static PackerOrientation NEW = new("NEW", Coordinates.North, Coordinates.East, Coordinates.West);
        public static PackerOrientation SEW = new("SEW", Coordinates.South, Coordinates.East, Coordinates.West);
        public static PackerOrientation ENS = new("ENS", Coordinates.East, Coordinates.North, Coordinates.South);
        public static PackerOrientation WNS = new("WNS", Coordinates.West, Coordinates.North, Coordinates.South);
        public static PackerOrientation NUD = new("NUD", Coordinates.North, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation SUD = new("SUD", Coordinates.South, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation EUD = new("EUD", Coordinates.East, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation WUD = new("WUD", Coordinates.West, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation UNS = new("UNS", Coordinates.Up, Coordinates.North, Coordinates.South);
        public static PackerOrientation UEW = new("UEW", Coordinates.Up, Coordinates.East, Coordinates.West);
        public static PackerOrientation DNS = new("DNS", Coordinates.Down, Coordinates.North, Coordinates.South);
        public static PackerOrientation DEW = new("DEW", Coordinates.Down, Coordinates.East, Coordinates.West);
        public static PackerOrientation None = new("None", Coordinates.Self, Coordinates.Self, Coordinates.Self);

        public static PackerOrientation[] AllOrientations = new[]
        {
            NEW,
            SEW,
            ENS,
            WNS,
            NUD,
            SUD,
            EUD,
            WUD,
            UNS,
            UEW,
            DNS,
            DEW
        };
    }
}
