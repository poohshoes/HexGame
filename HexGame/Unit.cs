using System;

namespace HexGame
{
    public enum UnitType
    {
        Infantry
    }

    class Unit : MobileMapItem
    {
        public UnitType UnitType { get; private set; }

        //public Hex DestinationTile
        //{
        //    get { return base.DestinationTile;  }
        //    set { base.DestinationTile = value;  }
        //}

        public Unit(UnitType unittype, IntVector2 hexQuoords, World world) 
            : base(hexQuoords, world)
        {
            this.UnitType = unittype;
        }
    }
}
