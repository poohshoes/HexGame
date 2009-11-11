using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Shipper : MapItem
    {
        bool _hasResource = false;
        Resources _resource;

        Stack<IntVector2> _path { get; set; }

        // a reference to the world
        World _world;

        public Shipper(IntVector2 startingPoisition, World world)
            : base(startingPoisition)
        {
            _world = world;
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);

            if (_path.Count == 0)
            {
                haveArrivedAtDestination();
            }
            else if (_path == null)
            {
                getNewPath();
            }
            else 
            {
                moveAlongPath();
            }
        }

        private void moveAlongPath()
        {
            hexQuoordinates = _path.Pop();
        }

        private void haveArrivedAtDestination() 
        {
            if (_hasResource)
                dropResource();
            else
                getResource();
        }

        private void getNewPath()
        {

        }

        private void dropResource()
        {
            _hasResource = false;
        }

        private void getResource()
        {
            _hasResource = true;
        }
    }
}
