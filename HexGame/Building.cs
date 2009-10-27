using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public Building(Vector2 buildingHexQuoord, BuildingTypes buildingType)
            : base(buildingHexQuoord)
        {
            BuildingType = buildingType;
        }
    }
}
