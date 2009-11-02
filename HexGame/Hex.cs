using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum Resources{ Food };
    class Hex
    {
        public List<Resources> Resources 
        {
            get 
            {
                return _resources;
            }
        }
        List<Resources> _resources;
        int _maxResources;
        public IntVector2 MapQuoordinate { get; private set; }

        // THIS SHOULD ALWAYS BE AT LEAST 1 TO SATISFY PATHFINDING
        public float MoveCost { private set; get; }

        public Hex(IntVector2 mapQuoordinate) 
        {
            MapQuoordinate = mapQuoordinate;
            _maxResources = 5;
            _resources = new List<Resources>(5);
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
        public bool AddResource(Resources toAdd) 
        {
            if (_resources.Count >= _maxResources)
                return false;

            _resources.Add(toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>False if the resource can not be removed or does not exist in the list.</returns>
        public bool RemoveResource(Resources toRemove)
        {
            return _resources.Remove(toRemove);
        }
    }
}
