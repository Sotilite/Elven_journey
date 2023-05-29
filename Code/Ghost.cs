using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CSGameProject.Code
{
    public class Ghost : Entity
    {
        private static Texture2D centre;
        private static Texture2D left;
        private static Texture2D right;
        private static List<Texture2D> texture2Ds;
        private static Rectangle position;
        private static float frameTime;
        private static float time;
        private static int frameIndex;
        private static bool flag;
        private static bool isFalling;

        public static void Initialize(GraphicsDeviceManager graphics)
        {
            position = new Rectangle(graphics.PreferredBackBufferWidth - 200, graphics.PreferredBackBufferHeight - 252, 45, 45);
            texture2Ds = new List<Texture2D>();
            frameTime = 2f;
            time = 0f;
            frameIndex = 0;
            flag = true;
        }

        public static void Load(ContentManager content)
        {
            centre = content.Load<Texture2D>("centreGhost");
            left = content.Load<Texture2D>("leftGhost");
            right = content.Load<Texture2D>("rightGhost");
            texture2Ds.Add(centre);
            texture2Ds.Add(left);
            texture2Ds.Add(centre);
            texture2Ds.Add(right);
        }

        public override void Update(GameTime gameTime, List<Player> sprites, GraphicsDeviceManager graphics, Vector2 playerPos)
        {
            isFalling = Player.IsFalling;

            if (playerPos.X + Map.player.Width / 10 + 5 >= position.X && playerPos.X <= position.X + centre.Width &&
                playerPos.Y + Map.player.Height >= position.Y && isFalling)
                flag = false;
            if (flag)
            {
                if (time <= frameTime)
                {
                    time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (time > frameTime)
                {
                    frameIndex++;
                    time = 0f;

                    if (frameIndex >= texture2Ds.Count)
                    {
                        frameIndex = 0;
                    }
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (flag)
            {
                if (frameIndex == 1)
                    spriteBatch.Draw(texture2Ds[frameIndex], new Rectangle(position.X + 2, position.Y, 43, 45), Color.White);
                else spriteBatch.Draw(texture2Ds[frameIndex], position, Color.White);
            }
        }
    }
}
