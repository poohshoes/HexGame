﻿using System.Collections.Generic;
using System.Linq;

namespace HexGame
{
    /// <summary>
    /// Automatically moves from current position to destination tile
    /// </summary>
    class MobileMapItem : MapItem
    {
        private readonly PathFinding _pathFinding;
        private Path _path;

        private bool IsAtDestinationTile
        {
            get { return base.HexTile == DestinationTile; }
        }

        private Hex _destinationTile;
        protected Hex DestinationTile
        {
            get { return _destinationTile; }
            set
            {
                if (_destinationTile == value)
                    return;

                _destinationTile = value;
                RecalculatePath();
            }
        }

        /// <summary>
        /// Determines if the unit will automatically move from the source to the destination.
        /// </summary>
        protected bool IsMoveEnabled { get; set; }

        public MobileMapItem(IntVector2 hexQuoords, World world)
            : base(hexQuoords, world)
        {
            _pathFinding = new PathFinding(world);
            IsMoveEnabled = true;
        }

        private void RecalculatePath()
        {
            var positionPath = _pathFinding.AStar(base.HexTile.MapQuoordinate, DestinationTile.MapQuoordinate);
            var hexPath = positionPath.Select(v => World.GetHexAt(v)).Reverse();
            Stack<Hex> initialPath = new Stack<Hex>(hexPath);
            _path = new Path(initialPath);
        }

        private void MoveAlongPath()
        {
            var nextTile = _path.GetNextMapTile();
            if (nextTile == null)
                return;

            base.HexQuoordinates = nextTile.MapQuoordinate;
            
            if (base.HexTile == DestinationTile)
                OnArrivedAtDestination();
        }

        private readonly double _moveInterval = 0.25;
        private double _lastMoveTime;

        public override void Update(double totalGameSeconds)
        {
            base.Update(totalGameSeconds);
            if (IsMoveEnabled && DestinationTile != null && (totalGameSeconds - _lastMoveTime) >= _moveInterval)
            {
                MoveAlongPath();
                RecalculatePath();
                _lastMoveTime = totalGameSeconds;
            }
        }

        protected virtual void OnArrivedAtDestination()
        {
        }
    }
}
