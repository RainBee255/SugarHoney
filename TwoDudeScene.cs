using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConsoleGame
{
    class TwoDudeScene : Scene
    {
        private SpriteFont _font;
        private Texture2D _dudeTexture;
        public TwoDudeScene(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = _game.Content.Load<SpriteFont>("font");
            _dudeTexture = _game.Content.Load<Texture2D>("sprTestPlayer");

            var e = StrawberryUtils.ECS.instantiateEntity();
            Component.Transform transform = new Component.Transform();
            Component.Sprite sprite = new Component.Sprite();
            Component.RenderSprite render = new Component.RenderSprite();

            transform.position = new Vector2(128, 128);
            sprite.spriteTexture = _dudeTexture;
            sprite.spriteColor = Color.White;

            e.AddComponent(transform);
            e.AddComponent(sprite);
            e.AddComponent(render);

            e = StrawberryUtils.ECS.instantiateEntity();
            transform = new Component.Transform();
            sprite = new Component.Sprite();
            render = new Component.RenderSprite();

            transform.position = new Vector2(128, 128+64);
            sprite.spriteTexture = _dudeTexture;
            sprite.spriteColor = Color.White;

            e.AddComponent(transform);
            e.AddComponent(sprite);
            e.AddComponent(render);


        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_game.CurKeyboardState.IsKeyDown(Keys.Space) && _game.PrevKeyboardState.IsKeyUp(Keys.Space))
            {
                _game.changeScene(new OneDudeScene(_game));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "Two dude scene", new Vector2(10, 10), Color.White);



        }
    }
}
