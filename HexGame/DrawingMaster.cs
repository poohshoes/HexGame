using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HexGame
{
    /// <summary>
    /// DrawingMaster is responsible for drawing the world.
    /// </summary>
    class DrawingMaster
    {
        World _world;

        Texture2D _hexTexture;
        readonly int _hexTextureWidth;
        readonly int _hexTextureHeight;

        const int _hexWidthPixels = 80;
        const int _hexHeightPixels = 50;

        const int _hexXOverlapPixels = 20;

        readonly Vector2 _worldOffset = new Vector2(10, 10);

        SpriteBatch _spriteBatch;

        public DrawingMaster(Game game, World world) 
        {
            _world = world;

            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            _hexTexture = game.Content.Load<Texture2D>("hexGrass");
            _hexTextureWidth = _hexTexture.Width;
            _hexTextureHeight = _hexTexture.Height;
        }

        public void DrawWorld() 
        {
            _spriteBatch.Begin();

            _drawMapTiles();

            _spriteBatch.End();
        }

        void _drawMapTiles() 
        {
            for (int i = 0; i < _world.Width; i++)
            {
                for (int j = 0; j < _world.Height; j++)
                {
                    _spriteBatch.Draw(
                        _hexTexture,
                        new Vector2
                            (
                                i * (_hexWidthPixels - _hexXOverlapPixels),
                                j * _hexHeightPixels + (i % 2 == 1 ? _hexHeightPixels/2 : 0)
                            )
                            + _worldOffset,
                        Color.White
                        );
                }
            }
        }
    }
}
