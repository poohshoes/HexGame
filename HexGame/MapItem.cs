using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    abstract class MapItem
    {
        public Vector2 hexQuoordinates;

        public MapItem(Vector2 hexQuoords) 
        {
            hexQuoordinates = hexQuoords;
        }
    }
}
