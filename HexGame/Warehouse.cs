using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Warehouse : Building
    {
        public Warehouse(IntVector2 farmLocation, Hex hex)
            : base(farmLocation, BuildingTypes.Warehouse, hex)
        {
            // warehouses increase the number of resources a hex can hold
            hex.setMaxResources(20);
        }
    }
}
