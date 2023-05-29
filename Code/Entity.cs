using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CSGameProject.Code
{
    public abstract class Entity
    {
        public abstract void Update(GameTime gameTime, List<Player> sprites, GraphicsDeviceManager graphics, Vector2 position);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }
}
