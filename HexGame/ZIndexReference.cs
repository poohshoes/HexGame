using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{ 
    /// <summary>
    /// The order of this enum is the order that Items will draw in the game.
    /// Divide the enum position by 100 to get the zIndex.
    /// </summary>
    static class ZIndexReference
    {   
        public enum drawnItemOrders
        {
            mapTile = 0,
            interfaceBackground,
            interfaceContent
        }

        public static float getZIndex(drawnItemOrders drawnItem)
        {
            return ((int)drawnItem) / 100f;
            }
    }
}
