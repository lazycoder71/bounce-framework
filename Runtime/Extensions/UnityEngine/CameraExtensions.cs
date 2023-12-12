using UnityEngine;

namespace Bounce.Framework
{
    public static class CameraExtensions
    {
        public static void SetHeight(this Camera camera, float height)
        {
            camera.orthographicSize = camera.GetOrthoSizeFromHeight(height);
        }

        public static void SetWidth(this Camera camera, float width)
        {
            camera.orthographicSize = camera.GetOrthoSizeFromWidth(width);
        }

        public static float GetHeight(this Camera camera)
        {
            return camera.orthographicSize * 2.0f;
        }

        public static float GetOrthoSizeFromWidth(this Camera camera, float width)
        {
            return width / camera.aspect * 0.5f;
        }

        public static float GetOrthoSizeFromHeight(this Camera camera, float height)
        {
            return height * 0.5f;
        }

        public static float GetWidth(this Camera camera)
        {
            return camera.GetHeight() * camera.aspect;
        }

        public static float Top(this Camera camera)
        {
            return camera.transform.position.y + camera.GetHeight() * 0.5f;
        }

        public static float Bottom(this Camera camera)
        {
            return camera.transform.position.y - camera.GetHeight() * 0.5f;
        }

        public static float Right(this Camera camera)
        {
            return camera.transform.position.x + camera.GetWidth() * 0.5f;
        }

        public static float Left(this Camera camera)
        {
            return camera.transform.position.x - camera.GetWidth() * 0.5f;
        }
    }
}