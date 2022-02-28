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

            var e = StrawberryUtils.ECS.Instantiate("p_Dummy");
            e.DisableComponent(e.GetComponent<Component.TestBehavior>());
            e.GetComponent<Component.Transform>().position = new Vector2(128, 128);
            e.GetComponent<Component.Sprite>().spriteTexture = _dudeTexture;

            e = StrawberryUtils.ECS.Instantiate("p_Dummy");
            e.DisableComponent(e.GetComponent<Component.TestBehavior>());
            e.GetComponent<Component.Transform>().position = new Vector2(128, 128 + 64);
            e.GetComponent<Component.Sprite>().spriteTexture = _dudeTexture;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_game.CurKeyboardState.IsKeyDown(Keys.Space) && _game.PrevKeyboardState.IsKeyUp(Keys.Space))
            {
                _game.ChangeScene(new OneDudeScene(_game));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "Two dude scene", new Vector2(10, 10), Color.White);



        }
    }
}
