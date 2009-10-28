using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    class World
    {
        public IntVector2 MapSize { get; private set; }
        Hex[,] _map;

        List<MapItem> _mapItems;

        // keeps track of what the player has selected
        public bool HexIsSelected;
        public IntVector2 _selectedHex;
        public IntVector2 SelectedHex 
        {
            get
            {
                return _selectedHex;
            }
            set 
            {
                if (IsValidHexQuoord(value))
                {
                    HexIsSelected = true;
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
            //addResource(new IntVector2(0, 0), Resources.Food);

            _selectedHex = IntVector2.Zero;

            _mapItems = new List<MapItem>();
        }

        public void Update(double totalGameSeconds) 
        {
            foreach (MapItem m in _mapItems) 
            {
                m.Update(totalGameSeconds);
            }
        }

        public Hex getHexAt(IntVector2 quoord) 
        {
            if(_map[quoord.X, quoord.Y] == null)
                _map[quoord.X, quoord.Y] = new Hex(quoord);

            return _map[quoord.X, quoord.Y];
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
        public bool addResource(IntVector2 hexToAddTo, Resources resource)
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
        public bool removeResource(IntVector2 hexToRemoveFrom, Resources resource) 
        {
            if (_map[hexToRemoveFrom.X, hexToRemoveFrom.Y] == null)
                return false;

            return _map[hexToRemoveFrom.X, hexToRemoveFrom.Y].RemoveResource(resource);
        }
    }
}
