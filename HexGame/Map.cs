using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Map
    {
        public const int MapWidth = 10;
        public const int MapHeight = 10;

        List<MapItem> _mapItems;

        public Map() 
        {
            _mapItems = new List<MapItem>();
        }
    }
}
