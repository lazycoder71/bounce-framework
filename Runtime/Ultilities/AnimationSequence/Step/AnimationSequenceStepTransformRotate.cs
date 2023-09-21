using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepTransformRotate : AnimationSequenceStepTransform
    {
        [SerializeField]
        private RotateMode _rotateMode = RotateMode.Fast;

        public override string displayName { get { return $"{(_isSelf ? "Transform (This)" : _owner)}: DOLocalRotate"; } }

        protected override Tween GetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween;

            Vector3 start;
            Vector3 end;

            if (_isUseTarget)
            {
                float duration = _isSpeedBased ? Vector3.Angle(_target.localEulerAngles, owner.localEulerAngles) / _duration : _duration;
                start = owner.localEulerAngles;
                end = _target.localEulerAngles;

                tween = owner.DOLocalRotate(end, duration, _rotateMode)
                             .ChangeStartValue(start);
            }
            else
            {
                float duration = _isSpeedBased ? Vector3.Angle(_value, owner.localEulerAngles) / _duration : _duration;
                start = owner.localEulerAngles;
                end = _relative ? owner.localEulerAngles + _value : _value;

                tween = owner.DOLocalRotate(end, duration, _rotateMode)
                             .ChangeStartValue(start);
            }

            owner.localEulerAngles = end;

            return tween;
        }

        protected override Tween GetResetTween(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            return owner.DORotate(owner.localEulerAngles, 0.0f);
        }
    }
}