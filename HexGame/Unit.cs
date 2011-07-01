namespace HexGame
{
    public enum UnitType
    {
        Infantry
    }

    class Unit : MobileMapItem
    {
        public UnitType UnitType { get; private set; }

        public Unit(UnitType unittype, IntVector2 hexQuoords, World world) 
            : base(hexQuoords, world)
        {
            this.UnitType = unittype;
        }
    }
}
