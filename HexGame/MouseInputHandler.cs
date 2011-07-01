using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HexGame
{
    public delegate void MouseLocation(IntVector2 mouseLocation);

    class MouseInputHandler : GameComponent
    {
        public MouseLocation LeftMouseClick;
        public MouseLocation RightMouseClick;

        MouseState _previousMouseState;

        public MouseInputHandler(Game game)
            : base(game)
        {
            game.Components.Add(this);

            _previousMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Released
                && _previousMouseState.LeftButton == ButtonState.Pressed)
                LeftMouseClick(new IntVector2(mouseState.X, mouseState.Y));
            if (mouseState.RightButton == ButtonState.Released
                && _previousMouseState.RightButton == ButtonState.Pressed)
                RightMouseClick(new IntVector2(mouseState.X, mouseState.Y));

            _previousMouseState = mouseState;

            base.Update(gameTime);
        }
    }
}
