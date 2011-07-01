using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum ResourceType { Food };

    class Resource
    {
        public readonly ResourceType ResourceType;

        public Resource(ResourceType resourceType) 
        {
            ResourceType = resourceType;
        }
    }
}
