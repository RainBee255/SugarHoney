using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame
{
    public static class Globals
    {

        public static Dictionary<uint, string> tagRegistry = new Dictionary<uint, string>();
        public static uint tagCount = 0;

        public static uint TagAssign(string tagName)
        {
            tagRegistry.Add(tagCount, tagName);
            tagCount++;
            return tagCount;
        }

        static public uint tag_Player = TagAssign("Player");
        static public uint tag_Solid = TagAssign("Solid");
        static public uint tag_Test = TagAssign("Test");

    }
}
