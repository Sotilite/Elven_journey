using CSGameProject.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSGameProject
{
    enum State
    {
        Menu,
        Game,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        State State = State.Menu;
        Map map;
        Menu menu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            menu = new Menu(this);
            map = new Map(this);
            menu.Initialize(this, _graphics);
            map.Initialize(this, _graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(_graphics);
            map.LoadContent(_graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            switch (State)
            {
                case State.Menu:
                    menu.Update(gameTime);
                    if (mouse.LeftButton == ButtonState.Pressed && MenuButtons.IsBoundButtonPlay(mouse)) State = State.Game;
                    break;
                case State.Game:
                    map.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) State = State.Menu;
                    break;
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || (mouse.LeftButton == ButtonState.Pressed && MenuButtons.IsBoundButtonQuit(mouse)))
            {
                Exit();
                State = State.Menu;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (State)
            {
                case State.Menu:
                    menu.Draw(_spriteBatch, _graphics);
                    break;
                case State.Game:
                    map.Draw(_spriteBatch, _graphics);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}