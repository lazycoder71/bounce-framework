using UnityEngine;

namespace Bounce.Framework
{
    public static class Vector2Extensions
    {
        public static Vector2 MultipliedBy(this in Vector2 a, Vector2 b)
        {
            b.x *= a.x;
            b.y *= a.y;

            return b;
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            return v.RotateRadians(degrees * Mathf.Deg2Rad);
        }

        public static Vector2 RotateRadians(this Vector2 v, float radians)
        {
            float ca = Mathf.Cos(radians);
            float sa = Mathf.Sin(radians);
            return new Vector2(ca * v.x - sa * v.y, sa * v.x + ca * v.y);
        }

        public static float ToAngle(this Vector2 v)
        {
            float angle = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
            if (angle < 0f)
                angle += 360f;

            return angle;
        }

        public static float RandomWithin(this Vector2 v)
        {
            return Random.Range(v.x, v.y);
        }
    }
}