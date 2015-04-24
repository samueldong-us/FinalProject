using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Utilities
{
    internal static class MathUtilities
    {
        public static float angleBetween(Vector2 one, Vector2 two)
        {
            float cos = Vector2.Dot(one, two) / (one.Length() * two.Length());
            return (float)Math.Acos(cos);
        }
    }
}