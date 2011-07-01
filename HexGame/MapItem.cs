using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    abstract class MapItem
    {
        public World World { get; private set; }

        public IntVector2 HexQuoordinates { get; protected set; }

        public Hex HexTile
        {
            get { return World.GetHexAt(HexQuoordinates); }
        }

        public MapItem(IntVector2 hexQuoords, World world) 
        {
            HexQuoordinates = hexQuoords;
        }

        virtual public void Update(double totalGameSeconds) 
        {
        
        }
    }
}
