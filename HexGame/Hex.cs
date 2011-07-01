using System.Collections.Generic;
using System.Diagnostics;

namespace HexGame
{
    /// <summary>
    /// A map tile
    /// </summary>
    class Hex
    {
        public List<Resource> Resources 
        {
            get 
            {
                return _resources;
            }
        }
        List<Resource> _resources;
        int _maxResources;
        public bool IsAtResourceCapacity 
        {
            get 
            {
                return (_resources.Count == _maxResources);
            }
        }

        public IntVector2 MapQuoordinate { get; private set; }

        // THIS SHOULD ALWAYS BE AT LEAST 1 TO SATISFY PATHFINDING
        public double MoveCost { private set; get; }

        public Hex(IntVector2 mapQuoordinate) 
        {
            MapQuoordinate = mapQuoordinate;
            _maxResources = 5;
            _resources = new List<Resource>(5);
            MoveCost = 1;
        }

        public int numResources() 
        {
            return Resources.Count;
        }

        /// <summary>
        /// As a side affect excess resources may be destroyed.
        /// </summary>
        public void setMaxResources(int newMaxResources) 
        {
            _maxResources = newMaxResources;

            if (_maxResources < _resources.Count) 
            {
                _resources.RemoveRange(_maxResources, _resources.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>False if there is no room to add the resource.</returns>
        public bool AddResource(Resource toAdd) 
        {
            Debug.Assert(toAdd != null, "Can't add a resource that is null.");

            if (_resources.Count >= _maxResources)
                return false;

            _resources.Add((Resource)toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>False if the resource can not be removed or does not exist in the list.</returns>
        public bool RemoveResource(Resource toRemove)
        {
            Debug.Assert(toRemove != null, "Can't remove a resource that is null.");

            return _resources.Remove((Resource)toRemove);
        }
    }
}
