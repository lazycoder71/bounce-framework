using UnityEngine;
using System;
using UnityEditor;

namespace Bounce.Framework.Editor
{
    public static class PGizmos
    {
        #region Public

        public static void DrawRectXY(Vector3 center, Vector2 size, Color? color = null)
        {
            Draw(color, () => { DrawRectXY(center, size); });
        }

        public static void DrawRectXZ(Vector3 center, Vector2 size, Color? color = null)
        {
            Draw(color, () => { DrawRectXZ(center, size); });
        }

        public static void DrawArc(Vector2 center, float startAngle, float endAngle, Color? color = null, int numPoint = 10, float radius = 1f)
        {
            Draw(color, () => { DrawArc(center, startAngle, endAngle, numPoint, radius); });
        }

        public static void DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color? color = null)
        {
            Draw(color, () => { DrawLine(center, center + (from * radius)); });
            DrawHandles(color, () => { DrawWireArc(center, normal, from, angle, radius); });
            Draw(color, () => { DrawLine(center, center + (Quaternion.AngleAxis(angle, normal) * from) * radius); });
        }

        public static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color? color = null)
        {
            DrawHandles(color, () => { DrawSolidArc(center, normal, from, angle, radius); });
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color? color = null)
        {
            Draw(color, () => { Gizmos.DrawLine(start, end); });
        }

        public static void DrawCircleXY(Vector3 center, float radius, Color? color = null, float angleStep = 10f)
        {
            Draw(color, () => { DrawCircle(center, radius, angleStep); });
        }

        public static void DrawCircleXZ(Vector3 center, float radius, Color? color = null, float? angleStep = null)
        {
            Draw(color, () => { DrawCircleXZ(center, radius, angleStep.GetValueOrDefault(10f)); });
        }

        public static void DrawWireSphere(Vector3 center, float radius, Color? color = null)
        {
            Draw(color, () => { Gizmos.DrawWireSphere(center, radius); });
        }

        public static void DrawSphere(Vector3 center, float radius, Color? color = null)
        {
            Draw(color, () => { Gizmos.DrawSphere(center, radius); });
        }

        public static void DrawRay(Vector3 from, Vector3 direction, Color? color = null)
        {
            Draw(color, () => { Gizmos.DrawRay(from, direction); });
        }

        public static void DrawWireCube(Vector3 center, Vector3 size, Color? color = null)
        {
            Draw(color, () => { Gizmos.DrawWireCube(center, size); });
        }

        public static void DrawFOV(Vector3 center, Vector3 direction, float fov, Color? color = null, float? length = null)
        {
            Draw(color, () => { DrawFOV(center, direction, fov, length.GetValueOrDefault(10.0f)); });
        }

        #endregion

        static void Draw(Color? color, Action callback)
        {
            if (color.HasValue)
            {
                Color gizmosColor = Gizmos.color;

                Gizmos.color = color.Value;
                callback();
                Gizmos.color = gizmosColor;
            }
            else
            {
                callback();
            }
        }

        static void DrawHandles(Color? color, Action callback)
        {
            if (color.HasValue)
            {
                Color handlesColor = Handles.color;

                Handles.color = color.Value;
                callback();
                Handles.color = handlesColor;
            }
            else
            {
                callback();
            }
        }

        static void DrawRectXY(Vector3 pos, Vector2 size)
        {
            Vector3 p1 = new Vector3(pos.x - size.x * 0.5f, pos.y - size.y * 0.5f, pos.z);
            Vector3 p2 = new Vector3(pos.x + size.x * 0.5f, pos.y - size.y * 0.5f, pos.z);
            Vector3 p3 = p2 + (Vector3.up * size.y);
            Vector3 p4 = p1 + (Vector3.up * size.y);

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }

        static void DrawRectXZ(Vector3 pos, Vector2 size)
        {
            Vector3 p1 = new Vector3(pos.x - size.x * 0.5f, pos.y, pos.z - size.y * 0.5f);
            Vector3 p2 = new Vector3(pos.x + size.x * 0.5f, pos.y, pos.z - size.y * 0.5f);
            Vector3 p3 = p2 + (Vector3.forward * size.y);
            Vector3 p4 = p1 + (Vector3.forward * size.y);

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }

        static void DrawArc(Vector2 center, float startAngle, float endAngle, int segments, float radius)
        {
            Vector2 lastPoint = Vector2.zero;
            float angle = startAngle;
            float arcLength = endAngle - startAngle;

            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                Vector2 currentPoint = new Vector2(x, y);

                if (i > 0)
                    Gizmos.DrawLine(center + lastPoint, center + currentPoint);

                lastPoint = currentPoint;

                angle += (arcLength / segments);
            }
        }

        static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawSolidArc(center, normal, from, angle, radius);
        }

        static void DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawWireArc(center, normal, from, angle, radius);
        }

        static void DrawCircle(Vector3 center, float radius, float angleStep = 10f)
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

                    Gizmos.DrawLine(from, to);

                    from = to;
                }
                else
                {
                    to.x = center.x + radius;
                    to.y = center.y;

                    Gizmos.DrawLine(from, to);

                    break;
                }
            }
            while (true);
        }

        static void DrawCircleXZ(Vector3 center, float radius, float angleStep)
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
                    to.z = center.z + Mathf.Sin(angle) * radius;
                    to.y = center.y;

                    Gizmos.DrawLine(from, to);

                    from = to;
                }
                else
                {
                    to.x = center.x + radius;
                    to.z = center.z;
                    to.y = center.y;

                    Gizmos.DrawLine(from, to);

                    break;
                }
            }
            while (true);
        }

        static void DrawFOV(Vector3 position, Vector3 direction, float fov, float length)
        {
            float halfFOV = fov * 0.5f;

            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

            Vector3 leftRayDirection = leftRayRotation * direction;
            Vector3 rightRayDirection = rightRayRotation * direction;

            Gizmos.DrawRay(position, leftRayDirection * length);
            Gizmos.DrawRay(position, rightRayDirection * length);
        }
    }
}