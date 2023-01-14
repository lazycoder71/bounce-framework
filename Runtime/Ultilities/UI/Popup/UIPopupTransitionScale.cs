using DG.Tweening;
using Bounce.Framework;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIPopupTransitionScale : UIPopupTransition
    {
        [Header("References")]
        [SerializeField] Transform _target = null;

        [Header("Config")]
        [SerializeField] float _startScale = 0.3f;
        [SerializeField] Ease _ease = Ease.OutBack;
        [Range(0.1f, 1f)]
        [SerializeField] float _scaleDurationRatio = 1f;

        public override Tween ConstructTransition(UIPopupBehaviour popup)
        {
            if (_target == null)
                _target = popup.transform;

            // Set target start scale
            _target.SetScaleXY(_startScale);

            return _target.DOScale(Vector3.one, popup.openDuration * _scaleDurationRatio).SetEase(_ease);
        }
    }
}