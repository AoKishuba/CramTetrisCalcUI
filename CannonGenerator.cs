using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CramTetrisCalcUI
{
    public class CannonGenerator
    {
        public CannonGenerator(ComponentSlot[,,] originalSlots, Coordinates primeConnectorCoordinates, int layerCount, int sideLength)
        {
            OriginalSlots = originalSlots;
            PrimeConnectorCoordinates = primeConnectorCoordinates;
            LayerCount = layerCount;
            SideLength = sideLength;
        }

        private ComponentSlot[,,] OriginalSlots { get; }
        private Coordinates PrimeConnectorCoordinates { get; }
        private int LayerCount { get; }
        private int SideLength { get; }
        private ConcurrentBag<Cannon> Cannons { get; set; } = new();
        private int ValidCannons { get; set; } = 0;
        private int RejectedCannons { get; set; } = 0;
        private int TotalCannons { get; set; } = 0;

        /// <summary>
        /// Generate all possible valid cannons
        /// </summary>
        /// <returns>HashSet containing all valid cannon configurations within given parameters</returns>
        public Cannon[] GenerateCannons()
        {
            GenerateCannonsRecursive(OriginalSlots, 0, 0, 0);
            return Cannons.ToArray();
        }

        /// <summary>
        /// Recursive helper
        /// </summary>
        /// <param name="currentSlotArray">Current array of ComponentSlot objects</param>
        /// <param name="x">Current x-coordinate</param>
        /// <param name="y">Current y-coordinate</param>
        /// <param name="z">Current z-coordinate</param>
        private void GenerateCannonsRecursive(ComponentSlot[,,] currentSlotArray, int x, int y, int z)
        {
            if (y == LayerCount)
            {
                // Finished recursively iterating through the entire array
                Cannon cannonToTest = new(currentSlotArray, PrimeConnectorCoordinates, LayerCount, SideLength);
                // Add only valid cannons
                if (cannonToTest.CheckConnections())
                {
                    cannonToTest.CalculateConnectionsPerVolume();
                    Cannons.Add(cannonToTest);
                    ValidCannons++;
                }
                else
                {
                    RejectedCannons++;
                }

                TotalCannons = ValidCannons + RejectedCannons;
                if (TotalCannons % 1000000 == 0)
                {
                    string validCannonStr = ValidCannons.ToString("N0");
                    string totalCannonStr = TotalCannons.ToString("N0");
                    string rejectedStr = RejectedCannons.ToString("N0");
                    Console.WriteLine($"{validCannonStr} valid cannons of {totalCannonStr} tested; {rejectedStr} rejected");
                }
            }
            else if (x == SideLength)
            {
                // Move to the next row in layer
                GenerateCannonsRecursive(currentSlotArray, 0, y, z + 1);
            }
            else if (z == SideLength)
            {
                // Move to the next layer
                GenerateCannonsRecursive(currentSlotArray, 0, y + 1, 0);
            }
            else
            {
                foreach (CramComponentType type in Enum.GetValues(typeof(CramComponentType)))
                {
                    ComponentSlot slotToSet = currentSlotArray[y, z, x];
                    // Skip unavailable
                    if (!slotToSet.IsAvailable)
                    {
                        GenerateCannonsRecursive(currentSlotArray, x + 1, y, z);
                    }
                    // Ensure prime connector is connector
                    else if (slotToSet.IsPrimeConnector)
                    {
                        slotToSet.ComponentType = CramComponentType.Connector;
                        GenerateCannonsRecursive(currentSlotArray, x + 1, y, z);
                    }
                    // Iterate over all possible Packer orientations
                    else if (type == CramComponentType.Packer)
                    {
                        foreach (PackerOrientation orientation in PackerOrientation.AllOrientations)
                        {
                            slotToSet.Orientation = orientation;
                            GenerateCannonsRecursive(currentSlotArray, x + 1, y, z);
                        }
                    }
                    else
                    {
                        slotToSet.ComponentType = type;
                        GenerateCannonsRecursive(currentSlotArray, x + 1, y, z);
                    }
                }
            }
        }
    }
}
