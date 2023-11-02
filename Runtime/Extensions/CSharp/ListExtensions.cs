using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bounce.Framework
{
    public static class ListExtensions
    {
        public static void Resize<T>(this List<T> list, int newSize, T defaultValue = default(T))
        {
            int currentSize = list.Count;
            if (newSize < currentSize)
            {
                list.RemoveRange(newSize, currentSize - newSize);
            }
            else if (newSize > currentSize)
            {
                if (newSize > list.Capacity)
                    list.Capacity = newSize;

                list.AddRange(Enumerable.Repeat(defaultValue, newSize - currentSize));
            }
        }

        public static void Shuffle<T>(this List<T> list)
        {
            System.Random random = new System.Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Swap<T>(this List<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        public static bool IsOutOfBounds<T>(this IList<T> list, int index)
        {
            if (list == null)
                return true;

            if (index < 0 || index >= list.Count)
                return true;

            return false;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                return true;

            return false;
        }

        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }

        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T First<T>(this List<T> list)
        {
            return list[0];
        }

        public static T GetClamp<T>(this List<T> list, int index)
        {
            return list[Mathf.Clamp(index, 0, list.Count - 1)];
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T GetLoop<T>(this List<T> list, int index)
        {
            return list[list.GetLoopIndex(index)];
        }

        public static void SetLoop<T>(this List<T> list, int index, T item)
        {
            list[list.GetLoopIndex(index)] = item;
        }

        public static int GetLoopIndex<T>(this List<T> list, int index)
        {
            if (index < 0)
                return list.Count - (Mathf.Abs(index) % list.Count);
            else
                return index % list.Count;
        }
    }
}