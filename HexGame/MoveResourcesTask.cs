using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class MoveResourcesTask : Task
    {
        private int _currentTaskIndex;
        private readonly List<Task> _tasks = new List<Task>();
        private Task _currentTask;

        private readonly Hex _resourceSourceTile;
        private readonly Hex _resourceDestinationTile;

        public MoveResourcesTask(Worker worker, Hex resourceSourceTile, Hex resourceDestinationTile, ResourceType shippedResourceType) : base(worker)
        {
            _resourceSourceTile = resourceSourceTile;
            _resourceDestinationTile = resourceDestinationTile;

            var moveToResourceSourceTileTask = new MoveToHexTask(worker, _resourceSourceTile);
            var moveToDestinationTileTask = new MoveToHexTask(worker, _resourceDestinationTile);
            var pickUpResourceTask = new PickUpResourcesTask(worker, shippedResourceType);
            var dropResourceTask = new DropResourcesTask(worker);

            _tasks = new List<Task>()
            {
                moveToResourceSourceTileTask,
                pickUpResourceTask,
                moveToDestinationTileTask,
                dropResourceTask
            };
            _currentTask = _tasks[0];
            _currentTaskIndex = 0;
        }

        public override bool PerformTask(double gameTimeInSeconds)
        {
            if (_currentTask.PerformTask(gameTimeInSeconds))
            {
                _currentTaskIndex++;
                if (_currentTaskIndex == _tasks.Count)
                    _currentTaskIndex = 0;

                _currentTask = _tasks[_currentTaskIndex];
            }

            return false;
        }
    }
}
