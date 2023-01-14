using UnityEngine;

namespace Bounce.Framework
{
    public static class Vector3Extensions
    {
        public static Vector3 MultipliedBy(this in Vector3 a, Vector3 b)
        {
            b.x *= a.x;
            b.y *= a.y;
            b.z *= a.z;

            return b;
        }
    }
}