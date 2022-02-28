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

            var e = Entity.Instantiate("p_Player");
            e.GetComponent<Component.Transform>().position = new Vector2(128, 128);
            e.GetComponent<Component.Sprite>().spriteTexture = _dudeTexture;
            e.AssignToTag("Player");

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_game.CurKeyboardState.IsKeyDown(Keys.Space) && _game.PrevKeyboardState.IsKeyUp(Keys.Space))
            {
                _game.ChangeScene(new TwoDudeScene(_game));
            }

            if (_game.CurKeyboardState.IsKeyDown(Keys.Tab) && _game.PrevKeyboardState.IsKeyUp(Keys.Tab))
            {
                for(int i = 0; i < 10; i++)
                {
                var e = Entity.Instantiate("p_Dummy");

                e.GetComponent<Component.Transform>().position = new Vector2(128, 128);
                e.GetComponent<Component.Sprite>().spriteTexture = _dudeTexture;
                e.AssignToTag("Test");
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "One dude scene, press space to change scene, press tab to spawn new entites", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(_font, Game1.entityRegistry.Count + " entites in the scene.", new Vector2(10, 32), Color.White);
            
            int centerX = _game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            int centerY = _game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;

            //StrawberryUtils.Graphics.drawSprite(_dudeTexture, new Vector2(centerX, centerY), Color.White, spriteBatch);


        }

    }
}
