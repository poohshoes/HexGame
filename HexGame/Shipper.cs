using System.Linq;

namespace HexGame
{
    class Shipper : MobileMapItem
    {
        private readonly ResourceType _shippedResourceType;
        private readonly Hex _resourceSourceTile;
        private readonly Hex _resourceDestinationTile;

        private Resource _carriedResource;

        private bool IsAtResourceDestinationTile
        {
            get { return base.HexTile == _resourceDestinationTile; }
        }

        private bool IsAtResourceSourceTile
        {
            get { return base.HexTile == _resourceSourceTile; }
        }

        public Shipper(IntVector2 startingPoisition, Hex resourceSourceTile, Hex resourceDestinationTile, ResourceType shippedResourceType, World world)
            : base(startingPoisition, world)
        {
            _resourceSourceTile = resourceSourceTile;
            _resourceDestinationTile = resourceDestinationTile;
            _shippedResourceType = shippedResourceType;
        }

        protected override void OnArrivedAtDestination()
        {
            base.OnArrivedAtDestination();

            if (IsAtResourceSourceTile)
            {
                PickUpResource();
                base.DestinationTile = _resourceDestinationTile;
            }
            else if (IsAtResourceDestinationTile)
            {
                DropResource();
                base.DestinationTile = _resourceSourceTile;
            }
        }

        private void PickUpResource()
        {
            var tileResource = base.HexTile.Resources.FirstOrDefault(r => r.ResourceType == _shippedResourceType);
            if (tileResource == null)
                return;

            bool pickedUpResource = _resourceDestinationTile.RemoveResource(tileResource);
            if (!pickedUpResource)
                return;

            _carriedResource = tileResource;
        }

        private void DropResource()
        {
            if (_carriedResource == null)
                return;

            base.HexTile.AddResource(_carriedResource);
            _carriedResource = null;
        }
    }
}
