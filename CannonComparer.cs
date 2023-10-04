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

            Cannon[] topCannonArr = { TopCannonPerBlockVolume, TopCannonPerBoundingBox };
            foreach (Cannon topCannon in topCannonArr)
            {
                string[] stringsToWrite = topCannon.GenerateStatStringArray(CD);
                for (int i = 0; i < stringsToWrite.Length; i++)
                {
                    writer.WriteLine(stringsToWrite[i]);
                }
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
