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
        private bool isJumping = false;
        private bool isFalling = false;
        private float jumpHeight = 3.4f;
        private float jumpTime = 0.0f;
        private float gravity = 9.8f;
        private float speed = 3f;
        private Vector2 velocity;

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

        public override void Update(GameTime gameTime, List<Player> sprites)
        {
            Jump(gameTime);
            
            Move(gameTime);

            SetAnimations();

            animationManager.Update(gameTime);

            Position += velocity;
            velocity = Vector2.Zero;
        }

        public void Move(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left) && position.X > 0)
                velocity.X = -speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right) && position.X < 842)
                velocity.X = speed;
        }

        public void Jump(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up) && !isJumping && !isFalling)
            {
                isJumping = true;
                jumpTime = 0.01f;
            }
            if (isJumping && !isFalling)
            {
                var speed = (jumpHeight * 2.0f * jumpTime) - (0.5f * gravity * jumpTime * jumpTime);
                if (speed <= 0)
                {
                    isJumping = false;
                    jumpTime = 0.01f;
                    isFalling = true;
                }
                else
                {
                    velocity.Y -= speed;
                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            if (isFalling)
            {
                var speed = (jumpHeight * 2.0f * jumpTime) - (0.5f * gravity * jumpTime * jumpTime);
                if (speed <= 0 || Position.Y == (Map.LandBoundary[0].Y + Map.LandBoundary[0].Height / 2))
                {
                    isFalling = false;
                    jumpTime = 0.01f;
                }
                else
                {
                    velocity.Y += speed;
                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (animationManager != null)
                animationManager.Draw(spriteBatch);
        }
    }
}
