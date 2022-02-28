using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace ConsoleGame
{
    public class StrawberryUtils
    {
        // Catch current game instance
        protected Game _game = Game1._game;

        public class Math : StrawberryUtils
            {
                public static float Lerp(float valueA, float valueB, float amount)
                {
                    return valueA * (1 - amount) + valueB * amount;
                }

                public static Vector2 Lerp(Vector2 valueA, Vector2 valueB, float amount)
                {
                    float retX = Lerp(valueA.X, valueB.X, amount);
                    float retY = Lerp(valueA.Y, valueB.Y, amount);

                    return new Vector2(retX, retY);
                }
              

            }


        public class Graphics : StrawberryUtils
        {
            public static void DrawSprite(Texture2D sprite, Vector2 position, Color color, SpriteBatch _spriteBatch)
            {
                //_spriteBatch.Begin();
                _spriteBatch.Draw(sprite, position, color);
                //_spriteBatch.End();

            }

        }
    }
}
