using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Warehouse : Building
    {
        public Warehouse(Hex hex, World world)
            : base(BuildingTypes.Warehouse, hex, world)
        {
            // warehouses increase the number of resources a hex can hold
            hex.setMaxResources(20);
        }
    }
}
