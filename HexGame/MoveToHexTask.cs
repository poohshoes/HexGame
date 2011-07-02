using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class MoveToHexTask : Task
    {
        private readonly MobileMapItem _mobileMapItem;

        private readonly Hex _target;
        private readonly PathFinding _pathFinding;
        private Path _path;

        private bool IsAtTargetTile
        {
            get { return _mobileMapItem.HexTile == _target; }
        }

        private double _lastMoveTime;


        public MoveToHexTask(MobileMapItem mobileMapItem, Hex target)
            : base(mobileMapItem)
        {
            _mobileMapItem = mobileMapItem;
            _target = target;
            _pathFinding = new PathFinding(mobileMapItem.World);
            RecalculatePath();
        }

        public override bool PerformTask(double totalGameSeconds)
        {
            if (IsAtTargetTile)
                return true;

            if ((totalGameSeconds - _lastMoveTime) >= _mobileMapItem.MoveIntervalInSeconds)
            {
                RecalculatePath();
                MoveAlongPath();
                _lastMoveTime = totalGameSeconds;
            }

            return false;
        }

        private void RecalculatePath()
        {
            var positionPath = _pathFinding.AStar(_mobileMapItem.HexTile.MapQuoordinate, _target.MapQuoordinate);
            var hexPath = positionPath.Select(v => World.GetHexAt(v)).Reverse();
            Stack<Hex> initialPath = new Stack<Hex>(hexPath);
            _path = new Path(initialPath);
        }

        private void MoveAlongPath()
        {
            var nextTile = _path.GetNextMapTile();
            if (nextTile == null)
                return;

            _mobileMapItem.HexQuoordinates = nextTile.MapQuoordinate;
        }
    }
}
