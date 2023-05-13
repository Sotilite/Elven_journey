using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSGameProject.Code
{
    public abstract class GamePosition
    {
        public static Texture2D backGround { get; set; }
        public static ContentManager content;

        public GamePosition(Game game)
        {
            content = new ContentManager(game.Services) { RootDirectory = "Content" };
        }

        public abstract void Initialize(Game game, GraphicsDeviceManager graphics);

        public abstract void LoadContent(GraphicsDeviceManager graphics); 

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics);
    }
}
