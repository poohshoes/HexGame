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

        public Building(IntVector2 buildingHexQuoord, BuildingTypes buildingType, Hex hex, World world)
            : base(buildingHexQuoord, world)
        {
            this.hex = hex;
            BuildingType = buildingType;
        }
    }
}
