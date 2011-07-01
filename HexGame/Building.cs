namespace HexGame
{
    public enum BuildingTypes
    {
        Farm,
        Warehouse,
        Houseing
    }

    abstract class Building : MapItem
    {
        public BuildingTypes BuildingType { get; private set; }

        public Building(BuildingTypes buildingType, Hex hex, World world)
            : base(hex.MapQuoordinate, world)
        {
            BuildingType = buildingType;
        }
    }
}
