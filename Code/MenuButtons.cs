using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CSGameProject.Code
{ 
    class ButtonSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    class MenuButtons : DrawableGameComponent
    {
        private enum State
        {
            NormalPlay = 0,
            PressedPlay = 1,
            NormalContinue = 0,
            PressedContinue = 1,
            NormalQuit = 0,
            PressedQuit = 1,
        }

        private static System.Numerics.Vector2 playPos;
        private static System.Numerics.Vector2 continuePos;
        private static System.Numerics.Vector2 quitPos;
        private static Texture2D[] playButton;
        private static Texture2D[] continueButton;
        private static Texture2D[] quitButton;
        private static ContentManager content;
        private static State statePlay;
        private static State stateContinue;
        private static State stateQuit;
        private static ButtonSize buttonSize;

        public MenuButtons(Game game) : base(game)
        {
            content = new ContentManager(game.Services) { RootDirectory = "Content" };
            playButton = new Texture2D[2];
            continueButton = new Texture2D[2]; 
            quitButton = new Texture2D[2]; 
            statePlay = State.NormalPlay;
            stateContinue = State.NormalContinue;
            stateQuit = State.NormalQuit;
            buttonSize = new ButtonSize { Width = 150, Height = 75 };
        }

        public static Action Action { get; set; }

        public static void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            playPos = new System.Numerics.Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2 - 100);
            continuePos = new System.Numerics.Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2);
            quitPos = new System.Numerics.Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2 + 100);
            var button = new MenuButtons(game);
            Action += () =>
            {
                var newButton = new MenuButtons(game);
                game.Components.Add(newButton);
            };
            game.Components.Add(button);
        }

        public static void Update()
        {
            var mouseState = Mouse.GetState();

            if (IsBoundButtonPlay(mouseState))
                statePlay = State.PressedPlay;
            else
                statePlay = State.NormalPlay;

            if (IsBoundButtonContinue(mouseState))
                stateContinue = State.PressedContinue;
            else
                stateContinue = State.NormalContinue;

            if (IsBoundButtonQuit(mouseState))
                stateQuit = State.PressedContinue;
            else
                stateQuit = State.NormalQuit;
        }

        public static bool IsBoundButtonPlay(MouseState mouseState)
        {
            if (mouseState.X >= playPos.X - buttonSize.Width / 2 && mouseState.X <= playPos.X + buttonSize.Width / 2
               && mouseState.Y >= playPos.Y - buttonSize.Height / 2 && mouseState.Y <= playPos.Y + buttonSize.Height / 2)
                return true;
            return false;
        }

        public static bool IsBoundButtonContinue(MouseState mouseState)
        {
            if (mouseState.X >= continuePos.X - buttonSize.Width / 2 && mouseState.X <= continuePos.X + buttonSize.Width / 2
               && mouseState.Y >= continuePos.Y - buttonSize.Height / 2 && mouseState.Y <= continuePos.Y + buttonSize.Height / 2)
                return true;
            return false;
        }

        public static bool IsBoundButtonQuit(MouseState mouseState)
        {
            if (mouseState.X >= quitPos.X - buttonSize.Width / 2 && mouseState.X <= quitPos.X + buttonSize.Width / 2
               && mouseState.Y >= quitPos.Y - buttonSize.Height / 2 && mouseState.Y <= quitPos.Y + buttonSize.Height / 2)
                return true;
            return false;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(playButton[(int)statePlay], new Rectangle((int)playPos.X, (int)playPos.Y, 150, 75), null,
                Color.White, 0f, new System.Numerics.Vector2(playButton[(int)statePlay].Width / 2, playButton[(int)statePlay].Height / 2),
                SpriteEffects.None, 0f);
            spriteBatch.Draw(continueButton[(int)stateContinue], new Rectangle((int)continuePos.X, (int)continuePos.Y, 150, 75), null,
                Color.White, 0f, new System.Numerics.Vector2(continueButton[(int)stateContinue].Width / 2, continueButton[(int)stateContinue].Height / 2),
                SpriteEffects.None, 0f);
            spriteBatch.Draw(quitButton[(int)stateQuit], new Rectangle((int)quitPos.X, (int)quitPos.Y, 150, 75), null,
                Color.White, 0f, new System.Numerics.Vector2(quitButton[(int)stateQuit].Width / 2, quitButton[(int)stateQuit].Height / 2),
                SpriteEffects.None, 0f);
        }

        public static void LoadContent()
        {
            playButton[0] = content.Load<Texture2D>("play1");
            playButton[1] = content.Load<Texture2D>("play2");
            continueButton[0] = content.Load<Texture2D>("continue1");
            continueButton[1] = content.Load<Texture2D>("continue2");
            quitButton[0] = content.Load<Texture2D>("quit1");
            quitButton[1] = content.Load<Texture2D>("quit2");
        }
    }
}
