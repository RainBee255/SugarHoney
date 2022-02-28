using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ConsoleGame
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        static public SpriteBatch _spriteBatch;
        static public Texture2D playerTexture;
        static public Random random;
        static public List<KeyValuePair<int,Entity>> entityRegistry;
        static public Dictionary<String,List<Type>>prefabRegistry;

        public static Scene _activeScene;
        public static Scene _nextScene;
        public static Game _game;

        public KeyboardState PrevKeyboardState { get; private set; }
        public KeyboardState CurKeyboardState { get; private set; }





        public void ChangeScene(Scene next)
        {
            if(_activeScene != next)
            {
                _nextScene = next;
            }
        }

        private void TransitionScene()
        {
            if(_activeScene != null)
            {
                _activeScene.UnloadContent();
            }

            entityRegistry.Clear();
            GC.Collect();

            _activeScene = _nextScene;

            _nextScene = null;

            if(_activeScene != null)
            {
                _activeScene.Initialize();
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            random = new Random();
            entityRegistry = new List<KeyValuePair<int, Entity>>();
            prefabRegistry = new Dictionary<String, List<Type>>();
            _game = this;

            ChangeScene(new OneDudeScene(this));
            Prefabs.Initalize();    
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("sprTestPlayer");
            Content.Load<SpriteFont>("font");
            
        }


        // Run all in-game entities
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            PrevKeyboardState = CurKeyboardState;
            CurKeyboardState = Keyboard.GetState();

            if(_nextScene != null)
            {
                TransitionScene();
            }

            if(_activeScene != null)
            {
                _activeScene.Update(gameTime);
                _activeScene.Start(gameTime);
            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            if (_activeScene != null)
            {
                _activeScene.BeforeDraw(_spriteBatch, Color.Black);
                _activeScene.Draw(_spriteBatch);
                _activeScene.AfterDraw(_spriteBatch);
            }

            base.Draw(gameTime);
        }

    }
}
