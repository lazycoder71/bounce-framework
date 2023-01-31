using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bounce.Framework
{
    public class AnimationSequenceStepMove : AnimationSequenceStep
    {
        [HideInInspector]
        [SerializeField]
        private bool _isSelf = true;

        [HorizontalGroup("Owner")]
        [ShowIf("@_isSelf == false")]
        [LabelWidth(70)]
        [GUIColor("OwnerFieldColor")]
        [SerializeField]
        private Transform _owner;

        [HorizontalGroup("AddType")]
        [SerializeField]
        private AddType _addType;

        [SuffixLabel("second(s)", Overlay = true)]
        [SerializeField]
        private float _duration;

        [SerializeField]
        private Ease _ease;

        [HorizontalGroup("Update")]
        [SerializeField]
        private UpdateType _updateType;

        [HorizontalGroup("Update")]
        [LabelWidth(120)]
        [SerializeField]
        private bool _ignoreTimeScale;

        [HorizontalGroup("Target")]
        [ShowIf("@_isUseTarget==false")]
        [SerializeField]
        private Vector3 _value;

        [HorizontalGroup("Target")]
        [ShowIf("@_isUseTarget==true"), GUIColor("TargetFieldColor")]
        [SerializeField]
        private Transform _target;

        [HideInInspector]
        [SerializeField]
        private bool _isUseTarget = false;

        [HideInInspector]
        [SerializeField]
        private bool _isFrom = false;

        [SerializeField]
        private bool _snapping = false;

        [ShowIf("@_isUseTarget == false")]
        [SerializeField]
        private bool _relative;

        [HorizontalGroup("AddType"), LabelWidth(75), SuffixLabel("second(s)", true)]
        [ShowIf("@_addType == AnimationSequenceStep.AddType.Insert"), MinValue(0)]
        [SerializeField]
        private float _insertTime;

        [SerializeField]
        [MinValue(0), HorizontalGroup("Loop")]
        private int _loopTime;

        [SerializeField]
        [ShowIf("@_loopTime > 0"), HorizontalGroup("Loop"), LabelWidth(75)]
        private LoopType _loopType;

        public override string displayName { get { return $"Move - {(_isSelf ? "Transform (This)" : _owner)}"; } }

        public override void AddTweenToSequence(AnimationSequence animationSequence)
        {
            Transform owner = _isSelf ? animationSequence.transformCached : _owner;

            Tween tween = owner.DOMove(_value, _duration, _snapping)
                               .ChangeStartValue(owner.position)
                               .SetEase(_ease)
                               .SetUpdate(_updateType, _ignoreTimeScale);

            if (!_isUseTarget)
                tween.SetRelative(_relative);

            owner.position += _value;

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

        [HorizontalGroup("Target", Width = 75)]
        [ShowIf("@_isUseTarget == true")]
        [Button("Target")]
        private void ToggleTarget1()
        {
            _isUseTarget = !_isUseTarget;
        }

        [HorizontalGroup("Target", Width = 75)]
        [HideIf("@_isUseTarget == true")]
        [Button("Value")]
        private void ToggleTarget2()
        {
            _isUseTarget = !_isUseTarget;
        }

        [HorizontalGroup("Target", Width = 75)]
        [ShowIf("@_isFrom == true")]
        [Button("From")]
        private void ToggleFrom1()
        {
            _isFrom = !_isFrom;
        }

        [HorizontalGroup("Target", Width = 75)]
        [HideIf("@_isFrom == true")]
        [Button("To")]
        private void ToggleFrom2()
        {
            _isFrom = !_isFrom;
        }

        private Color OwnerFieldColor()
        {
            return _owner == null ? new Color(1.0f, 0.2f, 0.2f) : Color.white;
        }

        private Color TargetFieldColor()
        {
            return _target == null ? new Color(1.0f, 0.2f, 0.2f) : Color.white;
        }
    }
}