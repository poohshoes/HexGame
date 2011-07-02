namespace HexGame
{
    enum ResourceHexType
    {
        Forest
    }

    class ResourceHex : MapItem
    {
        public ResourceHexType Type { get; private set; }

        public int amountRemaining { get; private set; }

        public ResourceHex(ResourceHexType type, int resourceQuantity, IntVector2 hexQuoords, World world)
            : base(hexQuoords, world)
        {
            Type = type;
            amountRemaining = amountRemaining;
        }
    }
}
