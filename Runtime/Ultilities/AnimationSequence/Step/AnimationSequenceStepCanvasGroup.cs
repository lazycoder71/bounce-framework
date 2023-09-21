using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepCanvasGroup : AnimationSequenceStepAction<CanvasGroup>
    {
        [SerializeField] float _value;

        public override string displayName { get { return $"{(_isSelf ? "CanvasGroup (This)" : _owner)}: DOFade"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            CanvasGroup owner = _isSelf ? animationSequence.GetComponent<CanvasGroup>() : _owner;

            Tween tween;

            float start;
            float end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Mathf.Abs(_target.alpha - owner.alpha) / _duration : _duration;
                start = owner.alpha;
                end = _target.alpha;

                tween = owner.DOFade(end, duration)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Mathf.Abs(_target.alpha - owner.alpha) / _duration : _duration;
                start = owner.alpha;
                end = _relative ? owner.alpha + _value : _value;

                tween = owner.DOFade(end, duration)
                             .ChangeStartValue(start);
            }

            owner.alpha = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            CanvasGroup owner = _isSelf ? animationSequence.GetComponent<CanvasGroup>() : _owner;

            return owner.DOFade(owner.alpha, 0.0f);
        }
    }
}