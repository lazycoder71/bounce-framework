using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Bounce.Framework
{
    public class AnimationSequenceStepCallback : AnimationSequenceStep
    {
        [HorizontalGroup]
        [SerializeField] bool _isInserted;

        [HorizontalGroup]
        [Min(0f)]
        [ShowIf("@_isInserted")]
        [SerializeField] float _insertTime;

        [SerializeField] UnityEvent _callback;

        public override string displayName { get { return "Callback"; } }

        public override void AddTweenToSequence(AnimationSequence animationSequence)
        {
            if (_isInserted)
                animationSequence.sequence.InsertCallback(_insertTime, () => { _callback?.Invoke(); });
            else
                animationSequence.sequence.AppendCallback(() => { _callback?.Invoke(); });
        }
    }
}