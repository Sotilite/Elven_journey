using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CSGameProject.Code
{
    class Map : GameState
    {
        public static Texture2D earth;
        public static Texture2D LeftIsland;
        public static Texture2D LeftEarthBigTile;
        public static Texture2D RightEarthBigTile;
        public static Texture2D player;
        public static Vector2 IslandPos;
        public static Dictionary<Rectangle, bool> LandBoundary;
        public static Vector2 earthPos;
        private static Texture2D background { get; set; }
        private static Texture2D textureScore;
        private static Vector2 scorePos;
        private static List<Player> sprites;

        public Map(Game game) : base(game)
        {
            content = new ContentManager(game.Services) { RootDirectory = "Content" };
        }

        public override void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            var map = new Map(game);
            earthPos = new Vector2(0, graphics.PreferredBackBufferHeight);
            IslandPos = new Vector2(0, graphics.PreferredBackBufferHeight);
            LandBoundary = new Dictionary<Rectangle, bool>();
            scorePos = new Vector2(5, 5);
            Treasures.Initialize(graphics);
            Ghost.Initialize(graphics);
        }

        public override void Load(GraphicsDeviceManager graphics)
        {
            background = content.Load<Texture2D>("backgroundMap");
            earth = content.Load<Texture2D>("earth");
            LeftIsland = content.Load<Texture2D>("earth");
            player = content.Load<Texture2D>("goRight");
            LeftEarthBigTile = content.Load<Texture2D>("leftEarthBigTile");
            RightEarthBigTile = content.Load<Texture2D>("rightEarthBigTile");
            textureScore = content.Load<Texture2D>("scoreBlack");
            Treasures.Load(content);
            Ghost.Load(content);

            var animations = new Dictionary<string, Animation>()
            {
                { "WalkRight", new Animation(content.Load<Texture2D>("goRight"), 5) },
                { "WalkLeft", new Animation(content.Load<Texture2D>("goLeft"), 5) }
            };
            sprites = new List<Player>()
            {
                new Player(animations)
                {
                    Position = new Vector2(10, graphics.PreferredBackBufferHeight - earth.Height - player.Height),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Right = Keys.Right,
                        Left = Keys.Left,
                    }
                }
            };

            var key1 = new Rectangle((int)IslandPos.X - player.Width / 30 * 2, (int)IslandPos.Y - 200, LeftEarthBigTile.Width, LeftEarthBigTile.Height);
            LandBoundary.Add(key1, false);
            var key2 = new Rectangle(graphics.PreferredBackBufferWidth - RightEarthBigTile.Width - player.Width / 25 * 3, (int)IslandPos.Y - 200,
                RightEarthBigTile.Width, RightEarthBigTile.Height);
            LandBoundary.Add(key2, false);
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (var sprite in sprites)
                sprite.Update(gameTime, sprites, graphics, sprite.Position);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 900, 500), Color.White);

            for (var i = 0; i < graphics.PreferredBackBufferWidth / earth.Width + 1; i++)
            {
                spriteBatch.Draw(earth, new Vector2(earthPos.X + earth.Width * i,
                    earthPos.Y - earth.Height), Color.White);
            }

            //spriteBatch.Draw(textureScore, scorePos, Color.White);

            Treasures.Draw(spriteBatch);

            var ghost = new Ghost();
            ghost.Draw(spriteBatch, gameTime);

            spriteBatch.Draw(LeftEarthBigTile, new Vector2(IslandPos.X, IslandPos.Y - 200), Color.White);
            spriteBatch.Draw(RightEarthBigTile, new Vector2(graphics.PreferredBackBufferWidth - RightEarthBigTile.Width, IslandPos.Y - 200), Color.White);

            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch, gameTime);
        }
    }
}
