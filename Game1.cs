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
        public delegate void drawSpriteDelegate(Texture2D sprite, Vector2 position, Color color);
        public Entity testEnity;
        public Entity testEntity2;
        static public Random random;
        static List<Entity> entityRegistry;

        public Entity instantiateEntity(Vector2 position)
        {
            int entityID = random.Next(500000);
            Entity entityObj = new Entity();
            entityRegistry.Add(entityObj);
            //Debug.WriteLine(entityObj);
            return entityObj;
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
            entityRegistry = new List<Entity>();

            // Entity Creation
            for(int i = 0; i < 10; i++)
            {
                var entity = instantiateEntity(new Vector2(255,255));

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
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("sprTestPlayer");
            
        }


        // Run all in-game entities
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(Entity entity in entityRegistry)
                {
                    entity.Update(gameTime);
                }
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Run each entity's draw code.
            foreach (Entity entity in entityRegistry)
            {
                entity.Draw(gameTime);
            }

            base.Draw(gameTime);
            
        }

    }
}
