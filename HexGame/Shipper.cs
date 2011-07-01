namespace HexGame
{
    class Shipper : MobileMapItem
    {
        Resource _resource;

        public Shipper(IntVector2 startingPoisition, World world)
            : base(startingPoisition, world)
        {
        }

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);
        }

        private void MoveAlongPath()
        {
            //HexQuoordinates = _path.Pop();
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
            _world.addResource(this.HexQuoordinates, _resource);
            _resource = null;
        }

        private void GetResource()
        {
            
        }
    }
}
