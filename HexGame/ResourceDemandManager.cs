using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    /// <summary>
    /// The resource demand manager knows where resources need to be taken.
    /// You can ask it where to place excess resources.
    /// </summary>
    class ResourceDemandManager
    {
        List<Warehouse> warehouses;

        public ResourceDemandManager() 
        {
            warehouses = new List<Warehouse>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="pathToStorageLocation"></param>
        /// <param name="travelingFrom"></param>
        /// <returns>True if it finds a storeage location, false otherwise.</returns>
        public bool GetStorageLocation(ResourceTypes resourceType, IntVector2 travelingFrom, out Path pathToStorageLocation) 
        {
            throw new NotImplementedException();
            //List<Warehouse> warehousesWithRoom = warehouses.Where(warehouse => !warehouse.hex.IsAtResourceCapacity);

            //if (warehousesWithRoom.Count() == 0)
            //{
            //    pathToStorageLocation = new Path();
            //    return false;
            //}

            //Warehouse closestWarehouse = warehousesWithRoom[0];
            //Path closestWarehousePath = PathFinding.AStar(travelingFrom, warehousesWithRoom[0].hexQuoordinates);

            //// If there are performance issues we could use the heuristic instead of the whole path finding alg.
            //for (int i = 1; i < ) 
            //{
                
            //}
        }


    }
}
