using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ConsoleGame
{

    public class Entity
    {
        // Component Registeries
        Queue<Component> starters = new Queue<Component>();
        List<Component> components = new List<Component>();
        List<KeyValuePair<Component,int>> disabledComponents = new List<KeyValuePair<Component,int>>();

        // Public Entity Fields
        public int Id;
        public string Name;
        public string Tag;
        private int ind = 0;

        // Entity Instantiation Functions
        public static Entity Instantiate()
        {
            int entityID = Game1.random.Next(99999999);
            Entity entityObj = new Entity();
            Game1.entityRegistry.Add(new KeyValuePair<int, Entity>(entityID, entityObj));
            entityObj.Id = entityID;
            entityObj.Name = entityID.ToString();
            Game1._activeScene.ind++;
            return entityObj;
        }

        public static Entity Instantiate(String prefabName)
        {
            Entity entityObj = Instantiate();
            List<Type> prefab;
            if (Game1.prefabRegistry.TryGetValue(prefabName, out prefab) == true)
            {
                for (int i = 0; i < prefab.Count; i++)
                {
                    Type T = prefab[i];
                    Component component = (Component)Activator.CreateInstance(T);
                    entityObj.AddComponent(component);
                    entityObj.Name = prefabName;
                }
            }

            Game1._activeScene.ind++;
            return entityObj;
        }

        // Component Registeration 
        public void AddComponent(Component component)
        {
            starters.Enqueue(component); 
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

        // Loop Logic
        public void Start(GameTime gameTime)
        {
            for (ind = 0; ind < starters.Count; ind++)
            {
                starters.TryDequeue(out Component _comp);
                _comp.Start(gameTime);
            }

        }
        public void UpdateStart(GameTime gameTime)
        {
            for (ind = 0; ind < components.Count; ind++)
            {
                var _comp = components[ind];
                _comp.UpdateStart(gameTime);
            }

        }
        public void Update(GameTime gameTime)
        {
            for(ind = 0; ind < components.Count; ind++)
            {
                var _comp = components[ind];
                _comp.Update(gameTime);
            }

        }
        public void UpdateEnd(GameTime gameTime)
        {
            for (ind = 0; ind < components.Count; ind++)
            {
                var _comp = components[ind];
                _comp.UpdateEnd(gameTime);
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
