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
        public PackerOrientation(Coordinates pelletFace1, Coordinates pelletFace2, Coordinates pelletFace3) 
        {
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
        public Coordinates[] PelletFaces { get; }
        public Coordinates[] ConnectorFaces { get; }

        public static PackerOrientation NEW = new(Coordinates.North, Coordinates.East, Coordinates.West);
        public static PackerOrientation SEW = new(Coordinates.South, Coordinates.East, Coordinates.West);
        public static PackerOrientation ENS = new(Coordinates.East, Coordinates.North, Coordinates.South);
        public static PackerOrientation WNS = new(Coordinates.West, Coordinates.North, Coordinates.South);
        public static PackerOrientation NUD = new(Coordinates.North, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation SUD = new(Coordinates.South, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation EUD = new(Coordinates.East, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation WUD = new(Coordinates.West, Coordinates.Up, Coordinates.Down);
        public static PackerOrientation UNS = new(Coordinates.Up, Coordinates.North, Coordinates.South);
        public static PackerOrientation UEW = new(Coordinates.Up, Coordinates.East, Coordinates.West);
        public static PackerOrientation DNS = new(Coordinates.Down, Coordinates.North, Coordinates.South);
        public static PackerOrientation DEW = new(Coordinates.Down, Coordinates.East, Coordinates.West);
        public static PackerOrientation None = new(Coordinates.Self, Coordinates.Self, Coordinates.Self);

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
