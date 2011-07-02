using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class DropResourcesTask : Task
    {
        public DropResourcesTask(Worker worker)
            : base(worker)
        {
        }

        public override bool PerformTask(double totalGameSeconds)
        {
            return TryDropResource();
        }

        private bool TryDropResource()
        {
            var carriedResource = ((Worker)base.MapItem).CarriedResource;

            if (carriedResource == null)
                return true;

            var droppedResource = base.MapItem.HexTile.AddResource(carriedResource);
            if (droppedResource)
                carriedResource = null;

            return droppedResource;
        }
    }
}
