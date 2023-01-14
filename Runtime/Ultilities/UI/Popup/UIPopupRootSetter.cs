using UnityEngine;

namespace Bounce.Framework
{
    public class UIPopupRootSetter : MonoBehaviour
    {
        void Awake()
        {
            UIPopupHelper.popupRoot = transform;
        }
    }
}