using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    abstract class MapItem
    {
        public IntVector2 hexQuoordinates;

        public MapItem(IntVector2 hexQuoords) 
        {
            hexQuoordinates = hexQuoords;
        }

        virtual public void Update(double totalGameSeconds) 
        {
        
        }
    }
}
