using System;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIButtonOpenPopup : UIButtonBase
    {
        [SerializeField] GameObject _popup;

        public event Action<UIPopupBehaviour> eventSpawnPopup;

        protected override void Button_OnClick()
        {
            base.Button_OnClick();

            UIPopupBehaviour popup = UIPopupHelper.Create(_popup);

            eventSpawnPopup?.Invoke(popup);

            HandleSpawnPopup(popup);
        }

        protected virtual void HandleSpawnPopup(UIPopupBehaviour popupBehaviour)
        {

        }
    }
}