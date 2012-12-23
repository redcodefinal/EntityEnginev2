using System;
using Microsoft.Xna.Framework;

namespace EntityEnginev2.Data
{
    public static class RandomExtentions
    {
        public static bool RandomBool(this Random rand)
        {
            return (rand.Next(0, 2) == 0);
        }

        public static Color RandomColor(this Random rand)
        {
            var r = rand.Next(0, 256);
            var g = rand.Next(0, 256);
            var b = rand.Next(0, 256);
            return new Color(r, g, b);
        }
    }
}