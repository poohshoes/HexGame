using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Farm : Building
    {
        const int _secondsToGenerateFood = 5;
        double _timeInSecondsForNextFoodGeneration;

        public Farm(IntVector2 farmLocation, Hex hex)
            : base(farmLocation, BuildingTypes.Farm, hex)
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
                _hex.AddResource(new Resource(ResourceTypes.Food));
            }
        }
    }
}
