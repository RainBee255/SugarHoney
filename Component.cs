using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace ConsoleGame
{
    public class Component 
    {
        public Entity entity;

        // Component Methods
        public virtual void Start(GameTime gameTime) { }
        public virtual void UpdateStart(GameTime gameTime) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void UpdateEnd(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public class Transform : Component
        {
            /// <summary>
            /// Contains 2D coordinates + Scaling + Rotation for sprites and collisions.
            /// </summary>
            public Vector2 position = Vector2.Zero;
            public Vector2 scale = Vector2.One;
            public float depth = 0;
            public float rotation = 0;
        }

        public class Sprite : Component
        {
            /// <summary>
            /// Contains a basic texture2D sprite.
            /// </summary>
            public Texture2D spriteTexture;
            public Color spriteColor = Color.White;
        }

        public class TestBehavior : Component
        {
            /// <summary>
            /// Simple demo test, moves the object around within a 50 unit radius, and lerps it into the new picked location within the radius.
            /// </summary>
            private int tick = 110;
            private Vector2 nPosition = Vector2.Zero;
            public override void Start(GameTime gameTime)
            {
                base.Start(gameTime);
                Debug.WriteLine(entity.Id);
            }
            public override void Update(GameTime gameTime)
            {
                Transform T = entity.GetComponent<Transform>();
                if(tick == 110)
                {
                    nPosition = T.position;
                }
                if (tick <= 0)
                {

                    tick = Game1.random.Next(100);
                    nPosition = T.position + new Vector2(Game1.random.Next(-50, 50), Game1.random.Next(-50, 50));

                }
                tick--;
                var f = 0.5f;
                T.position = StrawberryUtils.Math.Lerp(T.position, nPosition, f);
            }
        }
        public class RenderSprite : Component
        {
            /// <summary>
            /// Renders a basic 2D sprite to the screen using transform coordinates.
            /// </summary>
            /// <param name="gameTime"></param>
            public override void Draw(SpriteBatch spriteBatch)
            {
                
                Sprite S = entity.GetComponent<Sprite>();
                Transform T = entity.GetComponent<Transform>();
                
                StrawberryUtils.Graphics.DrawSprite(S.spriteTexture, new Vector2(MathF.Round(T.position.X),MathF.Round(T.position.Y)), S.spriteColor, spriteBatch);


                
            }

        }

        public class TestControl : Component
        {
            private KeyboardState curKeyboard;
            private MouseState curMouse;
            public override void Start(GameTime gameTime)
            {
                var S = entity.GetComponent<Sprite>();
                S.spriteColor = Color.Yellow;
                Debug.WriteLine(entity.Id);
                base.Update(gameTime);
            }
            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                var prevKeyboard = curKeyboard;
                curKeyboard = Keyboard.GetState();
                var mPos = curMouse.Position;
                Transform T = entity.GetComponent<Transform>();
                RenderSprite S = entity.GetComponent<RenderSprite>();

                if (curKeyboard.IsKeyDown(Keys.A))
                {
                    T.position.X--;
                }
                if(curKeyboard.IsKeyDown(Keys.D))
                {
                    T.position.X++;
                }
                if(curKeyboard.IsKeyDown(Keys.W))
                {
                    T.position.Y--;
                }
                if(curKeyboard.IsKeyDown(Keys.S))
                {
                    T.position.Y++;
                }


                if (curKeyboard.IsKeyDown(Keys.Q) && prevKeyboard.IsKeyUp(Keys.Q))
                {
                    entity.DisableComponent(S);

                }

                if (curKeyboard.IsKeyDown(Keys.E) && prevKeyboard.IsKeyUp(Keys.E))
                {
                    entity.EnableComponent<RenderSprite>();


                }

                for (int i = 0; i < entity.entityRegistry.Count; i++)
                {
                    if(entity.entityRegistry[i].Value.Tag != "Player")
                    { 
                        var P = entity.entityRegistry[i].Value.GetComponent<Transform>().position;
                        if (Vector2.Distance(T.position, P) < 30)
                        {
                            //Debug.WriteLine("Oh shit I'm nearby! " + Vector2.Distance(P,T.position));
                            var ES = entity.entityRegistry[i].Value.GetComponent<Sprite>();
                            ES.spriteColor = Color.DarkGoldenrod;
                        }
                    }
                }
            }
            
        }
       
    }
}

