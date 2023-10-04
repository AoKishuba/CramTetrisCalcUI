using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CramTetrisCalcUI
{
    public class TetrisArray
    {
        /// <summary>
        /// Stores an array of component slots, corresponding to one of the turret guide sizes
        /// </summary>
        /// <param name="sideLength">Overall length of side of turret, in blocks</param>
        /// <param name="blockedArray">Array containing number of blocked cells for each Z value (North/South) in top-left corner</param>
        public TetrisArray(int sideLength, int[] blockedArray)
        {
            Name = sideLength.ToString() + "x" + sideLength.ToString();
            SideLength = sideLength;
            BlockedArray = blockedArray;
        }
        public string Name { get; }
        public int SideLength { get; }
        public int[] BlockedArray { get; }

        /// <summary>
        /// Creates an Array of ComponentSlots for all slots in a given Tetris size
        /// </summary>
        /// <returns></returns>
        public ComponentSlot[,,] CreateComponentSlotArray(int layerCount, Coordinates primeConnectorCoordinates)
        {
            ComponentSlot[,,] componentSlotsArray = new ComponentSlot[layerCount, SideLength, SideLength];

            for (int y = 0; y < layerCount; y++)
            {
                for (int z = 0; z < SideLength; z++)
                {
                    for (int x = 0; x < SideLength; x++)
                    {
                        ComponentSlot newSlot;
                        // Set type to blocked if within coordinates defined by blocked array
                        // Note BlockedArray only contains number of blocked slots in top-left corner;
                        // Need to capture all four corners
                        if ((z < BlockedArray.Length && x < BlockedArray[z]) // Top left corner
                            || (SideLength - z <= BlockedArray.Length && x < BlockedArray[SideLength - z - 1]) // Bottom left corner
                            || (z < BlockedArray.Length && SideLength - x <= BlockedArray[z]) // Top right corner
                            || (SideLength - z <= BlockedArray.Length && SideLength - x <= BlockedArray[SideLength - z - 1])) // Bottom right corner
                        {
                            newSlot = new ComponentSlot(x, y, z, false);
                        }
                        else if (x == primeConnectorCoordinates.X
                            && y == primeConnectorCoordinates.Y
                            && z == primeConnectorCoordinates.Z)
                        {
                            newSlot = new ComponentSlot(x, y, z, true, true, CramComponentType.Connector);
                        }
                        else
                        {
                            newSlot = new ComponentSlot(x, y, z);
                        }

                        componentSlotsArray[y, z, x] = newSlot;
                    }
                }
            }

            return componentSlotsArray;
        }


        // Define turret guide shapes
        public static readonly TetrisArray ThreeByThree = new(3, Array.Empty<int>());

        private static readonly int[] FiveByFiveBlocked = { 1 };
        public static readonly TetrisArray FiveByFive = new(5, FiveByFiveBlocked);

        private static readonly int[] SevenBySevenBlocked = { 2, 1 };
        public static readonly TetrisArray SevenBySeven = new(7, SevenBySevenBlocked);

        private static readonly int[] NineByNineBlocked = { 2, 1 };
        public static readonly TetrisArray NineByNine = new(9, NineByNineBlocked);

        private static readonly int[] ElevenByElevenBlocked = { 2, 1 };
        public static readonly TetrisArray ElevenByEleven = new(11, ElevenByElevenBlocked);

        private static readonly int[] ThirteenByThirteenBlocked = { 3, 2, 1 };
        public static readonly TetrisArray ThirteenByThirteen = new(13, ThirteenByThirteenBlocked);

        private static readonly int[] FifteenByFifteenBlocked = { 4, 3, 2, 1 };
        public static readonly TetrisArray FifteenByFifteen = new(15, FifteenByFifteenBlocked);

        private static readonly int[] SeventeenBySeventeenBlocked = { 5, 3, 2, 1, 1 };
        public static readonly TetrisArray SeventeenBySeventeen = new(17, SeventeenBySeventeenBlocked);

        private static readonly int[] NineteenByNineteenBlocked = { 6, 4, 3, 2, 1, 1 };
        public static readonly TetrisArray NineteenByNineteen = new(19, NineteenByNineteenBlocked);

        private static readonly int[] TwentyoneByTwentyoneBlocked = { 7, 5, 4, 3, 2, 1 };
        public static readonly TetrisArray TwentyoneByTwentyone = new(21, TwentyoneByTwentyoneBlocked);
        public static TetrisArray[] AllArrays { get; } =
            {
            ThreeByThree,
            FiveByFive,
            SevenBySeven,
            NineByNine,
            ElevenByEleven,
            ThirteenByThirteen,
            FifteenByFifteen,
            SeventeenBySeventeen,
            NineteenByNineteen,
            TwentyoneByTwentyone
        };
    }
}
