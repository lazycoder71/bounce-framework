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

        public static Vector3Int RoundToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }

        public static Vector3Int FloorToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
        }

        public static Vector3Int CeilToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.CeilToInt(vector.x), Mathf.CeilToInt(vector.y), Mathf.CeilToInt(vector.z));
        }
    }
}