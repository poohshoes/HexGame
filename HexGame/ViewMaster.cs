using System;
using System.Collections.Generic;
using System.Linq;
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

        Image _farmTexture;
        Image _forestTexture;
        Image _foodTexture;
        Image _hexHighlightTexture;
        Image _hexTexture;
        Image _infantryTexture;
        Image _lumberTexture;
        Image _shipperTexture;
        Image _warehouseTexture;

        readonly IntVector2 _hexTextureDims;
        const int _hexXOverlapPixels = 20;

        readonly IntVector2 _worldScreenOffset = new IntVector2(10, 10);

        SpriteBatch _spriteBatch;

        public ViewMaster(Game game, World world) 
        {
            _world = world;

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _foodTexture = new Image(game.Content.Load<Texture2D>("resourceFood"));
            _lumberTexture = new Image(game.Content.Load<Texture2D>("resourceLumber"));
            _hexHighlightTexture = new Image(game.Content.Load<Texture2D>("hexHighlight"));

            _farmTexture = new Image(game.Content.Load<Texture2D>("farm"), new IntVector2(25, 0));
            _warehouseTexture = new Image(game.Content.Load<Texture2D>("warehouse"), new IntVector2(20, 5));

            _infantryTexture = new Image(game.Content.Load<Texture2D>("infantry"), new IntVector2(15, -10));

            _shipperTexture = new Image(game.Content.Load<Texture2D>("shipper"), new IntVector2(0, -5));
            _forestTexture = new Image(game.Content.Load<Texture2D>("forest"));
            _hexTexture = new Image(game.Content.Load<Texture2D>("hexGrass"));
            _hexTextureDims.X = _hexTexture.Texture.Width;
            _hexTextureDims.Y = _hexTexture.Texture.Height;

        }

        public void DrawWorld() 
        {
            _spriteBatch.Begin();

            _drawMapTiles();
            _drawResourseTiles();
            _drawHexSelection();
            _drawResources();
            _drawBuildings();
            _drawUnits();

            _spriteBatch.End();
        }

        /// <summary>
        /// Gets the Hex that us under the screen position supplied.
        /// </summary>
        /// <param name="positionOnScreen"></param>
        /// <param name="hex"></param>
        /// <returns>True if hex was found, false otherwise.</returns>
        public bool GetHexFromScreenPosition(IntVector2 positionOnScreen, out IntVector2 hex)
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
                        (_foodTexture.Texture.Width / 2) + i * 4,
                        (_hexTextureDims.Y / 2) - 4 + i * 5
                        );

                    drawLocation += _getScreenPositionOfHex(h.MapQuoordinate).ToVector2();

                    Image resourceImage = _getResourceImage(h.Resources[i].ResourceType);
                    _spriteBatch.Draw(
                        resourceImage.Texture,
                        drawLocation,
                        Color.White
                        );
                }
            }
        }

        void _drawHexSelection() 
        {
            if (_world.SelectedHex != null) 
            {
                _spriteBatch.Draw(
                        _hexHighlightTexture.Texture,
                        _getScreenPositionOfHex(_world.SelectedHex.Value).ToVector2(),
                        Color.White
                        );
            }
        }

        void _drawBuildings() 
        {
            foreach (Building b in _world.MapItems.Where(x => x is Building))
            {
                Image buildingImage = _getBuildingImage(b.BuildingType);
                _spriteBatch.Draw(
                        buildingImage.Texture,
                        _getScreenPositionOfMapItem(b, buildingImage).ToVector2(),
                        Color.White
                        );
            }
        }

        void _drawUnits()
        {
            foreach (Unit u in _world.MapItems.Where(x => x is Unit))
            {
                Image unitImage = _getUnitImage(u.UnitType);
                _spriteBatch.Draw(
                        unitImage.Texture,
                        _getScreenPositionOfMapItem(u, unitImage).ToVector2(),
                        Color.White
                        );
            }

            foreach (var shipper in _world.MapItems.OfType<Worker>())
                _spriteBatch.Draw(_shipperTexture.Texture, _getScreenPositionOfMapItem(shipper, _shipperTexture).ToVector2(), Color.White);
        }

        void _drawMapTiles() 
        {
            for (int x = 0; x < _world.MapSize.X; x++)
            {
                for (int y = 0; y < _world.MapSize.Y; y++)
                {
                    _spriteBatch.Draw(
                        _hexTexture.Texture,
                        _getScreenPositionOfHex(new IntVector2(x, y)).ToVector2(),
                        Color.White
                        );
                }
            }
        }

        void _drawResourseTiles()
        {
            foreach (var resourceHex in _world.MapItems.OfType<ResourceHex>())
            {
                Image resourceHexImage = _getResourceHexImage(resourceHex.Type);
                _spriteBatch.Draw(
                        resourceHexImage.Texture,
                        _getScreenPositionOfMapItem(resourceHex, resourceHexImage).ToVector2(),
                        Color.White
                        );
            }
        }

        private Image _getResourceImage(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Lumber:
                    return _lumberTexture;
                case ResourceType.Food:
                    return _foodTexture;
            }
            throw new Exception("Resource texture not supplied.");
        }

        private Image _getResourceHexImage(ResourceHexType type)
        {
            switch (type)
            {
                case ResourceHexType.Forest:
                    return _forestTexture;
            }
            throw new Exception("ResourceHex texture not supplied.");
        }

        Image _getBuildingImage(BuildingType building)
        {
            switch (building) 
            {
                case BuildingType.Warehouse:
                    return _warehouseTexture;
                case BuildingType.Farm:
                    return _farmTexture;
            }
            throw new Exception("Building texture not supplied.");
        }

        Image _getUnitImage(UnitType unit)
        {
            switch(unit)
            {
                case UnitType.Infantry:
                    return _infantryTexture;
            }
            throw new Exception("Unit texture not supplied.");
        }

        IntVector2 _getScreenPositionOfMapItem(MapItem mapItem, Image mapItemImage) 
        {
            return _getScreenPositionOfHex(mapItem.HexQuoordinates) + mapItemImage.DrawOffset;
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
