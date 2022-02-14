﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConsoleGame
{
    public class Scene
    {
        protected Game1 _game;

        protected ContentManager _content;


        public Scene(Game1 game)
        {
            if(game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game cannot be null!");
            }

            _game = game;
        }

        public virtual void Initialize()
        {
            _content = new ContentManager(_game.Services);
            _content.RootDirectory = _game.Content.RootDirectory;
            LoadContent();
        }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() 
        {
            _content.Unload();
            _content = null;
        }

        public virtual void Update(GameTime gameTime)
        {

            
            foreach (KeyValuePair<int,Entity> entity in Game1.entityRegistry)
            {
                entity.Value.Update(gameTime);
            }
            
        }


        public virtual void BeforeDraw(SpriteBatch spriteBatch, Color clearColor)
        {
            _game.GraphicsDevice.Clear(clearColor);

            spriteBatch.Begin();
        }

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
           
            foreach (KeyValuePair<int, Entity> entity in Game1.entityRegistry)
            {
                entity.Value.Draw(spriteBatch);
            }
           
        }

        public virtual void AfterDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.End();
        }

    }
}
