using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace CramTetrisCalcUI
{
    public class Cannon
    {
        /// <summary>
        /// Stores component information and runs tests for Tetris effectiveness
        /// </summary>
        /// <param name="componentArray">Array of ComponentSlot objects with pre-set coordinates, type, and orientation</param>
        /// <param name="primeConnectorCoordinates">x, y, z coordinates of fixed Prime Connector, to which all others must 
        /// connected</param>
        /// <param name="layerCount">Number of CRAM Tetris layers</param>
        /// <param name="sideLength">Side length of CRAM Tetris</param>
        public Cannon(ComponentSlot[,,] componentArray, Coordinates primeConnectorCoordinates, int layerCount, int sideLength)
        {
            ComponentArray = componentArray;
            PrimeConnectorCoordinates = primeConnectorCoordinates;
            LayerCount = layerCount;
            SideLength = sideLength;
        }
        public ComponentSlot[,,] ComponentArray { get; }
        public Coordinates PrimeConnectorCoordinates { get; }
        public int LayerCount { get; }
        public int SideLength { get; }
        public int TotalPelletConnections { get; set; } // Total pellet-packer connections
        public int OnePacker { get; set; }
        public int TwoPackers { get; set; }
        public int ThreePackers { get; set; }
        public int FourPackers { get; set; }
        public int FivePackers { get; set; }
        public int SixPackers { get; set; }
        public int BlockVolume { get; set; } // Does not include air
        public int BoundingBoxVolume { get; set; } // Includes all slots which could be occupied, even if they aren't
        public float ConnectionsPerBlockVolume { get; set; }
        public float ConnectionsPerBoundingBox { get; set; }
        public Dictionary<CramComponentType, int> ComponentCounts = new();
        public string Name { get; set; } = "";

        /// <summary>
        /// Gets ComponentSlot object at desired coordinates if they are within array bounds
        /// </summary>
        /// <param name="coords">Coordinates of desired slot</param>
        /// <returns></returns>
        public ComponentSlot GetSlotAtCoordinates(Coordinates coords)
        {
            if (coords.Y < 0 || coords.Y >= ComponentArray.GetLength(0)
                || coords.Z < 0 || coords.Z >= ComponentArray.GetLength(1)
                || coords.X < 0 || coords.X >= ComponentArray.GetLength(2))
            {
                return null;
            }
            else
            {
                return ComponentArray[coords.Y, coords.Z, coords.X];
            }
        }

        /// <summary>
        /// Check that all connectors and packers are connected to Prime Connector
        /// </summary>
        public bool CheckConnections()
        {
            bool allConnected = true;
            List<Coordinates> coordsToCheckList = new() { PrimeConnectorCoordinates };

            // Reset connection status
            foreach (ComponentSlot slotToReset in  ComponentArray)
            {
                slotToReset.IsConnected = false;
            }
            // Prime connector is always connected
            GetSlotAtCoordinates(PrimeConnectorCoordinates).IsConnected = true;

            // Check whether all connectors are connected to Prime Connector through other connectors
            while (coordsToCheckList.Count > 0)
            {
                for (int coordToCheckIndex = 0; coordToCheckIndex < coordsToCheckList.Count(); coordToCheckIndex++)
                {
                    ComponentSlot slot = GetSlotAtCoordinates(coordsToCheckList[coordToCheckIndex]);
                    foreach (Coordinates neighborCoord in slot.GetNeighboringCoordinates())
                    {
                        ComponentSlot neighbor = GetSlotAtCoordinates(neighborCoord);
                        // Don't iterate over same component twice
                        if (neighbor != null
                            && neighbor.ComponentType == CramComponentType.Connector
                            && !neighbor.IsConnected)
                        {
                            neighbor.IsConnected = true;
                            coordsToCheckList.Add(neighborCoord);
                        }
                    }

                    coordsToCheckList.RemoveAt(coordToCheckIndex);
                }
            }

            // Check that all packers are connected to connected connectors
            foreach (ComponentSlot slotToCheck in ComponentArray)
            {
                if (slotToCheck.ComponentType == CramComponentType.Packer)
                {
                    foreach (Coordinates connectorFaceCoords in slotToCheck.Orientation.ConnectorFaces)
                    {
                        Coordinates neighborCoords = Coordinates.AddCoordinates(slotToCheck.SlotCoordinates, connectorFaceCoords);
                        ComponentSlot neighborSlot = GetSlotAtCoordinates(neighborCoords);
                        if (neighborSlot != null
                            && neighborSlot.ComponentType == CramComponentType.Connector
                            && neighborSlot.IsConnected)
                        {
                            slotToCheck.IsConnected = true;
                            break;
                        }
                    }
                }
            }

            // Check all packers and connectors are conected to Prime Connector
            // And all pellets have at least one packer connection
            CalculatePackerToPelletConnections();
            foreach (ComponentSlot slot in ComponentArray)
            {
                if ((slot.ComponentType == CramComponentType.Connector
                    || slot.ComponentType == CramComponentType.Packer)
                    && !slot.IsConnected)
                {
                    allConnected = false;
                    break;
                }
                else if (slot.ComponentType == CramComponentType.Pellet
                    && slot.PackerConnectionCount == 0)
                {
                    allConnected = false;
                    break;
                }
            }
            return allConnected;
        }

        /// <summary>
        /// Check slots adjacent to packers for connected pellets
        /// </summary>
        private void CalculatePackerToPelletConnections()
        {
            // Reset packer connection counts
            foreach (ComponentSlot slotToReset in ComponentArray)
            {
                slotToReset.PackerConnectionCount = 0;
            }

            foreach (ComponentSlot slot in ComponentArray)
            {
                if (slot.ComponentType == CramComponentType.Packer)
                {
                    // Add offsets to slot coordinates and check for pellets
                    foreach (Coordinates offsets in slot.Orientation.PelletFaces)
                    {
                        Coordinates offsetCoordinatesToCheck = Coordinates.AddCoordinates(slot.SlotCoordinates, offsets);
                        ComponentSlot slotToCheck = GetSlotAtCoordinates(offsetCoordinatesToCheck);

                        if (slotToCheck != null
                            && (slotToCheck.ComponentType == CramComponentType.Pellet))
                        {
                            slotToCheck.PackerConnectionCount++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update total pellet-packer connection count, as well as pellet box connection counts
        /// </summary>
        private void CalculateConnectionCounts()
        {
            foreach(ComponentSlot slot in ComponentArray)
            {
                if (slot.ComponentType == CramComponentType.Pellet)
                {
                    if (slot.PackerConnectionCount == 1)
                    {
                        OnePacker++;
                    }
                    else if (slot.PackerConnectionCount == 2)
                    {
                        TwoPackers++;
                    }
                    else if (slot.PackerConnectionCount == 3)
                    {
                        ThreePackers++;
                    }
                    else if (slot.PackerConnectionCount == 4)
                    {
                        FourPackers++;
                    }
                    else if (slot.PackerConnectionCount == 5)
                    {
                        FivePackers++;
                    }
                    else if (slot.PackerConnectionCount == 6)
                    {
                        SixPackers++;
                    }

                    TotalPelletConnections += slot.PackerConnectionCount;
                }
            }
        }

        /// <summary>
        /// Calculate block and bounding box volume
        /// Block volume is number of occupied blocks
        /// Bounding box volume is volume of a cylinder drawn around turret (includes all possible occupied spaces)
        /// </summary>
        private void CalculateVolumes()
        {
            BlockVolume = 0;
            BoundingBoxVolume = 0;
            foreach (ComponentSlot slot in ComponentArray)
            {
                if (slot.IsAvailable)
                {
                    BoundingBoxVolume++;
                    if (slot.ComponentType != CramComponentType.Air)
                    {
                        BlockVolume++;
                    }
                }
            }
        }

        public void CalculateConnectionsPerVolume()
        {
            CalculateConnectionCounts();
            CalculateVolumes();

            ConnectionsPerBlockVolume = (float)TotalPelletConnections / BlockVolume;
            ConnectionsPerBoundingBox = (float)TotalPelletConnections / BoundingBoxVolume;
        }

        /// <summary>
        /// Count each component type
        /// </summary>
        public void CountComponentTypes()
        {
            int airCount = 0;
            int pelletCount = 0;
            int packerCount = 0;
            int connectorCount = 0;

            foreach (ComponentSlot slot in ComponentArray)
            {
                if (slot.ComponentType == CramComponentType.Air)
                {
                    airCount++;
                }
                else if (slot.ComponentType == CramComponentType.Pellet)
                {
                    pelletCount++;
                }
                else if (slot.ComponentType == CramComponentType.Packer)
                {
                    packerCount++;
                }
                else if (slot.ComponentType == CramComponentType.Connector)
                {
                    connectorCount++;
                }
            }

            ComponentCounts[CramComponentType.Air] = airCount;
            ComponentCounts[CramComponentType.Pellet] = pelletCount;
            ComponentCounts[CramComponentType.Packer] = packerCount;
            ComponentCounts[CramComponentType.Connector] = connectorCount;
        }

        /// <summary>
        /// Generates a string of comma-separated values for displaying cannon components
        /// </summary>
        /// <param name="cd">Column delimiter. Either "," or ";" depending on user regional settings</param>
        public IEnumerable<string> GenerateLayerStrings(char cd)
        {
            for (int y = 0; y < LayerCount; y++)
            {
                // Spacer between layers
                yield return new string($"{cd}Layer { y + 1 } of {LayerCount}");
                for (int z = 0; z < SideLength; z++)
                {
                    string zString = $"{cd}";
                    for (int x = 0; x < SideLength; x++)
                    {
                        ComponentSlot slot = GetSlotAtCoordinates(new(x, y, z));
                        if (slot != null)
                        {
                            if (slot.ComponentType == CramComponentType.Air
                                || !slot.IsAvailable)
                            {
                                zString += cd;
                            }
                            else if (slot.ComponentType == CramComponentType.Packer)
                            {
                                zString += slot.Orientation.Name + cd;
                            }
                            else
                            {
                                zString += slot.ComponentType.ToString() + cd;
                            }
                        }
                    }
                    yield return zString;
                }
            }
        }

        /// <summary>
        /// Generate comma-separated cannon stats array
        /// For printing to file or console
        /// </summary>
        /// <param name="cd">Column delimiter; either ',' or ';' depending on regional settings</param>
        /// <returns></returns>
        public string[] GenerateStatStringArray(char cd)
        {
            List<string> strings = new()
            {
                Name
            };
            foreach (string stringToWrite in GenerateLayerStrings(cd))
            {
                strings.Add(stringToWrite);
            }

            CountComponentTypes();
            string componentCountHeader = $"{cd}";
            foreach (CramComponentType type in ComponentCounts.Keys)
            {
                componentCountHeader += type.ToString() + cd;
            }
            strings.Add(componentCountHeader);
            string componentCountValues = $"{cd}";
            foreach (CramComponentType type in ComponentCounts.Keys)
            {
                componentCountValues += ComponentCounts[type].ToString() + cd;

            }
            strings.Add(componentCountValues);

            strings.Add($"Total pellet connections{cd}{TotalPelletConnections}");
            strings.Add($"Occupied blocks{cd}{BlockVolume}");
            strings.Add($"Connections per block volume{cd}{ConnectionsPerBlockVolume}");
            strings.Add($"Bounding volume{cd}{BoundingBoxVolume}");
            strings.Add($"Connections per bounding{cd}{ConnectionsPerBoundingBox}");

            // Spacer between cannons
            strings.Add("");
            strings.Add("");

            return strings.ToArray();
        }
    }
}
