using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bounce.Framework
{
    public static class TransformExtensions
    {
        #region Position

        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static void SetXY(this Transform transform, float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }

        public static void SetXZ(this Transform transform, float x, float z)
        {
            transform.position = new Vector3(x, transform.position.y, z);
        }

        public static void SetYZ(this Transform transform, float y, float z)
        {
            transform.position = new Vector3(transform.position.x, y, z);
        }

        public static void SetXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        public static void SetLocalX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        public static void SetLocalXY(this Transform transform, float x, float y)
        {
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }

        public static void SetLocalXZ(this Transform transform, float x, float z)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, z);
        }

        public static void SetLocalYZ(this Transform transform, float y, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, z);
        }

        public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        public static void TranslateX(this Transform transform, float x)
        {
            transform.position += new Vector3(x, 0f, 0f);
        }

        public static void TranslateY(this Transform transform, float y)
        {
            transform.position += new Vector3(0f, y, 0f);
        }

        public static void TranslateZ(this Transform transform, float z)
        {
            transform.position += new Vector3(0f, 0f, z);
        }

        public static void TranslateXY(this Transform transform, float x, float y)
        {
            transform.position += new Vector3(x, y, 0f);
        }

        public static void TranslateXZ(this Transform transform, float x, float z)
        {
            transform.position += new Vector3(x, 0f, z);
        }

        public static void TranslateYZ(this Transform transform, float y, float z)
        {
            transform.position += new Vector3(0f, y, z);
        }

        public static void TranslateXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position += new Vector3(x, y, z);
        }

        public static void TranslateLocalX(this Transform transform, float x)
        {
            transform.localPosition += new Vector3(x, 0f, 0f);
        }

        public static void TranslateLocalY(this Transform transform, float y)
        {
            transform.localPosition += new Vector3(0f, y, 0f);
        }

        public static void TranslateLocalZ(this Transform transform, float z)
        {
            transform.localPosition += new Vector3(0f, 0f, z);
        }

        public static void TranslateLocalXY(this Transform transform, float x, float y)
        {
            transform.localPosition += new Vector3(x, y, 0f);
        }

        public static void TranslateLocalXZ(this Transform transform, float x, float z)
        {
            transform.localPosition += new Vector3(x, 0f, z);
        }

        public static void TranslateLocalYZ(this Transform transform, float y, float z)
        {
            transform.localPosition += new Vector3(0f, y, z);
        }

        public static void TranslateLocalXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition += new Vector3(x, y, z);
        }

        #endregion

        #region Scale

        public static void SetScaleX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
        }

        public static void SetScaleXY(this Transform transform, float x, float y)
        {
            transform.localScale = new Vector3(x, y, transform.localScale.z);
        }

        public static void SetScaleXY(this Transform transform, float xy)
        {
            transform.localScale = new Vector3(xy, xy, transform.localScale.z);
        }

        public static void SetScaleXZ(this Transform transform, float x, float z)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, z);
        }

        public static void SetScaleYZ(this Transform transform, float y, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, z);
        }

        public static void SetScale(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(x, y, z);
        }

        public static void SetScale(this Transform transform, float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        public static void ScaleByX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
        }

        public static void ScaleByY(this Transform transform, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z);
        }

        public static void ScaleByZ(this Transform transform, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * z);
        }

        public static void ScaleByXY(this Transform transform, float x, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z);
        }

        public static void ScaleByXZ(this Transform transform, float x, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z * z);
        }

        public static void ScaleByYZ(this Transform transform, float y, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z * z);
        }

        public static void ScaleByXY(this Transform transform, float r)
        {
            transform.ScaleByXY(r, r);
        }

        public static void ScaleByXZ(this Transform transform, float r)
        {
            transform.ScaleByXZ(r, r);
        }

        public static void ScaleByYZ(this Transform transform, float r)
        {
            transform.ScaleByYZ(r, r);
        }

        public static void ScaleByXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(
                x, y, z);
        }

        public static void ScaleByXYZ(this Transform transform, float r)
        {
            transform.ScaleByXYZ(r, r, r);
        }

        #endregion

        #region Rotation

        public static void RotateAroundX(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(angle, 0f, 0f));
        }

        public static void RotateAroundY(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(0f, angle, 0f));
        }

        public static void RotateAroundZ(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(0f, 0f, angle));
        }

        public static void SetRotationX(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        public static void SetRotationY(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }

        public static void SetRotationZ(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }

        public static void SetLocalRotationX(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        public static void SetLocalRotationY(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
        }

        public static void SetLocalRotationZ(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
        }

        #endregion

        #region Children

        public static List<Transform> GetChildren(this Transform transform)
        {
            var children = new List<Transform>();

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                children.Add(child);
            }

            return children;
        }

        public static void Sort(this Transform transform, Func<Transform, IComparable> sortFunction)
        {
            var children = transform.GetChildren();
            var sortedChildren = children.OrderBy(sortFunction).ToList();

            for (int i = 0; i < sortedChildren.Count(); i++)
            {
                sortedChildren[i].SetSiblingIndex(i);
            }
        }

        public static void SortAlphabetically(this Transform transform)
        {
            transform.Sort(t => t.name);
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public static void SetActiveChildren(this Transform target, bool isActive)
        {
            for (int i = target.childCount - 1; i >= 0; i--)
            {
                target.GetChild(i).gameObject.SetActive(isActive);
            }
        }

        public static void RecursiveAction(this Transform target, Action<Transform> action)
        {
            action(target);

            if (target.childCount > 0)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    target.GetChild(i).RecursiveAction(action);
                }
            }
        }

        public static void RecursiveAction<T>(this Transform target, Action<T> action) where T : Component
        {
            action(target.GetComponent<T>());

            if (target.childCount > 0)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    target.GetChild(i).RecursiveAction(action);
                }
            }
        }

        #endregion
    }
}