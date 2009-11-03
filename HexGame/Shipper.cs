using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Shipper : MapItem
    {
        bool _hasResource = false;
        Resources _resource;

        public Shipper(IntVector2 startingPoisition)
            : base(startingPoisition)
        {

        }
    }
}
