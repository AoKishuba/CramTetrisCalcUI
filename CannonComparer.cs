using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramTetrisCalcUI
{
    public enum TestType : int
    {
        BlockVolume,
        BoundingBox
    }

    internal class CannonComparer
    {
        public CannonComparer(
            TetrisArray tetrisArray,
            int layerCount,
            Coordinates primeConnectorCoordinates,
            char columnDelimiter)
        {
            TetrisArray = tetrisArray;
            LayerCount = layerCount;
            PrimeConnectorCoordinates = primeConnectorCoordinates;
            ColumnDelimiter = columnDelimiter;

            TopCannonPerBlockVolume = new(new ComponentSlot[,,] { }, primeConnectorCoordinates, layerCount, TetrisArray.SideLength)
            {
                ConnectionsPerBlockVolume = 0
            };

            TopCannonPerBoundingBox = new(new ComponentSlot[,,] { }, primeConnectorCoordinates, layerCount, TetrisArray.SideLength)
            {
                ConnectionsPerBoundingBox = 0
            };
        }
        private TetrisArray TetrisArray { get; }
        private int LayerCount { get; }
        private Coordinates PrimeConnectorCoordinates { get; }
        private Cannon TopCannonPerBlockVolume { get; set; }
        private Cannon TopCannonPerBoundingBox { get; set; }
        private char ColumnDelimiter { get; }

        private void WriteCannonsToFile()
        {
            // Create filename from current time
            char CD = ColumnDelimiter;
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ff") + ".csv";

            using var writer = new StreamWriter(fileName, append: true);
            FileStream fs = (FileStream)writer.BaseStream;

            writer.WriteLine("\nTest Parameters");
            writer.WriteLine($"Tetris dimensions{CD}{TetrisArray.SideLength}x{TetrisArray.SideLength}");
            writer.WriteLine($"Layers{CD}{LayerCount}");
            writer.WriteLine($"Prime connector{CD}x{CD}y{CD}z");
            writer.WriteLine($"{CD}{PrimeConnectorCoordinates.X}{CD}{PrimeConnectorCoordinates.Y}{CD}{PrimeConnectorCoordinates.Z}");

            writer.WriteLine("Legend");
            writer.WriteLine($"{CD}P{CD}C");
            writer.WriteLine($"{CD}Pellet{CD}Connector");
            writer.WriteLine("Packers are labeled with letters indicating the orientation of their pellet-connecting faces");
            writer.WriteLine($"{CD}N{CD}S{CD}E{CD}W{CD}U{CD}D");
            writer.WriteLine($"{CD}North{CD}South{CD}East{CD}West{CD}Up{CD}Down");

            Cannon[] topCannonArr = { TopCannonPerBlockVolume, TopCannonPerBoundingBox };
            foreach (Cannon topCannon in topCannonArr)
            {
                writer.WriteLine(topCannon.Name);
                foreach (string stringToWrite in topCannon.GenerateLayerStrings(CD))
                {
                    writer.WriteLine(stringToWrite);
                }

                topCannon.CountComponentTypes();
                string componentCountHeader = $"{CD}";
                foreach (CramComponentType type in topCannon.ComponentCounts.Keys)
                {
                    componentCountHeader += type.ToString() + ColumnDelimiter;
                }
                writer.WriteLine(componentCountHeader);
                string componentCountValues = $"{CD}";
                foreach (CramComponentType type in topCannon.ComponentCounts.Keys)
                {
                    componentCountValues += topCannon.ComponentCounts[type].ToString() + ColumnDelimiter;

                }
                writer.WriteLine(componentCountValues);

                writer.WriteLine($"Total pellet connections {CD}{topCannon.TotalPelletConnections}");
                writer.WriteLine($"Occupied blocks {CD}{topCannon.BlockVolume}");
                writer.WriteLine($"Connections per block volume {CD}{topCannon.ConnectionsPerBlockVolume}");
                writer.WriteLine($"Bounding volume {CD}{topCannon.BoundingBoxVolume}");
                writer.WriteLine($"Connections per bounding {CD}{topCannon.ConnectionsPerBoundingBox}");

                // Spacer between cannons
                writer.WriteLine("");
                writer.WriteLine("");
            }
        }

        /// <summary>
        /// Test all possible cannon configurations and output the highest-performing
        /// </summary>
        public void CannonTest()
        {
            ComponentSlot[,,] componentSlots = TetrisArray.CreateComponentSlotArray(LayerCount, PrimeConnectorCoordinates);
            CannonGenerator generator = new(componentSlots, PrimeConnectorCoordinates, LayerCount, TetrisArray.SideLength);
            Cannon[] cannonArr = generator.GenerateCannons();

            for (int cannonIndex = 0; cannonIndex < cannonArr.Length; cannonIndex++)
            {
                Cannon cannonToCompare = cannonArr[cannonIndex];
                if (cannonToCompare.ConnectionsPerBlockVolume > TopCannonPerBlockVolume.ConnectionsPerBlockVolume)
                {
                    TopCannonPerBlockVolume = cannonToCompare;
                }

                if (cannonToCompare.ConnectionsPerBoundingBox > TopCannonPerBoundingBox.ConnectionsPerBoundingBox)
                {
                    TopCannonPerBoundingBox = cannonToCompare;
                }
            }

            TopCannonPerBlockVolume.Name = "Most connections per block volume";
            TopCannonPerBoundingBox.Name = "Most connections per bounding box";
            WriteCannonsToFile();
        }
    }
}
