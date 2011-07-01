using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Shipper : MapItem
    {
        Resource _resource;

        Path _path { get; set; }

        protected World _world;

        public Shipper(IntVector2 startingPoisition, World world)
            : base(startingPoisition)
        {
            _world = world;
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);
        }

        private void MoveAlongPath()
        {
            hexQuoordinates = _path.Pop();
        }

        private void HaveArrivedAtDestination() 
        {
            if (_resource == null)
                DropResource();
            else
                GetResource();
        }

        private void GetNewPath()
        {

        }

        private void DropResource()
        {
            _world.addResource(this.hexQuoordinates, _resource);
            _resource = null;
        }

        private void GetResource()
        {
            
        }
    }
}
