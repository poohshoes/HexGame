namespace HexGame
{
    class MobileMapItem : MapItem
    {
        Path _path { get; set; }

        public MobileMapItem(IntVector2 hexQuoords, World world)
            : base(hexQuoords, world)
        {
        }

        private void _moveAlongPath()
        {
            //HexQuoordinates = _path.Pop();
        }

        private void _getNewPath()
        {

        }
    }
}
