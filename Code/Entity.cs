using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CSGameProject.Code
{
    public abstract class Entity
    {
        public abstract void Update(GameTime gameTime, List<Player> sprites);

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
