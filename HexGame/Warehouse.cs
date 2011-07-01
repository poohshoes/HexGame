namespace HexGame
{
    class Warehouse : Building
    {
        public Warehouse(Hex hex, World world)
            : base(BuildingType.Warehouse, hex, world)
        {
            // warehouses increase the number of resources a hex can hold
            hex.setMaxResources(20);
        }
    }
}
