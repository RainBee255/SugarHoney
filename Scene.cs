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
        public int ind = 0;

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

        public virtual void Start(GameTime gameTime)
        {
            for(ind = Game1.entityRegistry.Count-1; ind > -1; ind--)
            {
                var _entity = Game1.entityRegistry[ind];
                _entity.Value.Start(gameTime);
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            for (ind = Game1.entityRegistry.Count-1; ind > -1; ind--)
            {
                var _entity = Game1.entityRegistry[ind];
                _entity.Value.UpdateStart(gameTime);
                _entity.Value.Update(gameTime);
                _entity.Value.UpdateEnd(gameTime);
            }

        }


        public virtual void BeforeDraw(SpriteBatch spriteBatch, Color clearColor)
        {
            _game.GraphicsDevice.Clear(clearColor);

            spriteBatch.Begin();
        }

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            for (ind = Game1.entityRegistry.Count-1; ind > -1; ind--)
            {
                var _entity = Game1.entityRegistry[ind];
                _entity.Value.Draw(spriteBatch);
            }
        }

        public virtual void AfterDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.End();
        }

    }
}
