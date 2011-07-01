using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class Path
    {
        public double Length 
        {
            get { return _calculatePathLength(); }
        }

        //public int NumHexes
        //{
        //    get { return _path.Count; }
        //}

        public bool Empty 
        {
            get { return _path.Count == 0; }
        }

        Stack<Hex> _path = null;

        public Path(Stack<Hex> initialPath) 
        {
            _path = initialPath;
        }

        /// <summary>
        /// Move cost is calcualted by talking half of the move cost of the hex you are on
        /// and adding it to half of the move cost on the square you are moving to.
        /// </summary>
        /// <returns></returns>
        private double _calculatePathLength() 
        {
            if (_path == null || _path.Count == 0)
                return 0;

            // Just get all of the move costs and then subract the cost of 
            // the first hex and the last hex (unless there is only 1 hex).
            double pathLength = _path.Sum(hex => hex.MoveCost);
            pathLength -= _path.First().MoveCost / 2;
            if (_path.Count > 1)
                pathLength -= _path.Last().MoveCost / 2;
            return pathLength;
        }

        internal IntVector2 Pop()
        {
            throw new NotImplementedException();
        }
    }
}
