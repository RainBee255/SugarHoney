using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConsoleGame
{
    public class Component 
    {
        public Entity entity;
        
        public virtual void Update(GameTime gameTime)
        { }
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
                
                StrawberryUtils.Graphics.drawSprite(S.spriteTexture, T.position, S.spriteColor, spriteBatch);


                
            }

        }

        public class TestControl : Component
        {
            private KeyboardState curKeyboard;
            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                var prevKeyboard = curKeyboard;
                curKeyboard = Keyboard.GetState();
                Transform T = entity.GetComponent<Transform>();

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

            }
            
        }
       
    }
}

