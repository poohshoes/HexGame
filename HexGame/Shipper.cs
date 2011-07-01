using System.Collections.Generic;
using System.Linq;

namespace HexGame
{
    class Shipper : MapItem
    {
        private readonly ResourceType _shippedResourceType;
        private readonly Hex _resourceSourceTile;
        private readonly Hex _resourceDestinationTile;
        private readonly PathFinding _pathFinding;

        private bool IsAtDestinationTile
        {
            get { return base.HexTile == _resourceDestinationTile; }
        }

        private bool IsAtSourceTile
        {
            get { return base.HexTile == _resourceSourceTile; }
        }

        private Resource _carriedResource;
        private Path _path;
        
        public Shipper(IntVector2 startingPoisition, Hex resourceSourceTile, Hex resourceDestinationTile, ResourceType shippedResourceType, World world)
            : base(startingPoisition, world)
        {
            _resourceSourceTile = resourceSourceTile;
            _resourceDestinationTile = resourceDestinationTile;
            _shippedResourceType = shippedResourceType;

            _pathFinding = new PathFinding(world);
        }

        public override void Update(double totalGameSeconds)
        {
            if (IsAtSourceTile)
            {
                PickUpResource();
                RecalculatePath(_resourceSourceTile.MapQuoordinate, _resourceDestinationTile.MapQuoordinate);
            }
            else if (IsAtDestinationTile)
            {
                DropResource();
                RecalculatePath(_resourceDestinationTile.MapQuoordinate, _resourceSourceTile.MapQuoordinate);
            }
            else
                MoveAlongPath();

            base.Update(totalGameSeconds);
        }

        private void RecalculatePath(IntVector2 startPosition, IntVector2 endPosition)
        {
            var positionPath = _pathFinding.AStar(startPosition, endPosition);
            var hexPath = positionPath.Select(v => World.GetHexAt(v));
            Stack<Hex> initialPath = new Stack<Hex>(hexPath);
            _path = new Path(initialPath);
        }

        private void MoveAlongPath()
        {
            var nextTile = _path.GetNextMapTile();
            if (nextTile == null)
                return;

            base.HexQuoordinates = nextTile.MapQuoordinate;
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
