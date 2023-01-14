using DG.Tweening;
using Bounce.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    public class UIPopupTransitionBackground : UIPopupTransition
    {
        [Header("Behaviour")]
        [SerializeField] bool _closeOnClick = true;
        [SerializeField] Color _color = new Color(0f, 0f, 0f, 0.6f);
        [Range(0.1f, 1f)]
        [SerializeField] float _durationRatio = 0.5f;

        UIPopupBehaviour _popup;
        GameObject _objBG;

        public override Tween ConstructTransition(UIPopupBehaviour popup)
        {
            // Assign popup reference
            _popup = popup;

            //Listen to popup event
            popup.onClosed.AddListener(Popup_OnClosed);

            // Spawn background
            SpawnBackground(popup);

            // Return background fade tween
            Image imgBG = _objBG.GetComponent<Image>();
            imgBG.color = _color;

            return imgBG.DOFade(_color.a, popup.openDuration * _durationRatio)
                .ChangeStartValue(new Color(_color.r, _color.g, _color.b, 0f))
                .SetEase(Ease.Linear);
        }

        void SpawnBackground(UIPopupBehaviour popup)
        {
            _objBG = new GameObject("BG");

            RectTransform rtBG = _objBG.AddComponent<RectTransform>();
            rtBG.SetParent(popup.transform.parent);
            rtBG.SetSiblingIndex(popup.transform.GetSiblingIndex());
            rtBG.anchoredPosition3D = Vector3.zero;
            rtBG.SetWidth(Screen.width * 2f);
            rtBG.SetHeight(Screen.height * 2f);

            _objBG.AddComponent<Image>();

            Button btnBG = _objBG.AddComponent<Button>();
            btnBG.transition = Selectable.Transition.None;
            btnBG.onClick.AddListener(ButtonBackground_OnClick);
        }

        void ButtonBackground_OnClick()
        {
            if (_closeOnClick)
                _popup.Close();
        }

        void Popup_OnClosed()
        {
            Destroy(_objBG);
        }
    }
}