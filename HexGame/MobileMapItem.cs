using System.Collections.Generic;

namespace HexGame
{
    /// <summary>
    /// Automatically moves from current position to destination tile
    /// </summary>
    class MobileMapItem : MapItem
    {
        public double MoveIntervalInSeconds { get; private set; }

        public MobileMapItem(IntVector2 hexQuoords, World world)
            : base(hexQuoords, world)
        {
            MoveIntervalInSeconds = 0.25;
        }

        public new IntVector2 HexQuoordinates
        {
            get { return base.HexQuoordinates; }
            set { base.HexQuoordinates = value; }
        }
    }
}
