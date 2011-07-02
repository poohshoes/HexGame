using Microsoft.Xna.Framework.Graphics;

namespace HexGame
{
    class Image
    {
        public Texture2D Texture { get; private set; }
        public IntVector2 DrawOffset { get; private set; }


        public Image(Texture2D texture)
        {
            Texture = texture;
            DrawOffset = new IntVector2(0, 0);
        }

        public Image (Texture2D texture, IntVector2 drawOffset)
        {
            Texture = texture;
            DrawOffset = drawOffset;
        }
    }
}
