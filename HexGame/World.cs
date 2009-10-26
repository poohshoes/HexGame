using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class World
    {
        public Map Map { get; private set; }

        public int Height 
        {
            get
            {
                return Map.MapHeight;
            }
        }
        public int Width
        {
            get
            {
                return Map.MapWidth;
            }
        }

        public World()
        {
            Map = new Map();
        }
    }
}
