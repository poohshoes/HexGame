using System;
using System.Collections.Generic;

namespace HexGame
{
    public enum UnitType
    {
        Infantry
    }

    class Unit : MobileMapItem
    {
        public UnitType UnitType { get; private set; }

        public int HitPoints { get; private set; }

        public Boolean isDead
        {
            get { return HitPoints <= 0; }
        }

        public Hex DestinationTile
        {
            get { return base.DestinationTile;  }
            set { base.DestinationTile = value;  }
        }

        public Unit(UnitType unittype, IntVector2 hexQuoords, World world) 
            : base(hexQuoords, world)
        {
            this.UnitType = unittype;
            HitPoints = 4;
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);

            Unit neighourUnit = base.World.firstNeighbouringUnit(base.HexTile.MapQuoordinate);

            if (neighourUnit != null)
                neighourUnit.ChangeHitPoints(-1);
        }

        private void ChangeHitPoints(int hitPointChange)
        {
            HitPoints += hitPointChange;
            if (isDead)
                base.IsMoveEnabled = false;
        }
    }
}
