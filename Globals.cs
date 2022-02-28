using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame
{
    public class Globals
    {
        public static Dictionary<uint, List<uint>>tagRegistry;
        public static Dictionary<string, uint>tagNames;
        public static uint tagCount = 0;
        

        public uint TagAssign(string tagName)
        {
            tagNames[tagName] = tagCount;
            tagRegistry.Add(tagCount, new List<uint>());
            tagCount++;
            return tagCount;
        }


        public void GlobalInitalization()
        {
            tagNames = new Dictionary<string, uint>();
            tagRegistry = new Dictionary<uint, List<uint>>();

            TagAssign("Player");
            TagAssign("Test");


        }
    }


}
