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

        Texture2D _hexHighlightTexture;
        Texture2D _farmTexture;

        Texture2D _hexTexture;
        readonly Vector2 _hexTextureDims;

        //const int _hexWidthPixels = 80;
        //const int _hexHeightPixels = 50;

        const int _hexXOverlapPixels = 20;

        readonly Vector2 _worldScreenOffset = new Vector2(10, 10);
        readonly Vector2 _buildingOffsetFromHex = new Vector2(20, 5);

        SpriteBatch _spriteBatch;

        public ViewMaster(Game game, World world, MouseInputHandler mouseInputHandler) 
        {
            _world = world;

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _hexHighlightTexture = game.Content.Load<Texture2D>("hexHighlight");
            _farmTexture = game.Content.Load<Texture2D>("farm");

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
            _drawBuildings();

            _spriteBatch.End();
        }

        public void LeftMouseClickBehaviour(Vector2 mousePosition) 
        {
            Vector2 mouseRelativeToWorld = mousePosition - _worldScreenOffset;

            // find the most likely hex at that mouse X (could still be one to the left or right)
            Vector2 suspectHex = new Vector2(0, 0);
            suspectHex.X = (float)Math.Floor(mouseRelativeToWorld.X / (_hexTextureDims.X - _hexXOverlapPixels));

            suspectHex.Y = _hexIsSunken(Convert.ToInt32(suspectHex.X)) ?
                (float)Math.Floor((mouseRelativeToWorld.Y - (_hexTextureDims.Y/2)) / _hexTextureDims.Y) : 
                (float)Math.Floor(mouseRelativeToWorld.Y / _hexTextureDims.Y);

            // object clicked on was not a hex
            if(!_world.IsValidHexQuoord(suspectHex))
                return;

            // get all the the hexes it could potentially be (thse form a bone shape around the suspect hex)
            List<Vector2> potentialHexes = _getBoneShapeHexLocations(suspectHex);

            // the middleHex has already been evaluated so we should have at least one potential hex
            Debug.Assert(potentialHexes.Count != 0);

            // now we just need to find the hex that is closest to the mouse and that is the one that was clicked on
            Vector2 closestToMouseHex = suspectHex;
            float distanceToMouse = 999999;

            foreach (Vector2 v in potentialHexes) 
            {
                float testDistance = (_getHexCenter(v) - mouseRelativeToWorld).Length();
                if(testDistance < distanceToMouse)
                {
                    distanceToMouse = testDistance;
                    closestToMouseHex = v;
                }
            }

            _world.SelectedHex = closestToMouseHex;
        }

        Vector2 _getHexCenter(Vector2 hexQuoords) 
        {
            Vector2 center = _hexTextureDims / 2;
            center.Y += hexQuoords.Y * _hexTextureDims.Y;
            center.X += hexQuoords.X * (_hexTextureDims.X - _hexXOverlapPixels);

            if (_hexIsSunken(hexQuoords.X))
                center.Y += _hexTextureDims.Y / 2;

            return center;
        }

        /// <summary>
        /// The bone shape is like this:
        ///  _   _
        /// / \_/ \
        /// \_/ \_/
        /// / \_/ \
        /// \_/ \_/
        /// </summary>
        /// <param name="middleHex">The hex that will be the middle bone.</param>
        /// <returns>A list of the VALID hex quoordinates in the bone.</returns>
        List<Vector2> _getBoneShapeHexLocations(Vector2 middleHex) 
        {
            List<Vector2> unvalidatedBoneHexes = new List<Vector2>();

            unvalidatedBoneHexes.Add(middleHex);
            unvalidatedBoneHexes.Add(new Vector2(middleHex.X - 1, middleHex.Y));
            unvalidatedBoneHexes.Add(new Vector2(middleHex.X + 1, middleHex.Y));

            if (_hexIsSunken(middleHex.X)) 
            {
                unvalidatedBoneHexes.Add(new Vector2(middleHex.X - 1, middleHex.Y+1));
                unvalidatedBoneHexes.Add(new Vector2(middleHex.X + 1, middleHex.Y+1));
            }
            else
            {
                unvalidatedBoneHexes.Add(new Vector2(middleHex.X - 1, middleHex.Y-1));
                unvalidatedBoneHexes.Add(new Vector2(middleHex.X + 1, middleHex.Y-1));
            }

            List<Vector2> validatedBoneHexes = new List<Vector2>();

            foreach (Vector2 v in unvalidatedBoneHexes) 
            {
                if (_world.IsValidHexQuoord(v))
                    validatedBoneHexes.Add(v);
            }

            return validatedBoneHexes;
        }

        void _drawHexSelection() 
        {
            if (_world.HexIsSelected) 
            {
                _spriteBatch.Draw(
                        _hexHighlightTexture,
                        _getScreenPositionOfHex(_world.SelectedHex),
                        Color.White
                        );
            }
        }

        void _drawBuildings() 
        {
            foreach (Building b in _world.MapItems) 
            {
                _spriteBatch.Draw(
                        _farmTexture,
                        _getScreenPositionOfBuilding(b.hexQuoordinates),
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
                        _getScreenPositionOfHex(new Vector2(x, y)),
                        Color.White
                        );
                }
            }
        }

        Vector2 _getScreenPositionOfBuilding(Vector2 buildingQuoords) 
        {
            return _getScreenPositionOfHex(buildingQuoords) + _buildingOffsetFromHex;
        }

        Vector2 _getScreenPositionOfHex(Vector2 hexQuoords) 
        {
            return new Vector2
            (
                hexQuoords.X * (_hexTextureDims.X - _hexXOverlapPixels),
                hexQuoords.Y * _hexTextureDims.Y + (_hexIsSunken(hexQuoords.X) ? _hexTextureDims.Y / 2 : 0)
            ) + _worldScreenOffset;
        }

        /// <summary>
        /// Every some hexes are sunk so that they line up.
        /// </summary>
        /// <param name="x">The hexes x quoordinate.</param>
        /// <returns>True if the hex is sunken.</returns>
        bool _hexIsSunken(int x) 
        {
            return x % 2 == 1;
        }

        bool _hexIsSunken(float x) 
        {
            return _hexIsSunken(Convert.ToInt32(x));
        }
    }
}
