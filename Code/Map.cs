using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CSGameProject.Code
{
    class Map : GamePosition
    {
        private static Texture2D background { get; set; }
        public static Texture2D earth;
        public static Texture2D LeftIsland;
        public static Texture2D player;
        public static List<Rectangle> LandBoundary;
        private static Vector2 earthPos;
        public static Vector2 IslandPos;
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
            LandBoundary = new List<Rectangle>();
        }

        public override void LoadContent(GraphicsDeviceManager graphics)
        {
            background = content.Load<Texture2D>("backgroundMap");
            earth = content.Load<Texture2D>("earth");
            LeftIsland = content.Load<Texture2D>("earth");
            player = content.Load<Texture2D>("goRight");

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
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites)
                sprite.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 900, 500), Color.White);

            for (var i = 0; i < graphics.PreferredBackBufferWidth / earth.Width + 1; i++)
            {
                spriteBatch.Draw(earth, new Vector2(earthPos.X + earth.Width * i,
                    earthPos.Y - earth.Height), Color.White);
            }

            for (var i = 0; i < 4; i++)
                spriteBatch.Draw(earth, new Vector2(IslandPos.X + LeftIsland.Width * i, IslandPos.Y - 175), Color.White);
            for (var i = 0; i < 5; i++)
                spriteBatch.Draw(earth, new Vector2(IslandPos.X + 350 + LeftIsland.Width * i, IslandPos.Y - 225), Color.White);
            for (var i = 0; i < graphics.PreferredBackBufferWidth / earth.Width + 1; i++)
                spriteBatch.Draw(earth, new Vector2(IslandPos.X + graphics.PreferredBackBufferWidth / 2 + LeftIsland.Width * i, IslandPos.Y - 350), Color.White);
            for (var i = 0; i < 4; i++)
                spriteBatch.Draw(earth, new Vector2(IslandPos.X + 730 + LeftIsland.Width * i, IslandPos.Y - 200), Color.White);

            LandBoundary.Add(new Rectangle((int)earthPos.X * 3, (int)earthPos.Y - 175, earth.Width / 2 * 5, earth.Height / 2));

            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
