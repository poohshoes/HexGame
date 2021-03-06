﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HexGame
{
    class World
    {
        public PathFinding PathFinder { get; private set; }

        public IntVector2 MapSize { get; private set; }
        Hex[,] _map;

        List<MapItem> _mapItems;

        private IntVector2? _selectedHex = null;
        public IntVector2? SelectedHex 
        {
            get
            {
                return _selectedHex;
            }
            set 
            {
                if (IsValidHexQuoord(value))
                {
                    _selectedHex = value;
                }
            }
        }

        public IEnumerable<MapItem> MapItems 
        {
            get
            {
                return _mapItems.AsEnumerable();
            }
        } 

        public World() 
        {
            MapSize = new IntVector2(10, 10);
            _map = new Hex[MapSize.X, MapSize.Y];

            _selectedHex = IntVector2.Zero;

            _mapItems = new List<MapItem>();
        }

        public void Update(double totalGameSeconds) 
        {
            foreach (MapItem m in _mapItems) 
            {
                m.Update(totalGameSeconds);
            }

            _mapItems.RemoveAll(x => x is Unit && ((Unit) x).isDead);
        }

        public Hex GetHexAt(IntVector2 quoord) 
        {
            if(_map[quoord.X, quoord.Y] == null)
                _map[quoord.X, quoord.Y] = new Hex(quoord);

            return _map[quoord.X, quoord.Y];
        }

        public bool IsValidHexQuoord(IntVector2? hexQuoord)
        {
            if (hexQuoord == null)
                return false;

            return IsValidHexQuoord(hexQuoord.Value);
        }

        public bool IsValidHexQuoord(IntVector2 hexQuoord) 
        {
            if (hexQuoord.X >= 0
                && hexQuoord.X < MapSize.X
                && hexQuoord.Y >= 0
                && hexQuoord.Y < MapSize.Y)
                return true;

            return false;
        }

        public void AddMapItem(MapItem toAdd) 
        {
            _mapItems.Add(toAdd);
        }

        public List<Hex> getHexesWithResources() 
        {
            List<Hex> hexesWithResources = new List<Hex>();

            for (int x = 0; x < MapSize.X; x++) 
            {
                for (int y = 0; y < MapSize.Y; y++) 
                {
                    if (_map[x, y] != null && _map[x, y].numResources() > 0)
                        hexesWithResources.Add(_map[x, y]);
                }
            }

            return hexesWithResources;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexToAddTo"></param>
        /// <param name="resource"></param>
        /// <returns>False if the resource was not added.</returns>
        public bool addResource(IntVector2 hexToAddTo, Resource resource)
        {
            if(_map[hexToAddTo.X, hexToAddTo.Y] == null)
                _map[hexToAddTo.X, hexToAddTo.Y] = new Hex(hexToAddTo);

            return _map[hexToAddTo.X, hexToAddTo.Y].AddResource(resource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexToRemoveFrom"></param>
        /// <param name="resource"></param>
        /// <returns>False if the resource was not removed.</returns>
        public bool removeResource(IntVector2 hexToRemoveFrom, Resource resource) 
        {
            if (_map[hexToRemoveFrom.X, hexToRemoveFrom.Y] == null)
                return false;

            return _map[hexToRemoveFrom.X, hexToRemoveFrom.Y].RemoveResource(resource);
        }

        /// <summary>
        /// Every some hexes are sunk so that they line up.
        /// </summary>
        /// <param name="x">The hexes x quoordinate.</param>
        /// <returns>True if the hex is sunken.</returns>
        public bool hexIsSunken(int x)
        {
            return x % 2 == 1;
        }

        public bool hexIsSunken(float x)
        {
            return hexIsSunken(Convert.ToInt32(x));
        }

        /// <summary>
        /// Gets the move cost between two ADJACENT hexes.
        /// </summary>
        /// <param name="startingLocation"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public float getMoveCost(IntVector2 startingLocation, IntVector2 endingLocation) 
        {
            Hex startHex = _map[startingLocation.X, startingLocation.Y];
            float startingCost;
            if (startHex == null)
                startingCost = 1;
            else
                startingCost = (float)startHex.MoveCost;

            Hex endHex = _map[endingLocation.X, endingLocation.Y];
            float endingCost;
            if (endHex == null)
                endingCost = 1;
            else
                endingCost = (float)endHex.MoveCost;

            return (startingCost + endingCost) / 2;
        }

        /// <summary>
        /// The neighbours shape without middle is like this:
        ///    _
        ///  _/ \_
        /// / \_/ \
        /// \_/N\_/
        /// / \_/ \
        /// \_/ \_/
        ///   \_/
        /// </summary>
        /// <param name="location">The middle.</param>
        /// <returns>A list of the quordinates of valid hex locations in the neighbours battern.</returns>
        public List<IntVector2> neighborHexes(IntVector2 location)
        {
            List<IntVector2> unvalidatedNeighborHexes = new List<IntVector2>();

            // the hexes to the left and right/ above and below will be neighbours
            unvalidatedNeighborHexes.Add(new IntVector2(location.X, location.Y + 1));
            unvalidatedNeighborHexes.Add(new IntVector2(location.X, location.Y - 1));
            unvalidatedNeighborHexes.AddRange(getBoneShapeWithoutMiddleHexLocations(location));

            List<IntVector2> neighborHexes = new List<IntVector2>();

            foreach (IntVector2 h in unvalidatedNeighborHexes) 
            {
                if (IsValidHexQuoord(h))
                    neighborHexes.Add(h);
            }

            return neighborHexes;
        }

        public List<Unit> neighbourUnits(IntVector2 location)
        {
            List<Unit> units = new List<Unit>();

            foreach (IntVector2 neighbourTile in neighborHexes(location))
            {
                Unit unit = _getUnitAt(neighbourTile);
                if (unit != null)
                    units.Add(unit);
            }

            return units;
        }

        public Unit firstNeighbouringUnit(IntVector2 location)
        {
            foreach (IntVector2 neighbourTile in neighborHexes(location))
            {
                Unit unit = _getUnitAt(neighbourTile);
                if (unit != null)
                    return unit;
            }

            return null;
        }

        /// <summary>
        /// The bone shape is like this:
        ///  _   _
        /// / \_/ \
        /// \_/ \_/
        /// / \_/ \
        /// \_/ \_/
        /// </summary>
        /// <param name="middleHex">The hex that will be the middle bone.</param>
        /// <returns>A list of the VALID hex quoordinates in the bone.</returns>
        public List<IntVector2> getBoneShapeHexLocations(IntVector2 middleHex)
        {
            List<IntVector2> unvalidatedBoneHexes = new List<IntVector2>();

            unvalidatedBoneHexes.Add(middleHex);
            unvalidatedBoneHexes.AddRange(getBoneShapeWithoutMiddleHexLocations(middleHex));

            List<IntVector2> validatedBoneHexes = new List<IntVector2>();

            foreach (IntVector2 v in unvalidatedBoneHexes)
            {
                if (IsValidHexQuoord(v))
                    validatedBoneHexes.Add(v);
            }

            return validatedBoneHexes;
        }

        /// <summary>
        /// The bone shape without middle is like this:
        ///  _   _
        /// / \ / \
        /// \_/ \_/
        /// / \ / \
        /// \_/ \_/
        /// </summary>
        /// <param name="middleHex">The hex that will be the middle bone.</param>
        /// <returns>A list of the VALID hex quoordinates in the bone.</returns>
        public List<IntVector2> getBoneShapeWithoutMiddleHexLocations(IntVector2 middleHex)
        {
            List<IntVector2> unvalidatedBoneHexes = new List<IntVector2>();

            unvalidatedBoneHexes.Add(new IntVector2(middleHex.X - 1, middleHex.Y));
            unvalidatedBoneHexes.Add(new IntVector2(middleHex.X + 1, middleHex.Y));

            if (hexIsSunken(middleHex.X))
            {
                unvalidatedBoneHexes.Add(new IntVector2(middleHex.X - 1, middleHex.Y + 1));
                unvalidatedBoneHexes.Add(new IntVector2(middleHex.X + 1, middleHex.Y + 1));
            }
            else
            {
                unvalidatedBoneHexes.Add(new IntVector2(middleHex.X - 1, middleHex.Y - 1));
                unvalidatedBoneHexes.Add(new IntVector2(middleHex.X + 1, middleHex.Y - 1));
            }

            List<IntVector2> validatedBoneHexes = new List<IntVector2>();

            foreach (IntVector2 v in unvalidatedBoneHexes)
            {
                if (IsValidHexQuoord(v))
                    validatedBoneHexes.Add(v);
            }

            return validatedBoneHexes;
        }

        public void MoveCommand(IntVector2? selectedHex, IntVector2 clickHex)
        {
            Unit selectedUnit = _getUnitAt((IntVector2)selectedHex);

            if (selectedUnit != null)
            {
                selectedUnit.Tasks.Clear();
                selectedUnit.Tasks.Enqueue(new MoveToHexTask(selectedUnit, GetHexAt(clickHex)));
            }
        }

        private Unit _getUnitAt(IntVector2 selectedHex)
        {
            return _mapItems.OfType<Unit>().FirstOrDefault(x => x.HexTile == GetHexAt(selectedHex));
        }
    }
}
