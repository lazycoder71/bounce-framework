using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    public class AnimationSequenceStepGraphicColor : AnimationSequenceStepAction<Graphic>
    {
        [SerializeField]
        [HorizontalGroup("Target"), ShowIf("@_isUseTarget == false")]
        [InlineButton("@_isUseTarget = true", Label = "Value")]
        protected Color _value;

        public override string displayName { get { return $"{(_isSelf ? "Graphic (This)" : _owner)}: Color To {(_isUseTarget ? (_target == null ? "Null" : _target.name) : _value)}"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Graphic owner = _isSelf ? animationSequence.graphic : _owner;

            Tween tween;

            Color start;
            Color end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Mathf.Abs(_target.color.Magnitude() - owner.color.Magnitude()) / _duration : _duration;
                start = owner.color;
                end = _target.color;

                tween = owner.DOColor(end, duration)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Mathf.Abs(_value.Magnitude() - owner.color.Magnitude()) / _duration : _duration;
                start = owner.color;
                end = _relative ? owner.color + _value : _value;

                tween = owner.DOColor(end, duration)
                             .ChangeStartValue(start);
            }

            owner.color = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Graphic owner = _isSelf ? animationSequence.graphic : _owner;

            return owner.DOColor(owner.color, 0.0f);
        }
    }
}