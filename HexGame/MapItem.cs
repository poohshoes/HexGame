using System;
using System.Collections.Generic;

namespace HexGame
{
    abstract class MapItem
    {
        public World World { get; private set; }

        public IntVector2 HexQuoordinates { get; protected set; }

        public Queue<Task> Tasks { get; private set; }
        private Task _currentTask;

        /// <summary>
        /// The tile the map item is currently on
        /// </summary>
        public Hex HexTile
        {
            get { return World.GetHexAt(HexQuoordinates); }
        }

        protected MapItem(IntVector2 hexQuoords, World world) 
        {
            HexQuoordinates = hexQuoords;
            World = world;

            Tasks = new Queue<Task>();
        }

        virtual public void Update(double totalGameSeconds) 
        {
            if (_currentTask != null)
            {
                var completedTask = _currentTask.PerformTask(totalGameSeconds);
                if (completedTask)
                    _currentTask = null;
            }
            else
            {
                if (Tasks.Count == 0)
                    return;
                _currentTask = Tasks.Dequeue();
            }
        }
    }
}
