using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepAction<T> : AnimationSequenceStep where T : class
    {
        [SerializeField]
        [HorizontalGroup("AddType")]
        private AddType _addType = AddType.Append;

        [SerializeField]
        [HorizontalGroup("AddType"), LabelWidth(75), SuffixLabel("Second(s)", true)]
        [ShowIf("@_addType == AnimationSequenceStep.AddType.Insert"), MinValue(0)]
        private float _insertTime = 0.0f;

        [SerializeField, HideInInspector]
        protected bool _isSelf = true;

        [SerializeField]
        [HorizontalGroup("Owner"), ShowIf("@_isSelf == false"), LabelWidth(75)]
        [GUIColor("@_owner == null ? new Color(1.0f, 0.2f, 0.2f) : Color.white")]
        protected T _owner;

        [SerializeField, HideInInspector]
        protected bool _isSpeedBased = false;

        [SerializeField, SuffixLabel("@_isSpeedBased?\"Unit/Second\":\"Second(s)\"", Overlay = true)]
        [InlineButton("@_isSpeedBased = true", Label = "Duration", ShowIf = ("@_isSpeedBased == false"))]
        [InlineButton("@_isSpeedBased = false", Label = "Speed Based", ShowIf = ("@_isSpeedBased == true"))]
        protected float _duration = 1.0f;

        [SerializeField]
        protected Ease _ease = Ease.Linear;

        [SerializeField, HorizontalGroup("Update")]
        [InlineButton("@_isIndependentUpdate = true", Label = "Timescale Based", ShowIf = ("@_isIndependentUpdate == false"))]
        [InlineButton("@_isIndependentUpdate = false", Label = "Independent Update", ShowIf = ("@_isIndependentUpdate == true"))]
        protected UpdateType _updateType = UpdateType.Normal;

        [SerializeField, HideInInspector]
        protected bool _isIndependentUpdate = false;

        [SerializeField]
        [MinValue(0), HorizontalGroup("Loop")]
        private int _loopTime = 0;

        [SerializeField]
        [ShowIf("@_loopTime != 0"), HorizontalGroup("Loop"), LabelWidth(75)]
        private LoopType _loopType = LoopType.Restart;

        [SerializeField]
        [HorizontalGroup("Target"), ShowIf("@_isUseTarget==true"), GUIColor("@_target == null ? new Color(1.0f, 0.2f, 0.2f) : Color.white")]
        [InlineButton("@_isUseTarget = false", Label = "Target")]
        protected T _target;

        [SerializeField, HideInInspector]
        protected bool _isUseTarget = false;

        [SerializeField, ShowIf("@_isUseTarget == false")]
        protected bool _relative;

        public override string displayName { get { return "ERROR!!!"; } }

        public override void AddToSequence(AnimationSequence animationSequence)
        {
            if (!Application.isPlaying)
            {
                switch (_addType)
                {
                    case AddType.Append:
                        animationSequence.sequence.Append(GetResetTween(animationSequence));
                        break;
                    case AddType.Join:
                        animationSequence.sequence.Join(GetResetTween(animationSequence));
                        break;
                    case AddType.Insert:
                        animationSequence.sequence.Insert(_insertTime, GetResetTween(animationSequence));
                        break;
                }
            }

            Tween tween = GetTween(animationSequence);

            tween.SetEase(_ease);
            tween.SetUpdate(_updateType, _isIndependentUpdate);
            tween.SetLoops(_loopTime, _loopType);

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

        protected virtual Tween GetTween(AnimationSequence animationSequence)
        {
            return null;
        }

        protected virtual Tween GetResetTween(AnimationSequence animationSequence)
        {
            return null;
        }

        [HorizontalGroup("Owner")]
        [ShowIf("@_isSelf == true")]
        [Button("SELF", Stretch = false, ButtonAlignment = 0), GUIColor(0, 1, 0), PropertyOrder(-1)]
        private void ToggleSelf1()
        {
            _isSelf = !_isSelf;
        }

        [HorizontalGroup("Owner", Width = 65)]
        [HideIf("@_isSelf == true")]
        [Button("OTHER", ButtonAlignment = 0), GUIColor(1, 1, 0), PropertyOrder(-1)]
        private void ToggleSelf2()
        {
            _isSelf = !_isSelf;
        }
    }
}