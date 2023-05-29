using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CSGameProject.Code
{
    public class Vector2AndBool
    {
        public Vector2 Vector;
        public bool Flag;
    }

    public class Treasures
    {
        private static Texture2D textureCoin;
        private static Texture2D textureKey;
        private static Texture2D textureChestClose;
        private static Texture2D textureChestOpen;
        private static Texture2D textureDoorClose;
        private static Texture2D textureDoorOpen;
        private static Vector2 coinsOnEarthPos;
        private static Vector2 coinsOnLeftIslandPos;
        private static Vector2 coinsOnRightIslandPos;
        private static Vector2AndBool key;
        private static Vector2AndBool chestClose;
        private static Vector2AndBool chestOpen;
        private static Vector2AndBool doorOpen;
        private static Vector2AndBool doorClose;
        private static Dictionary<Vector2, bool> coinsOnEarth;
        private static Dictionary<Vector2, bool> coinsOnLeftIsland;
        private static Dictionary<Vector2, bool> coinsOnRightIsland;
        private static float time;
        private static float timeExit;
        public static bool IsExit;
        

        public static void Initialize(GraphicsDeviceManager graphics)
        {
            coinsOnEarthPos = new Vector2(300, graphics.PreferredBackBufferHeight - 80);
            coinsOnLeftIslandPos = new Vector2(90, graphics.PreferredBackBufferHeight - 235);
            coinsOnRightIslandPos = new Vector2(graphics.PreferredBackBufferWidth - 340, graphics.PreferredBackBufferHeight - 235);
            key = new Vector2AndBool() { Vector = new Vector2(graphics.PreferredBackBufferWidth - 100, graphics.PreferredBackBufferHeight - 90), Flag = true};
            chestClose = new Vector2AndBool() { Vector = new Vector2(10, graphics.PreferredBackBufferHeight - 240), Flag = true };
            chestOpen = new Vector2AndBool() { Vector = new Vector2(10, graphics.PreferredBackBufferHeight - 240) , Flag = true };
            doorOpen = new Vector2AndBool() { Vector = new Vector2(graphics.PreferredBackBufferWidth - 110, graphics.PreferredBackBufferHeight - 300), Flag = true };
            doorClose = new Vector2AndBool() { Vector = new Vector2(graphics.PreferredBackBufferWidth - 110, graphics.PreferredBackBufferHeight - 300), Flag = true };
            coinsOnEarth = new Dictionary<Vector2, bool>();
            coinsOnLeftIsland = new Dictionary<Vector2, bool>();
            coinsOnRightIsland = new Dictionary<Vector2, bool>();
            time = 0f;
            timeExit = 1000f;
            IsExit = false;
        }

        public static void Load(ContentManager content)
        {
            textureCoin = content.Load<Texture2D>("coin");
            textureKey = content.Load<Texture2D>("key");
            textureChestClose = content.Load<Texture2D>("chestClose");
            textureChestOpen = content.Load<Texture2D>("chestOpen");
            textureDoorClose = content.Load<Texture2D>("doorClose");
            textureDoorOpen = content.Load<Texture2D>("doorOpen");

            for (var i = 0; i < 5; i++)
                coinsOnEarth.Add(new Vector2(coinsOnEarthPos.X + textureCoin.Width / 2 * 3 * i, coinsOnEarthPos.Y), true);
            for (var i = 0; i < 6; i++)
                coinsOnLeftIsland.Add(new Vector2(coinsOnLeftIslandPos.X + textureCoin.Width / 2 * 3 * i, coinsOnLeftIslandPos.Y), true);
            for (var i = 0; i < 3; i++)
                coinsOnRightIsland.Add(new Vector2(coinsOnRightIslandPos.X + textureCoin.Width / 2 * 3 * i, coinsOnRightIslandPos.Y), true);
        }

        public static void Update(Vector2 playerPos, GameTime gameTime)
        {
            var centrePlayerPos = playerPos.X + Map.player.Width / 10;

            foreach (var key in coinsOnEarth.Keys)
                if (centrePlayerPos >= key.X && centrePlayerPos <= key.X + textureCoin.Width && 
                    playerPos.Y + Map.player.Height > key.Y && playerPos.Y < key.Y)
                    coinsOnEarth[key] = false;

            foreach (var key in coinsOnLeftIsland.Keys)
                if (centrePlayerPos >= key.X && centrePlayerPos <= key.X + textureCoin.Width && 
                    playerPos.Y + Map.player.Height > key.Y && playerPos.Y < key.Y)
                    coinsOnLeftIsland[key] = false;

            foreach (var key in coinsOnRightIsland.Keys)
                if (centrePlayerPos >= key.X && centrePlayerPos <= key.X + textureCoin.Width &&
                    playerPos.Y + Map.player.Height > key.Y && playerPos.Y < key.Y)
                    coinsOnRightIsland[key] = false;

            if (centrePlayerPos >= key.Vector.X && centrePlayerPos <= key.Vector.X + textureKey.Width &&
                playerPos.Y + Map.player.Height > key.Vector.Y && playerPos.Y < key.Vector.Y)
                key.Flag = false;

            if (centrePlayerPos >= chestClose.Vector.X && centrePlayerPos <= chestClose.Vector.X + textureChestClose.Width &&
                playerPos.Y + Map.player.Height > chestClose.Vector.Y && playerPos.Y < chestClose.Vector.Y)
                chestClose.Flag = false;
            else chestClose.Flag = true;

            if (!chestClose.Flag && !key.Flag)
                chestOpen.Flag = false;

            if (centrePlayerPos >= doorClose.Vector.X && centrePlayerPos <= doorClose.Vector.X + textureDoorClose.Width &&
                playerPos.Y + Map.player.Height + 5 > doorClose.Vector.Y && playerPos.Y - 50 < doorClose.Vector.Y)
                doorClose.Flag = false;
            else doorClose.Flag = true;

            if (!chestOpen.Flag && !doorClose.Flag)
                doorOpen.Flag = false;

            if (!doorOpen.Flag)
            {
                if (time <= timeExit)
                    time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                else time = 0f;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < coinsOnEarth.Count; i++)
            {
                var vector = new Vector2(coinsOnEarthPos.X + textureCoin.Width / 2 * 3 * i, coinsOnEarthPos.Y);
                if (coinsOnEarth[vector])
                    spriteBatch.Draw(textureCoin, vector, Color.White);
            }

            for (var i = 0; i < coinsOnLeftIsland.Count; i++)
            {
                var vector = new Vector2(coinsOnLeftIslandPos.X + textureCoin.Width / 2 * 3 * i, coinsOnLeftIslandPos.Y);
                if (coinsOnLeftIsland[vector])
                    spriteBatch.Draw(textureCoin, vector, Color.White);
            }

            for (var i = 0; i < coinsOnRightIsland.Count; i++)
            {
                var vector = new Vector2(coinsOnRightIslandPos.X + textureCoin.Width / 2 * 3 * i, coinsOnRightIslandPos.Y);
                if (coinsOnRightIsland[vector])
                    spriteBatch.Draw(textureCoin, vector, Color.White);
            }

            if (key.Flag)
                spriteBatch.Draw(textureKey, new Rectangle((int)key.Vector.X, (int)key.Vector.Y, 40, 40), Color.White);

            if (!chestOpen.Flag)
                spriteBatch.Draw(textureChestOpen, chestClose.Vector, Color.White);
            else
                spriteBatch.Draw(textureChestClose, chestClose.Vector, Color.White);

            if (!doorOpen.Flag)
            {
                spriteBatch.Draw(textureDoorOpen, doorOpen.Vector, Color.White);
                if (time > timeExit)
                {
                    IsExit = true;
                    time = 0f;
                }
            }
            else
                spriteBatch.Draw(textureDoorClose, doorClose.Vector, Color.White);
        }
    }
}
