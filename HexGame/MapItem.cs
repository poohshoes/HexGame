namespace HexGame
{
    abstract class MapItem
    {
        public IntVector2 HexQuoordinates;

        // a reference to the world
        protected World _world;

        protected MapItem(IntVector2 hexQuoords, World world) 
        {
            HexQuoordinates = hexQuoords;
            _world = world;
        }

        virtual public void Update(double totalGameSeconds) 
        {
        
        }
    }
}
