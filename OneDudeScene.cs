using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConsoleGame
{
    public class OneDudeScene : Scene
    {
        private SpriteFont _font;
        private Texture2D _dudeTexture;
        public OneDudeScene(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = _game.Content.Load<SpriteFont>("font");
            _dudeTexture = _game.Content.Load<Texture2D>("sprTestPlayer");

            var e = StrawberryUtils.ECS.instantiateEntity();
            Component.Transform transform = new Component.Transform();
            Component.Sprite sprite = new Component.Sprite();
            Component.RenderSprite render = new Component.RenderSprite();
            Component.TestBehavior test = new Component.TestBehavior();

            transform.position = new Vector2(128, 128);
            sprite.spriteTexture = _dudeTexture;
            sprite.spriteColor = Color.White;

            e.AddComponent(transform);
            e.AddComponent(test);
            e.AddComponent(sprite);
            e.AddComponent(render);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_game.CurKeyboardState.IsKeyDown(Keys.Space) && _game.PrevKeyboardState.IsKeyUp(Keys.Space))
            {
                _game.changeScene(new TwoDudeScene(_game));
            }

            if (_game.CurKeyboardState.IsKeyDown(Keys.Tab) && _game.PrevKeyboardState.IsKeyUp(Keys.Tab))
            {
                var e = StrawberryUtils.ECS.instantiateEntity();
                Component.Transform transform = new Component.Transform();
                Component.Sprite sprite = new Component.Sprite();
                Component.RenderSprite render = new Component.RenderSprite();
                Component.TestBehavior test = new Component.TestBehavior();

                transform.position = new Vector2(128, 128);
                sprite.spriteTexture = _dudeTexture;
                sprite.spriteColor = Color.White;

                e.AddComponent(transform);
                e.AddComponent(test);
                e.AddComponent(sprite);
                e.AddComponent(render);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "One dude scene, press space to change scene, press tab to spawn new entites", new Vector2(10, 10), Color.White);

            int centerX = _game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            int centerY = _game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;

            //StrawberryUtils.Graphics.drawSprite(_dudeTexture, new Vector2(centerX, centerY), Color.White, spriteBatch);


        }

    }
}
