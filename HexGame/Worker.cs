using System.Linq;

namespace HexGame
{
    class Worker : MobileMapItem
    {
        public Resource CarriedResource { get; set; }

        public Worker(Hex startingHex, World world)
            : base(startingHex.MapQuoordinate, world)
        {
        }
    }
}
