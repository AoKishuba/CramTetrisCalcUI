using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CramTetrisCalcUI
{
    /// <summary>
    /// Enumerates type of component occupying space in Tetris array
    /// </summary>
    public enum CramComponentType : int
    {
        Air,
        Connector,
        Pellet,
        Packer
    }

    /// <summary>
    /// Base class for specific blocks within Tetris array
    /// </summary>
    public class ComponentSlot
    {
        /// <summary>
        /// Initializes a specific spot in 3D space
        /// </summary>
        /// <param name="x">X coordinate (West-East)</param>
        /// <param name="y">Y coordinate (Up-Down)</param>
        /// <param name="z">Z coordinate (South-North)</param>
        /// <param name="isAvailable">Slot can be used for components without interfering with turret rotation</param>
        /// <param name="componentType">Type of CRAM component</param>
        public ComponentSlot(
            int x,
            int y,
            int z,
            bool isAvailable = true,
            bool isPrimeConnector = false,
            CramComponentType componentType = CramComponentType.Air)
        {
            SlotCoordinates = new(x, y, z);
            IsAvailable = isAvailable;
            IsPrimeConnector = isPrimeConnector;
            ComponentType = componentType;
            _orientation = PackerOrientation.None;
        }

        public bool IsAvailable { get; }
        public bool IsPrimeConnector { get; }
        public CramComponentType ComponentType { get; set; }
        // Non-packer components have no orientation
        public PackerOrientation Orientation
        {
            get { return ComponentType == CramComponentType.Packer ? _orientation : PackerOrientation.None; }
            set { _orientation = ComponentType == CramComponentType.Packer ? value : PackerOrientation.None; }
        }
        private PackerOrientation _orientation;

        public int PackerConnectionCount = 0;
        public bool IsConnected { get; set; } = false;

        public Coordinates SlotCoordinates { get; }

        /// <summary>
        /// Get coordinates for slots on all six sides
        /// </summary>
        /// <returns></returns>
        public Coordinates[] GetNeighboringCoordinates()
        {
            return new Coordinates[]
            {
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.North),
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.South),
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.East),
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.West),
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.Up),
                Coordinates.AddCoordinates(SlotCoordinates, Coordinates.Down)
            };
        }
    }
}
