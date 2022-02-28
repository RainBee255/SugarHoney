using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    public class Prefabs
    {
        public static List<Type> generatePrefab<T>(string prefabName, params Type[] components)
        {
            var prefab = new List<Type>();
            for(int i = 0; i < components.Length; i++)
            {
                prefab.Add(components[i]);
            }
            Game1.prefabRegistry.Add(prefabName, prefab);
            return prefab;
        }

        public static void Initalize()
        {
            generatePrefab<Component>("p_Dummy", typeof(Component.Transform), typeof(Component.Sprite), typeof(Component.TestBehavior), typeof(Component.RenderSprite));
            generatePrefab<Component>("p_Player", typeof(Component.Transform), typeof(Component.Sprite), typeof(Component.TestControl), typeof(Component.RenderSprite));
        }
    }
}
