using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Framework;
using ConsoleGame;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Design;

namespace ConsoleGame
{

    public class Entity
    {
        List<Component> components = new List<Component>();
        public int ID;
        public string NAME;



        public void AddComponent(Component component)
        {
            components.Add(component);
            component.entity = this;
        }
        public T GetComponent<T>() where T : Component
        {
            foreach(Component component in components)
            {
                if(component.GetType().Equals(typeof(T)))
                {
                    return (T)component;
                }
            }
            return null;
        }

        // Execute Components
        public void Update(GameTime gameTime)
        {
            
            foreach(Component component in components)
            {
                component.Update(gameTime);   
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

    }
}
