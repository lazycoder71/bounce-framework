using UnityEngine;

namespace Bounce.Framework
{
    public static class SpriteExtensions
    {
        public static float GetWidth(this Sprite sprite)
        {
            return sprite.bounds.size.x;
        }

        public static float GetHeight(this Sprite sprite)
        {
            return sprite.bounds.size.y;
        }

        public static Vector3 GetScale(this Sprite sprite, float desireWidth, float desireHeight, bool preserveAspect = false)
        {
            return sprite.GetScale(new Vector2(desireWidth, desireHeight), preserveAspect);
        }

        public static Vector3 GetScale(this Sprite sprite, Vector2 desireSize, bool preserveAspect = false)
        {
            if (preserveAspect)
            {
                float scale = Mathf.Max(desireSize.x / sprite.bounds.size.x, desireSize.y / sprite.bounds.size.y);
                return new Vector3(scale, scale, 1f);
            }
            else
            {
                return new Vector3(desireSize.x / sprite.bounds.size.x, desireSize.y / sprite.bounds.size.y, 1f);
            }
        }

        public static void GetUVs(this Sprite sprite, ref Vector2 uv0, ref Vector2 uvSize)
        {
            if (sprite == null)
            {
                BDebug.LogError("Can't get uvs, sprite is null");
                return;
            }

            Texture texture = sprite.texture;

            Rect rect = sprite.textureRect;
            Vector2 rectOffset = sprite.textureRectOffset;

            float uvLeft = (rect.x + rectOffset.x) / texture.width;
            float uvRight = uvLeft + rect.width / texture.width;
            float uvBottom = (rect.y + rectOffset.y) / texture.height;
            float uvTop = uvBottom + rect.height / texture.height;

            uv0.Set(uvLeft, uvBottom);
            uvSize.Set(uvRight - uvLeft, uvTop - uvBottom);
        }

        public static void GetUVs(this Sprite sprite, ref Vector2 uv0, ref Vector2 uv1, ref Vector2 uv2, ref Vector2 uv3)
        {
            float uvLeft, uvTop, uvRight, uvBottom;
            GetUVs(sprite, out uvLeft, out uvTop, out uvRight, out uvBottom);

            uv0.x = uv1.x = uvLeft;
            uv2.x = uv3.x = uvRight;
            uv0.y = uv2.y = uvBottom;
            uv1.y = uv3.y = uvTop;
        }

        public static void GetUVs(this Sprite sprite, out float uvLeft, out float uvTop, out float uvRight, out float uvBottom, bool flipX = false, bool flipY = false)
        {
            Texture texture = sprite.texture;
            if (texture != null)
            {
                Rect rect = sprite.textureRect;
                Vector2 rectOffset = sprite.textureRectOffset;

                uvLeft = (rect.x + rectOffset.x) / texture.width;
                uvRight = uvLeft + rect.width / texture.width;
                uvBottom = (rect.y + rectOffset.y) / texture.height;
                uvTop = uvBottom + rect.height / texture.height;

                if (flipX)
                {
                    Swap(ref uvLeft, ref uvRight);
                }

                if (flipY)
                {
                    Swap(ref uvTop, ref uvBottom);
                }
            }
            else
            {
                uvLeft = uvTop = uvRight = uvBottom = 0;
            }
        }

        static void Swap(ref float a, ref float b)
        {
            float temp = a;
            a = b;
            b = temp;
        }

        public static Vector2 NormalizedPivot(this Sprite sprite)
        {
            Bounds bounds = sprite.bounds;
            var pivotX = -bounds.center.x / bounds.extents.x * 0.5f + 0.5f;
            var pivotY = -bounds.center.y / bounds.extents.y * 0.5f + 0.5f;

            return new Vector2(pivotX, pivotY);
        }

        public static void FixPosByPivot(this Sprite sprite, ref float left, ref float top, ref float right, ref float bottom, bool flipX = false, bool flipY = false)
        {
            Vector2 pivot = sprite.NormalizedPivot();
            pivot.Set(pivot.x - (flipX ? (pivot.x - 0.5f) * 2f : 0f), pivot.y - (flipY ? (pivot.y - 0.5f) * 2f : 0f));

            float deltaX = (right - left) * (0.5f - pivot.x);
            float deltaY = (top - bottom) * (0.5f - pivot.y);

            left += deltaX;
            right += deltaX;
            top += deltaY;
            bottom += deltaY;
        }

        public static float GetFixedHeight(this Sprite sprite, float width)
        {
            return width / sprite.bounds.size.x * sprite.bounds.size.y;
        }

        public static float GetFixedWidth(this Sprite sprite, float height)
        {
            return height / sprite.bounds.size.y * sprite.bounds.size.x;
        }
    }
}
