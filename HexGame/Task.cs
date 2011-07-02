using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    enum TaskType { CollectResource };

    abstract class Task
    {
        private readonly MapItem _mapItem;
        protected MapItem MapItem
        {
            get { return _mapItem; }
        }

        protected World World { get { return _mapItem.World; } }

        public Task(MapItem mapItem)
        {
            _mapItem = mapItem;
        }

        // True, if the task is complete; otherwise false.
        public abstract bool PerformTask(double totalGameSeconds);
    }
}
