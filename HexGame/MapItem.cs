using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    abstract class MapItem
    {
        public int X;
        public int Y;

        public MapItem(int x, int y) 
        {
            X = x;
            Y = y;
        }
    }
}
