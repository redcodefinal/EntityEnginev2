using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Data
{
    public struct Assets
    {
        public static Texture2D Pixel;

        public static void LoadConent(Game game)
        {
            Pixel = game.Content.Load<Texture2D>(@"EntityEngine/pixel");
        }
    }
}