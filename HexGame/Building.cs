using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum BuildingTypes
    {
        Farm,
        Warehouse,
        Houseing
    }

    class Building : MapItem
    {
        public BuildingTypes BuildingType { get; private set; }

        public Building(int x, int y, BuildingTypes buildingType) 
            :base(x, y)
        {
            BuildingType = buildingType;
        }
    }
}
