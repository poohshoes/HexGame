using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    class World
    {
        public Vector2 MapSize { get; private set; }

        List<MapItem> _mapItems;

        // keeps track of what the player has selected
        public bool HexIsSelected;
        public Vector2 _selectedHex;
        public Vector2 SelectedHex 
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
            MapSize = new Vector2(10, 10);
            _selectedHex = Vector2.Zero;

            _mapItems = new List<MapItem>();
        }

        public bool IsValidHexQuoord(Vector2 hexQuoord) 
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
    }
}
