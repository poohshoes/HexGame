using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum Resources{ Food };
    class Hex
    {
        public List<Resources> Resources { get; private set; }
        int _maxResources;
        public IntVector2 MapQuoordinate { get; private set; }

        public Hex(IntVector2 mapQuoordinate) 
        {
            MapQuoordinate = mapQuoordinate;
            _maxResources = 5;
            Resources = new List<Resources>(5);
        }

        public int numResources() 
        {
            return Resources.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>False if there is no room to add the resource.</returns>
        public bool AddResource(Resources toAdd) 
        {
            if (Resources.Count >= _maxResources)
                return false;

            Resources.Add(toAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns>False if the resource can not be removed or does not exist in the list.</returns>
        public bool RemoveResource(Resources toRemove)
        {
            return Resources.Remove(toRemove);
        }
    }
}
