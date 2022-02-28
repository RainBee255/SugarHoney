using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConsoleGame
{

    public class Entity
    {
        List<Component> components = new List<Component>();
        List<KeyValuePair<Component,int>> disabledComponents = new List<KeyValuePair<Component,int>>();
        public int ID;
        public string NAME;
        private int ind = 0;


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
        public void RemoveComponent(Component component)
        {
            components.Remove(component);
            ind++;
        }

        public void DisableComponent(Component component)
        {
            var index = components.IndexOf(component);
            components.Remove(component);
            disabledComponents.Add(new KeyValuePair<Component,int>(component,index));
            ind++;
        }

        public Component EnableComponent<T>() where T : Component
        {
            Component _comp = null;
            int _ind = 0;
            KeyValuePair<Component, int> _pair = new KeyValuePair<Component, int>();

            if(GetComponent<T>() == null)
            { 
                foreach (KeyValuePair<Component,int> pair in disabledComponents)
                {
                    if(pair.Key.GetType().Equals(typeof(T)))
                    {
                        _comp = pair.Key;
                        _ind = pair.Value;
                        _pair = pair;
                        break;
                    }
                }

                if(_comp != null)
                {
                    disabledComponents.Remove(_pair);
                    ind--;
                    components.Insert(_ind, _comp);
                    return _comp;
                }
            }
            return null;
        }

        // Execute Components
        public void Update(GameTime gameTime)
        {
            for(ind = 0; ind < components.Count; ind++)
            {
                var _comp = components[ind];
                _comp.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (ind = 0; ind < components.Count; ind++)
            {
                var _comp = components[ind];
                _comp.Draw(spriteBatch);
            }
        }

    }
}
