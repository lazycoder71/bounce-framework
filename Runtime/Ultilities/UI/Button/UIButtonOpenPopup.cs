using System;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIButtonOpenPopup : UIButtonBase
    {
        [SerializeField] GameObject _popup;

        public event Action<UIPopupBehaviour> eventSpawnPopup;

        public override void Button_OnClick()
        {
            base.Button_OnClick();

            SpawnPopup();
        }

        protected virtual void HandleSpawnPopup(UIPopupBehaviour popupBehaviour)
        {

        }

        public UIPopupBehaviour SpawnPopup()
        {
            UIPopupBehaviour popup = UIPopupHelper.Create(_popup);

            eventSpawnPopup?.Invoke(popup);

            HandleSpawnPopup(popup);

            return popup;
        }
    }
}