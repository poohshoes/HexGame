namespace HexGame
{
    public enum BuildingType
    {
        Farm,
        Warehouse,
        Houseing
    }

    abstract class Building : MapItem
    {
        public BuildingType BuildingType { get; private set; }

        public Building(BuildingType buildingType, Hex hex, World world)
            : base(hex.MapQuoordinate, world)
        {
            BuildingType = buildingType;
        }
    }
}
