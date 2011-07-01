using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HexGame
{
    /// <summary>
    /// DrawingMaster is responsible for drawing the world.
    /// </summary>
    class ViewMaster
    {
        World _world;

        Texture2D _foodTexture;
        Texture2D _hexHighlightTexture;
        Texture2D _farmTexture;
        Texture2D _warehouseTexture;

        Texture2D _hexTexture;
        readonly IntVector2 _hexTextureDims;

        const int _hexXOverlapPixels = 20;

        readonly IntVector2 _worldScreenOffset = new IntVector2(10, 10);
        readonly IntVector2 _buildingOffsetFromHex = new IntVector2(20, 5);

        SpriteBatch _spriteBatch;

        public ViewMaster(Game game, World world, MouseInputHandler mouseInputHandler) 
        {
            _world = world;

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _foodTexture = game.Content.Load<Texture2D>("resourceFood");
            _hexHighlightTexture = game.Content.Load<Texture2D>("hexHighlight");
            _farmTexture = game.Content.Load<Texture2D>("farm");
            _warehouseTexture = game.Content.Load<Texture2D>("warehouse");

            _hexTexture = game.Content.Load<Texture2D>("hexGrass");
            _hexTextureDims.X = _hexTexture.Width;
            _hexTextureDims.Y = _hexTexture.Height;

            mouseInputHandler.LeftMouseClick += LeftMouseClickBehaviour;
        }

        public void DrawWorld() 
        {
            _spriteBatch.Begin();

            _drawMapTiles();
            _drawHexSelection();
            _drawResources();
            _drawBuildings();

            _spriteBatch.End();
        }

        public void LeftMouseClickBehaviour(IntVector2 mousePosition) 
        {
            IntVector2 selectedHex;
            if (_getHexFromScreenPosition(mousePosition, out selectedHex))
              _world.SelectedHex = selectedHex;
        }

        /// <summary>
        /// Gets the Hex that us under the screen position supplied.
        /// </summary>
        /// <param name="positionOnScreen"></param>
        /// <param name="hex"></param>
        /// <returns>True if hex was found, false otherwise.</returns>
        bool _getHexFromScreenPosition(IntVector2 positionOnScreen, out IntVector2 hex)
        {
            // alows us to return early if no hex can be found.
            hex = IntVector2.Zero;

            IntVector2 mouseRelativeToWorld = positionOnScreen - _worldScreenOffset;

            // find the most likely hex at that mouse X (could still be one to the left or right)
            IntVector2 suspectHex = new IntVector2(0, 0);
            suspectHex.X = mouseRelativeToWorld.X / (_hexTextureDims.X - _hexXOverlapPixels);

            suspectHex.Y = _world.hexIsSunken(suspectHex.X) ?
                (mouseRelativeToWorld.Y - (_hexTextureDims.Y/2)) / _hexTextureDims.Y : 
                mouseRelativeToWorld.Y / _hexTextureDims.Y;

            // object clicked on was not a hex
            if(!_world.IsValidHexQuoord(suspectHex))
                return false;

            // get all the the hexes it could potentially be (thse form a bone shape around the suspect hex)
            List<IntVector2> potentialHexes = _world.getBoneShapeHexLocations(suspectHex);

            // the middleHex has already been evaluated so we should have at least one potential hex
            Debug.Assert(potentialHexes.Count != 0);

            // now we just need to find the hex that is closest to the mouse and that is the one that was clicked on
            IntVector2 closestToMouseHex = suspectHex;
            float distanceToMouse = 999999;

            foreach (IntVector2 v in potentialHexes) 
            {
                float testDistance = (_getHexCenter(v) - mouseRelativeToWorld).Length();
                if(testDistance < distanceToMouse)
                {
                    distanceToMouse = testDistance;
                    closestToMouseHex = v;
                }
            }

            hex = closestToMouseHex;
            return true;
        }

        IntVector2 _getHexCenter(IntVector2 hexQuoords) 
        {
            IntVector2 center = _hexTextureDims / 2;
            center.Y += hexQuoords.Y * _hexTextureDims.Y;
            center.X += hexQuoords.X * (_hexTextureDims.X - _hexXOverlapPixels);

            if (_world.hexIsSunken(hexQuoords.X))
                center.Y += _hexTextureDims.Y / 2;

            return center;
        }

        void _drawResources() 
        {
            List<Hex> hexesWithResources = _world.getHexesWithResources();
            foreach (Hex h in hexesWithResources) 
            {
                for (int i = 0; i < h.Resources.Count; i++) 
                {
                    Vector2 drawLocation = new Vector2(
                        (_foodTexture.Width / 2) + i * 4,
                        (_hexTextureDims.Y / 2) - 4 + i * 5
                        );

                    drawLocation += _getScreenPositionOfHex(h.MapQuoordinate).ToVector2();

                    _spriteBatch.Draw(
                        _foodTexture,
                        drawLocation,
                        Color.White
                        );
                }
            }
        }

        void _drawHexSelection() 
        {
            if (_world.HexIsSelected) 
            {
                _spriteBatch.Draw(
                        _hexHighlightTexture,
                        _getScreenPositionOfHex(_world.SelectedHex).ToVector2(),
                        Color.White
                        );
            }
        }

        void _drawBuildings() 
        {
            foreach (var b in _world.MapItems.OfType<Building>()) 
            {
                _spriteBatch.Draw(
                        _getBuildingTexture(b.BuildingType),
                        _getScreenPositionOfBuilding(b.HexQuoordinates).ToVector2(),
                        Color.White
                        );
            }
        }

        void _drawMapTiles() 
        {
            for (int x = 0; x < _world.MapSize.X; x++)
            {
                for (int y = 0; y < _world.MapSize.Y; y++)
                {
                    _spriteBatch.Draw(
                        _hexTexture,
                        _getScreenPositionOfHex(new IntVector2(x, y)).ToVector2(),
                        Color.White
                        );
                }
            }
        }

        Texture2D _getBuildingTexture(BuildingTypes building)
        {
            switch (building) 
            {
                case BuildingTypes.Warehouse:
                    return _warehouseTexture;
                case BuildingTypes.Farm:
                    return _farmTexture;
            }
            throw new Exception("Building texture not supplied.");
        }

        IntVector2 _getScreenPositionOfBuilding(IntVector2 buildingQuoords) 
        {
            return _getScreenPositionOfHex(buildingQuoords) + _buildingOffsetFromHex;
        }

        IntVector2 _getScreenPositionOfHex(IntVector2 hexQuoords) 
        {
            return new IntVector2
            (
                hexQuoords.X * (_hexTextureDims.X - _hexXOverlapPixels),
                hexQuoords.Y * _hexTextureDims.Y + (_world.hexIsSunken(hexQuoords.X) ? _hexTextureDims.Y / 2 : 0)
            ) + _worldScreenOffset;
        }
    }
}
