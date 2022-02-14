using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame
{
    public class Prefabs
    {
        public List<Component> generatePrefab(string prefabName)
        {
            var prefab = new List<Component>();
            Game1.prefabRegistry.Add(prefabName, prefab);
            return prefab;
        }

        public List<Component> cloneEntity(Entity entity, String prefabName)
        {
            var prefab = new List<Component>();

            return prefab;
        }
    }
}
