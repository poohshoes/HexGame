using System;

namespace HexGame
{
    abstract class MapItem
    {
        public World World { get; private set; }

        public IntVector2 HexQuoordinates { get; protected set; }

        /// <summary>
        /// The tile the map item is currently on
        /// </summary>
        public Hex HexTile
        {
            get { return World.GetHexAt(HexQuoordinates); }
        }

        protected MapItem(IntVector2 hexQuoords, World world) 
        {
            HexQuoordinates = hexQuoords;
            World = world;
        }

        virtual public void Update(double totalGameSeconds) 
        {
        
        }
    }
}
