using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace CSGameProject.Code
{
    public class Player : Entity
    {
        public Input Input;
        private AnimationManager animationManager;
        private Dictionary<string, Animation> animations;
        private Vector2 position;
        private Texture2D texture;
        public static bool IsJumping = false;
        public static bool IsFalling = false;
        private float jumpHeight = 3.6f;
        private float jumpTime = 0.0f;
        private float gravity = 9.8f;
        private float speed = 3f;
        private Vector2 velocity;
        private bool flag = true;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                if (animationManager != null)
                    animationManager.Position = position;
            }
        }

        public Player(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            animationManager = new AnimationManager(this.animations.First().Value);
        }

        public Player(Texture2D texture)
        {
            this.texture = texture;
        }

        public override void Update(GameTime gameTime, List<Player> sprites, GraphicsDeviceManager graphics, Vector2 position)
        {
            Move(gameTime);

            Treasures.Update(Position, gameTime);

            var ghost = new Ghost();
            ghost.Update(gameTime, sprites, graphics, Position);

            SetAnimations();

            animationManager.Update(gameTime);

            Position += velocity;
            velocity = Vector2.Zero;
        }

        public void Move(GameTime gameTime)
        {
            Jump(gameTime);

            if (Keyboard.GetState().IsKeyDown(Input.Left) && position.X > 0 && flag)
            {
                velocity.X = -speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right) && position.X < 842 && flag)
            {
                velocity.X = speed;
            }
        }

        public void Jump(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up) && !IsJumping && !IsFalling)
            {
                IsJumping = true;
                jumpTime = 0.01f;
            }
            if (IsJumping && !IsFalling)
            {
                var speed = (jumpHeight * 2.0f * jumpTime) - (0.5f * gravity * jumpTime * jumpTime);
                if (speed <= 0)
                {
                    IsJumping = false;
                    jumpTime = 0.01f;
                    IsFalling = true;
                }
                else
                {
                    velocity.Y -= speed;
                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            if (IsFalling)
            {
                flag = false;
                var speed = (jumpHeight * 2.0f * jumpTime) - (0.5f * gravity * jumpTime * jumpTime);
                if (speed <= 0)
                {
                    IsFalling = false;
                    flag = true;
                    jumpTime = 0f;
                }
                foreach (var e in Map.LandBoundary.Keys)
                    if (Position.Y + Map.player.Height < e.Y && Map.LandBoundary[e] == false && 
                        Position.X >= e.X && Position.X <= e.X + e.Width)
                    {
                        IsFalling = false;
                        flag = true;
                        jumpTime = 0;
                        Map.LandBoundary[e] = true;
                    }
                else
                {
                    velocity.Y += speed;
                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            foreach (var e in Map.LandBoundary.Keys)
                if ((Position.X < e.X || Position.X > e.X + e.Width) && Position.Y <= e.Y 
                    && Map.LandBoundary[e] == true && IsJumping == false && IsFalling == false)
                {
                    IsFalling = true;
                    jumpTime = 0.01f;
                    Map.LandBoundary[e] = false;
                }
        }       

        public void SetAnimations()
        {
            if (velocity.X > 0)
                animationManager.Play(animations["WalkRight"]);
            else if (velocity.X < 0)
                animationManager.Play(animations["WalkLeft"]);
            else animationManager.Stop();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (animationManager != null)
                animationManager.Draw(spriteBatch);
        }
    }
}
