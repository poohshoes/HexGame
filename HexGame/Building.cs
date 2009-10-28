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

        protected Hex _hex;

        public Building(IntVector2 buildingHexQuoord, BuildingTypes buildingType, Hex hex)
            : base(buildingHexQuoord)
        {
            _hex = hex;
            BuildingType = buildingType;
        }
    }
}
