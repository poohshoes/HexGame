namespace HexGame
{
    enum ResourceType
    {
        Food,
        Lumber
    };

    class Resource
    {
        public readonly ResourceType ResourceType;

        public Resource(ResourceType resourceType) 
        {
            ResourceType = resourceType;
        }
    }
}
