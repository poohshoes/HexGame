using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum ResourceTypes { Food };
    struct Resource
    {
        ResourceTypes ResourceType;
        bool NeedsPickup;

        public Resource(ResourceTypes resourceType) 
        {
            NeedsPickup = true;
            ResourceType = resourceType;
        }
    }
}
