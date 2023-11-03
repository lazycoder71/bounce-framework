using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Bounce.Framework
{
    public static class ArrayExtensions
    {
        public static void Shuffle<T>(this T[] arr)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = arr.Length;
            while (n > 1)
            {
                byte[] box = new byte[1];

                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (Byte.MaxValue / n)));

                int k = (box[0] % n);
                n--;
                T value = arr[k];
                arr[k] = arr[n];
                arr[n] = value;
            }
        }

        public static T First<T>(this T[] array)
        {
            return array[0];
        }

        public static T Last<T>(this T[] array)
        {
            return array[array.Length - 1];
        }

        public static T GetClamp<T>(this T[] array, int index)
        {
            return array[Mathf.Clamp(index, 0, array.Length - 1)];
        }

        public static T GetRandom<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T GetTry<T>(this T[] array, int index) where T : class
        {
            if (index < 0 || index >= array.Length)
                return null;

            return array[index];
        }

        public static T[] Resize<T>(this T[] array, int size, T defaultValue = default(T))
        {
            if (array.Length == size)
                return array;

            T[] newArray = new T[size];
            for (int i = 0; i < newArray.Length; i++)
            {
                if (i >= array.Length)
                    newArray[i] = defaultValue;
                else
                    newArray[i] = array[i];
            }

            return newArray;
        }

        public static void SwapElements<T>(this T[] data, int index0, int index1)
        {
            T t = data[index0];
            data[index0] = data[index1];
            data[index1] = t;
        }

        public static T GetLoop<T>(this T[] array, int index)
        {
            return array[array.GetLoopIndex(index)];
        }

        public static void SetLoop<T>(this T[] array, int index, T item)
        {
            array[array.GetLoopIndex(index)] = item;
        }

        public static int GetLoopIndex<T>(this T[] array, int index)
        {
            if (index < 0)
                return (array.Length - (Mathf.Abs(index) % array.Length)) % array.Length;
            else
                return index % array.Length;
        }
    }
}
