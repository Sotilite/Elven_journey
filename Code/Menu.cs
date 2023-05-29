using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CSGameProject.Code
{
    public class Menu : GameState
    {
        public static Texture2D background { get; set; }

        public Menu(Game game) : base(game)
        {
            content = new ContentManager(game.Services) { RootDirectory = "Content" };
        }


        public override void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            var menu = new Menu(game);
            MenuButtons.Initialize(game, graphics);
        }

        public override void Load(GraphicsDeviceManager graphics)
        {
            background = content.Load<Texture2D>("backgroundMenu");
            MenuButtons.LoadContent();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            MenuButtons.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 650), Color.White);
            MenuButtons.Draw(spriteBatch, graphics);
        }
    }
}
