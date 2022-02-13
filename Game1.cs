using ConsoleGame;
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
        static public List<Entity> entityRegistry;

        private Scene _activeScene;
        private Scene _nextScene;

        public KeyboardState PrevKeyboardState { get; private set; }
        public KeyboardState CurKeyboardState { get; private set; }





        public void changeScene(Scene next)
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

            //StrawberryUtils.ECS.FlushEntities(entityRegistry);
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
            changeScene(new OneDudeScene(this));
            random = new Random();
            entityRegistry = new List<Entity>();
            /*
            // Entity Creation
            for(int i = 0; i < 10; i++)
            {
                var entity = instantiateEntity();

                Component.Transform transform = new Component.Transform();
                transform.position = new Vector2(random.Next(255), random.Next(255));
                Component.Sprite sprite = new Component.Sprite();
                sprite.spriteTexture = playerTexture;
                Component.RenderSprite renderSprite = new Component.RenderSprite();
                Component.TestBehavior testBehavior = new Component.TestBehavior();

                entity.AddComponent(transform);
                entity.AddComponent(sprite);
                entity.AddComponent(testBehavior);
                entity.AddComponent(renderSprite);

               
            }
            */
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
            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            if (_activeScene != null)
            {
                _activeScene.BeforeDraw(_spriteBatch, Color.Black);
                _activeScene.Draw(_spriteBatch);
                _activeScene.AfterDraw(_spriteBatch);
            }
            // Run each entity's draw code.


            base.Draw(gameTime);
            
        }

    }
}
