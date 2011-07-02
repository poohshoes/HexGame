using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class PickUpResourcesTask : Task
    {
        private ResourceType _shippedResourceType;

        public PickUpResourcesTask(Worker worker, ResourceType shippedResourceType)
            : base(worker)
        {
            _shippedResourceType = shippedResourceType;
        }

        public override bool PerformTask(double totalGameSeconds)
        {
            return TryPickUpResource();
        }

        private bool TryPickUpResource()
        {
            var tileResource = base.MapItem.HexTile.Resources.FirstOrDefault(r => r.ResourceType == _shippedResourceType);
            if (tileResource == null)
                return false;

            bool pickedUpResource = base.MapItem.HexTile.RemoveResource(tileResource);
            if (!pickedUpResource)
                return false;

            ((Worker)base.MapItem).CarriedResource = tileResource;
            return true;
        }
    }
}
