using UnityEngine;

namespace Bounce.Framework
{
    public static class RectTransformExtensions
    {
        public static void SetWidth(this RectTransform rectTransform, float width)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        public static void SetHeight(this RectTransform rectTransform, float height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public static void TranslateX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + x, rectTransform.anchoredPosition.y);
        }

        public static void TranslateY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + y);
        }

        public static void TranslateXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + x, rectTransform.anchoredPosition.y + y);
        }

        public static void AnchorToCorners(this RectTransform transform)
        {
            if (transform.parent == null)
                return;

            var parent = transform.parent.GetComponent<RectTransform>();

            Vector2 newAnchorsMin = new Vector2(transform.anchorMin.x + transform.offsetMin.x / parent.rect.width,
                              transform.anchorMin.y + transform.offsetMin.y / parent.rect.height);

            Vector2 newAnchorsMax = new Vector2(transform.anchorMax.x + transform.offsetMax.x / parent.rect.width,
                              transform.anchorMax.y + transform.offsetMax.y / parent.rect.height);

            transform.anchorMin = newAnchorsMin;
            transform.anchorMax = newAnchorsMax;
            transform.offsetMin = transform.offsetMax = Vector2.zero;
        }
    }
}