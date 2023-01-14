using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Bounce.Framework
{
    /// <summary>
    /// Class that contains methods useful for debugging.
    /// All methods are only compiled if the DEVELOPMENT_BUILD symbol or UNITY_EDITOR is defined.
    /// </summary>
    public static class BDebug
    {
        #region Logs

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertIfTrue(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                Debug.LogErrorFormat(message, args);
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void AssertIfFalse(bool condition, string message, params object[] args)
        {
            if (!condition)
            {
                Debug.LogErrorFormat(message, args);
            }
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message, Color? color = null)
        {
            if (color.HasValue)
                Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color.Value) + ">" + message.ToString() + "</color>");
            else
                Debug.Log(message);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message, Color color, params object[] args)
        {
            Debug.LogFormat("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + message + "</color>", args);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log<T>(string message, params object[] args)
        {
            Debug.Log(string.Format("[{0}]:", typeof(T)) + string.Format(message, args));
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(string message, params object[] args)
        {
            Debug.LogWarningFormat(message, args);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogError(string message, params object[] args)
        {
            Debug.LogError(string.Format(message, args));
        }

        #endregion

        #region Draw

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawLine(Vector3 start, Vector3 end, Color? color = null, float? duration = null)
        {
            Debug.DrawLine(start, end, color.HasValue ? color.Value : Color.white, duration.GetValueOrDefault());
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawRay(Ray ray, Color? color = null, float? duration = null)
        {
            Debug.DrawRay(ray.origin, ray.direction, color.HasValue ? color.Value : Color.white, duration.GetValueOrDefault());
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawCircle(Vector3 center, float radius, float angleStep = 10f)
        {
            angleStep *= Mathf.Deg2Rad;

            Vector3 from = new Vector3(center.x + radius, center.y, center.z);
            Vector3 to = Vector3.zero;

            float angle = 0;
            float doublePI = Mathf.PI * 2f;

            do
            {
                angle += angleStep;

                if (angle < doublePI)
                {
                    to.x = center.x + Mathf.Cos(angle) * radius;
                    to.y = center.y + Mathf.Sin(angle) * radius;

                    Debug.DrawLine(from, to);

                    from = to;
                }
                else
                {
                    to.x = center.x + radius;
                    to.y = center.y;

                    Debug.DrawLine(from, to);

                    break;
                }
            }
            while (true);
        }

        // Draws just the box at where it is currently hitting.
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance, Color? color = null)
        {
            origin = CastCenterOnCollision(origin, direction, hitInfoDistance);

            DrawBox(origin, halfExtents, orientation, color.HasValue ? color.Value : Color.white);
        }

        // Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance, Color? color = null)
        {
            direction.Normalize();
            Box bottomBox = new Box(origin, halfExtents, orientation);
            Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

            DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft, color);
            DrawLine(bottomBox.backBottomRight, topBox.backBottomRight, color);
            DrawLine(bottomBox.backTopLeft, topBox.backTopLeft, color);
            DrawLine(bottomBox.backTopRight, topBox.backTopRight, color);
            DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft, color);
            DrawLine(bottomBox.frontTopRight, topBox.frontTopRight, color);
            DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft, color);
            DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight, color);

            DrawBox(bottomBox, color.HasValue ? color.Value : Color.white);
            DrawBox(topBox, color.HasValue ? color.Value : Color.white);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color? color = null)
        {
            DrawBox(new Box(origin, halfExtents, orientation), color.HasValue ? color.Value : Color.white);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void DrawBox(Box box, Color? color = null)
        {
            DrawLine(box.frontTopLeft, box.frontTopRight, color);
            DrawLine(box.frontTopRight, box.frontBottomRight, color);
            DrawLine(box.frontBottomRight, box.frontBottomLeft, color);
            DrawLine(box.frontBottomLeft, box.frontTopLeft, color);

            DrawLine(box.backTopLeft, box.backTopRight, color);
            DrawLine(box.backTopRight, box.backBottomRight, color);
            DrawLine(box.backBottomRight, box.backBottomLeft, color);
            DrawLine(box.backBottomLeft, box.backTopLeft, color);

            DrawLine(box.frontTopLeft, box.backTopLeft, color);
            DrawLine(box.frontTopRight, box.backTopRight, color);
            DrawLine(box.frontBottomRight, box.backBottomRight, color);
            DrawLine(box.frontBottomLeft, box.backBottomLeft, color);
        }

        public struct Box
        {
            public Vector3 localFrontTopLeft { get; private set; }
            public Vector3 localFrontTopRight { get; private set; }
            public Vector3 localFrontBottomLeft { get; private set; }
            public Vector3 localFrontBottomRight { get; private set; }
            public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
            public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
            public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
            public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

            public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
            public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
            public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
            public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
            public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
            public Vector3 backTopRight { get { return localBackTopRight + origin; } }
            public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
            public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

            public Vector3 origin { get; private set; }

            public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
            {
                Rotate(orientation);
            }
            public Box(Vector3 origin, Vector3 halfExtents)
            {
                this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
                this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
                this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

                this.origin = origin;
            }

            public void Rotate(Quaternion orientation)
            {
                localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
                localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
                localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
                localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
            }
        }

        //This should work for all cast types
        static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
        {
            return origin + (direction.normalized * hitInfoDistance);
        }

        static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            Vector3 direction = point - pivot;
            return pivot + rotation * direction;
        }

        #endregion
    }
}