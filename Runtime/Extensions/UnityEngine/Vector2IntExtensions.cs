using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bounce.Framework
{
    public static class Vector2IntExtensions 
    {
        public static int RandomWithin(this Vector2Int v)
        {
            return Random.Range(v.x, v.y);
        }
    }
}