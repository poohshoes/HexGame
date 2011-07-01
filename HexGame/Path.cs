using System.Collections.Generic;

namespace HexGame
{
    class Path
    {
        private readonly Stack<Hex> _path;

        public Path(Stack<Hex> initialPath) 
        {
            _path = initialPath;
        }

        internal Hex GetNextMapTile()
        {
            if (_path.Count == 0)
                return null;

            return _path.Pop();
        }
    }
}
