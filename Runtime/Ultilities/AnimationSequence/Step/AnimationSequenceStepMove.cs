using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepMove : AnimationSequenceStep
    {
        [SerializeField] bool _isSelf = true;

        [ShowIf("@_isSelf == false")]
        [SerializeField] Transform _target;
        [SerializeField] AddType _addType;
        [SerializeField] float _duration;
        [SerializeField] bool _ignoreTimeScale;
        [SerializeField] Ease _ease;
        [SerializeField] Vector3 _position;
        [SerializeField] bool _snapping = false;
        [SerializeField] UpdateType _updateType;
        [ShowIf("@_addType == AnimationSequenceStep.AddType.Insert")]
        [SerializeField] float _insertTime;

        public override string displayName { get { return $"Move - {(_isSelf ? "Transform (This)" : _target)}"; } }

        public override void AddTweenToSequence(AnimationSequence animationSequence)
        {
            Transform target = _isSelf ? animationSequence.transformCached : _target;

            Tween tween = target.DOMove(_position, _duration, _snapping)
                                .ChangeStartValue(target.position)
                                .SetEase(_ease)
                                .SetUpdate(_updateType, _ignoreTimeScale);

            target.position += _position;

            switch (_addType)
            {
                case AddType.Append:
                    animationSequence.sequence.Append(tween);
                    break;
                case AddType.Join:
                    animationSequence.sequence.Join(tween);
                    break;
                case AddType.Insert:
                    animationSequence.sequence.Insert(_insertTime, tween);
                    break;

            }
        }
    }
}