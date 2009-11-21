using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HexGame
{
    public enum BuildingTypes
    {
        Farm,
        Warehouse,
        Houseing
    }

    class Building : MapItem
    {
        public BuildingTypes BuildingType { get; private set; }

        public Hex hex { get; private set; }

        public Building(IntVector2 buildingHexQuoord, BuildingTypes buildingType, Hex hex)
            : base(buildingHexQuoord)
        {
            this.hex = hex;
            BuildingType = buildingType;
        }
    }
}
