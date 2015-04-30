using Microsoft.Xna.Framework;
using System;

namespace FinalProject.Utilities
{
    internal static class UtilitiesMath
    {
        public static float AngleBetween(Vector2 one, Vector2 two)
        {
            float cos = Vector2.Dot(one, two) / (one.Length() * two.Length());
            return (float)Math.Acos(cos);
        }

        public static Vector2 VectorFromMagnitudeAndAngle(float magnitude, float angle)
        {
            return new Vector2((float)(magnitude * Math.Cos(angle)), (float)(magnitude * Math.Sin(angle)));
        }
    }
}