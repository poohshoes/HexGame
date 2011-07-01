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

        public Farm(Hex hex, World world)
            : base(BuildingTypes.Farm, hex, world)
        {
            _timeInSecondsForNextFoodGeneration = _secondsToGenerateFood;
        }

        // every so often a farm generates food
        override public void Update(double totalGameSeconds) 
        {
            if (_timeInSecondsForNextFoodGeneration <= totalGameSeconds)
            {
                _timeInSecondsForNextFoodGeneration += _secondsToGenerateFood;
                
                // if there is room add a food
                base.HexTile.AddResource(new Resource(ResourceType.Food));
            }
        }
    }
}
