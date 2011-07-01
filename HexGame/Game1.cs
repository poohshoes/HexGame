using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HexGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ViewMaster _drawingMaster;
        World _world;
        MouseInputHandler _mouseInputHandler;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            _mouseInputHandler = new MouseInputHandler(this);

            _world = new World();
            _world.AddMapItem(new Farm(_world.GetHexAt(new IntVector2(4, 4)), _world));
            _world.AddMapItem(new Farm(_world.GetHexAt(new IntVector2(5, 3)), _world));
            _world.AddMapItem(new Farm(_world.GetHexAt(new IntVector2(6, 5)), _world));
            _world.AddMapItem(new Farm(_world.GetHexAt(new IntVector2(4, 5)), _world));
            _world.AddMapItem(new Farm(_world.GetHexAt(new IntVector2(5, 5)), _world));
            _world.AddMapItem(new Warehouse(_world.GetHexAt(new IntVector2(5, 4)), _world));
            _world.AddMapItem(new Unit(new IntVector2(6, 6), _world));

            _drawingMaster = new ViewMaster(this, _world, _mouseInputHandler);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _world.Update(gameTime.TotalGameTime.Seconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _drawingMaster.DrawWorld();

            base.Draw(gameTime);
        }
    }
}
