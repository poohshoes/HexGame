namespace HexGame
{
    class Farm : Building
    {
        const int _secondsToGenerateFood = 5;
        double _timeInSecondsForNextFoodGeneration;

        public Farm(IntVector2 farmLocation, Hex hex, World world)
            : base(farmLocation, BuildingTypes.Farm, hex, world)
        {
            _timeInSecondsForNextFoodGeneration = _secondsToGenerateFood;
            //_hex.AddResource(Resources.Food);
        }

        // every so often a farm generates food
        override public void Update(double totalGameSeconds) 
        {
            if (_timeInSecondsForNextFoodGeneration <= totalGameSeconds)
            {
                _timeInSecondsForNextFoodGeneration += _secondsToGenerateFood;
                // if there is room add a food
                hex.AddResource(new Resource(ResourceTypes.Food));
            }
        }
    }
}
