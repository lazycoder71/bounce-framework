using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    /// <summary>
    /// Safe area implementation for notched mobile devices. Usage:
    ///  (1) Add this component to the top level of any GUI panel. 
    ///  (2) If the panel uses a full screen background image, then create an immediate child and put the component on that instead, with all other elements childed below it.
    ///      This will allow the background image to stretch to the full extents of the screen behind the notch, which looks nicer.
    ///  (3) For other cases that use a mixture of full horizontal and vertical background stripes, use the Conform X & Y controls on separate elements as needed.
    /// </summary>
    public class UISafeArea : MonoBehaviour
    {
        [Title("Config")]
        [SerializeField] bool _conformX = true;  // Conform to screen safe area on X-axis (default true, disable to ignore)
        [SerializeField] bool _conformY = true;  // Conform to screen safe area on Y-axis (default true, disable to ignore)

        [Space]

        [SerializeField] bool _refreshOnUpdate = false;

        [Space]

        [SerializeField] bool _logging = false;  // Conform to screen safe area on Y-axis (default true, disable to ignore)

        RectTransform _rectTransform;

        Rect _lastSafeArea = new Rect(0, 0, 0, 0);

        Vector2Int _lastScreenSize = new Vector2Int(0, 0);

        ScreenOrientation _lastOrientation = ScreenOrientation.AutoRotation;

        RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        void OnEnable()
        {
            Refresh();
        }

        private void Update()
        {
            if (_refreshOnUpdate)
                Refresh();
        }

        void Refresh()
        {
            Rect safeArea = Screen.safeArea;

            if (safeArea != _lastSafeArea
                || Screen.width != _lastScreenSize.x
                || Screen.height != _lastScreenSize.y
                || Screen.orientation != _lastOrientation)
            {
                // Fix for having auto-rotate off and manually forcing a screen orientation.
                // See https://forum.unity.com/threads/569236/#post-4473253 and https://forum.unity.com/threads/569236/page-2#post-5166467
                _lastScreenSize.x = Screen.width;
                _lastScreenSize.y = Screen.height;
                _lastOrientation = Screen.orientation;

                ApplySafeArea(safeArea);
            }
        }

        void ApplySafeArea(Rect r)
        {
            _lastSafeArea = r;

            // Ignore x-axis?
            if (!_conformX)
            {
                r.x = 0;
                r.width = Screen.width;
            }

            // Ignore y-axis?
            if (!_conformY)
            {
                r.y = 0;
                r.height = Screen.height;
            }

            // Check for invalid screen startup state on some Samsung devices (see below)
            if (Screen.width > 0 && Screen.height > 0)
            {
                // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
                Vector2 anchorMin = r.position;
                Vector2 anchorMax = r.position + r.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;

                // Fix for some Samsung devices (e.g. Note 10+, A71, S20) where Refresh gets called twice and the first time returns NaN anchor coordinates
                // See https://forum.unity.com/threads/569236/page-2#post-6199352
                if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
                {
                    rectTransform.anchorMin = anchorMin;
                    rectTransform.anchorMax = anchorMax;
                }
            }

            if (_logging)
            {
                BDebug.Log($"New safe area applied to {name}: x={r.x}, y={r.y}, w={r.width}, h={r.height} on full extents w={Screen.width}, h={Screen.height}");
            }
        }
    }
}
