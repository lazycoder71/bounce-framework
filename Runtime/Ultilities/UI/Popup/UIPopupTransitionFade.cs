using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIPopupTransitionFade : UIPopupTransition
    {
        [Header("Config")]
        [Range(0.1f, 1f)]
        [SerializeField] float _durationRatio = 1f;

        public override Tween ConstructTransition(UIPopupBehaviour popup)
        {
            CanvasGroup canvasGroup = popup.canvasGroup;

            // Set target start scale
            canvasGroup.alpha = 0f;

            return canvasGroup.DOFade(1f, popup.openDuration * _durationRatio);
        }
    }
}