using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Shipper : MapItem
    {
        Resource? _resource;

        Path _path { get; set; }

        // a reference to the world
        protected World _world;

        public Shipper(IntVector2 startingPoisition, World world)
            : base(startingPoisition)
        {
            _world = world;
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);

            //if (_path.Empty)
            //{
            //    _haveArrivedAtDestination();
            //}
            //else if (_path == null)
            //{
            //    _getNewPath();
            //}
            //else 
            //{
            //    _moveAlongPath();
            //}
        }

        private void _moveAlongPath()
        {
            hexQuoordinates = _path.Pop();
        }

        private void _haveArrivedAtDestination() 
        {
            if (_resource == null)
                _dropResource();
            else
                _getResource();
        }

        private void _getNewPath()
        {

        }

        private void _dropResource()
        {
            _world.addResource(this.hexQuoordinates, _resource);
            _resource = null;
        }

        private void _getResource()
        {
            
        }
    }
}
