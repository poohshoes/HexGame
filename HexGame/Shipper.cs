namespace HexGame
{
    class Shipper : MobileMapItem
    {
        Resource? _resource;

        public Shipper(IntVector2 startingPoisition, World world)
            : base(startingPoisition, world)
        {
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

        private void _haveArrivedAtDestination()
        {
            if (_resource == null)
                _dropResource();
            else
                _getResource();
        }

        private void _dropResource()
        {
            _world.addResource(this.HexQuoordinates, _resource);
            _resource = null;
        }

        private void _getResource()
        {
            
        }
    }
}
