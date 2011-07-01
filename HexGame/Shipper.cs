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

        public Shipper(Hex startingHex, Hex resourceSourceTile, Hex resourceDestinationTile, ResourceType shippedResourceType, World world)
            : base(startingHex.MapQuoordinate, world)
        {
            _resourceSourceTile = resourceSourceTile;
            _resourceDestinationTile = resourceDestinationTile;
            _shippedResourceType = shippedResourceType;

            base.DestinationTile = _resourceSourceTile;
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);

            if (IsAtResourceSourceTile)
            {
                var pickedUpResource = TryPickUpResource();
                if (pickedUpResource)
                {
                    base.DestinationTile = _resourceDestinationTile;
                    base.IsMoveEnabled = true;
                }
                else
                {
                    base.IsMoveEnabled = false;
                }
            }
            else if (IsAtResourceDestinationTile)
            {
                var droppedResource = TryDropResource();
                if (droppedResource)
                {
                    base.DestinationTile = _resourceSourceTile;
                    base.IsMoveEnabled = true;
                }
                else
                {
                    base.IsMoveEnabled = false;
                }
            }
        }


        private bool TryPickUpResource()
        {
            var tileResource = base.HexTile.Resources.FirstOrDefault(r => r.ResourceType == _shippedResourceType);
            if (tileResource == null)
                return false;

            bool pickedUpResource = base.HexTile.RemoveResource(tileResource);
            if (!pickedUpResource)
                return false;

            _carriedResource = tileResource;
            return true;
        }

        private bool TryDropResource()
        {
            if (_carriedResource == null)
                return true;

            var droppedResource = base.HexTile.AddResource(_carriedResource);
            if (droppedResource)
                _carriedResource = null;

            return droppedResource;
        }
    }
}
