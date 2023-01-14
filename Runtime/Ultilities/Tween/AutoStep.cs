using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Bounce.Framework
{
    [System.Serializable]
    public class AutoStep
    {
        public virtual void Construct(AutoSequence autoSequence, Sequence sequence)
        {
        }

        public virtual Vector3 GetDeltaPosition()
        {
            return Vector3.zero;
        }
    }

    public class AutoStepCallback : AutoStep
    {
        [SerializeField] UnityEvent _event;

        [HorizontalGroup]
        [SerializeField] bool _insert;

        [ShowIf("@_insert == true")]
        [Min(0f)]
        [HorizontalGroup]
        [SerializeField] float _insertTime;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            if (!_insert)
                sequence.AppendCallback(() => { _event?.Invoke(); });
            else
                sequence.InsertCallback(_insertTime, () => { _event?.Invoke(); });
        }
    }

    public class AutoStepInterval : AutoStep
    {
        [SerializeField] float _interval;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            sequence.AppendInterval(_interval);
        }
    }

    public class AutoStepTween : AutoStep
    {
        [System.Serializable]
        protected enum JoinType
        {
            Append,
            Join,
            Insert,
        }

        [PropertyOrder(10)]
        [InlineButton("DurationBySpeed", "Speed")]
        [HorizontalGroup("StepTween_Duration")]
        [SerializeField] protected float _duration;

        [PropertyOrder(11)]
        [HorizontalGroup("StepTween_Duration")]
        [LabelWidth(100f)]
        [SerializeField] protected Ease _ease = Ease.Linear;

        [HorizontalGroup("StepTween_Loop")]
        [PropertyOrder(12)]
        [Min(0)]
        [SerializeField] int _loopTime;

        [PropertyOrder(13)]
        [HorizontalGroup("StepTween_Loop")]
        [ShowIf("@_loopTime != 0")]
        [LabelWidth(100f)]
        [SerializeField] LoopType _loopType;

        [PropertyOrder(14)]
        [HorizontalGroup("StepTween_JoinType")]
        [SerializeField] protected JoinType _joinType;

        [PropertyOrder(15)]
        [HorizontalGroup("StepTween_JoinType")]
        [ShowIf("@_joinType == JoinType.Insert")]
        [SerializeField] float _insertTime;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            Tween tween = ConstructTween(autoSequence, sequence);

            if (tween == null)
                return;

            tween.SetEase(_ease);
            tween.SetLoops(_loopTime, _loopType);

            switch (_joinType)
            {
                case JoinType.Append:
                    sequence.Append(tween);
                    break;
                case JoinType.Join:
                    sequence.Join(tween);
                    break;
                case JoinType.Insert:
                    sequence.Insert(_insertTime, tween);
                    break;
            }
        }

        protected virtual Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            return null;
        }

        protected void DurationBySpeed()
        {
            if (GetDeltaPosition() != Vector3.zero)
                _duration = GetDeltaPosition().magnitude / _duration;
        }
    }

    public class AutoStepTransform : AutoStepTween
    {
        [System.Serializable]
        public enum Type
        {
            Position,
            Rotation,
            Scale,
        }

        [BoxGroup("StepTransform", ShowLabel = false)]
        [SerializeField] Type _type;

        [BoxGroup("StepTransform", ShowLabel = false)]
        [HorizontalGroup("StepTransform/Value")]
        [SerializeField] Vector3 _end;

        [BoxGroup("StepTransform", ShowLabel = false)]
        [HorizontalGroup("StepTransform/Value")]
        [LabelWidth(50f)]
        [SerializeField] bool _persist;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            Tween tween = null;
            Transform transform = autoSequence.transformCached;

            switch (_type)
            {
                case Type.Position:
                    {
                        Vector3 end = _persist ? _end : transform.localPosition + _end;

                        if (_duration > 0f)
                        {
                            Vector3 start = transform.localPosition;

                            tween = transform.DOLocalMove(end, _duration)
                               .ChangeEndValue(end);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localPosition = end; });
                        }

                        transform.localPosition = end;
                    }
                    break;
                case Type.Rotation:
                    {
                        Vector3 end = _persist ? _end : transform.localEulerAngles + _end;

                        if (_duration > 0f)
                        {
                            tween = transform.DOLocalRotate(end, _duration)
                                .ChangeStartValue(transform.localEulerAngles);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localEulerAngles = end; });
                        }

                        transform.localEulerAngles = end;
                    }
                    break;
                case Type.Scale:
                    {
                        Vector3 end = _persist ? _end : transform.localScale + _end;

                        if (_duration > 0)
                        {
                            tween = transform.DOScale(end, _duration)
                               .ChangeStartValue(transform.localScale);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localScale = end; });
                        }

                        transform.localScale = end;
                    }
                    break;
            }

            return tween;
        }

        public override Vector3 GetDeltaPosition()
        {
            return _type == Type.Position ? _end : Vector3.zero;
        }
    }

    public class AutoStepRectTransform : AutoStepTween
    {
        [BoxGroup("StepRectTransform", ShowLabel = false)]
        [HorizontalGroup("StepRectTransform/Value")]
        [SerializeField] Vector3 _end;

        [BoxGroup("StepRectTransform", ShowLabel = false)]
        [HorizontalGroup("StepRectTransform/Value")]
        [LabelWidth(50f)]
        [SerializeField] bool _persist;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            Tween tween = null;
            RectTransform rectTransform = autoSequence.RectTransform;

            Vector3 end = Vector3.zero;

            if (_persist)
                end = _end;
            else
                end = rectTransform.anchoredPosition3D + _end;

            if (_duration > 0f)
            {
                tween = rectTransform.DOAnchorPos3D(end, _duration)
                   .ChangeStartValue(rectTransform.anchoredPosition3D);
            }
            else
            {
                sequence.AppendCallback(() => { rectTransform.anchoredPosition3D = end; });
            }

            rectTransform.anchoredPosition3D = end;

            return tween;
        }
    }

    public class AutoStepGraphicColor : AutoStepTween
    {
        [SerializeField] Color _color;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            return autoSequence.Graphic.DOColor(_color, _duration)
                .ChangeStartValue(autoSequence.Graphic.color);
        }
    }

    public class AutoStepCanvasGroup : AutoStepTween
    {
        [SerializeField] float _alpha;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            return autoSequence.CanvasGroup.DOFade(_alpha, _duration);
        }
    }

    public class AutoStepRigidbody2D : AutoStepTween
    {
        [System.Serializable]
        public enum Type
        {
            Position,
            Rotation,
        }

        [BoxGroup("StepRigidbody2D", ShowLabel = false)]
        [SerializeField] Type _type;

        [BoxGroup("StepRigidbody2D", ShowLabel = false)]
        [SerializeField] Vector3 _end;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            Tween tween = null;
            Rigidbody2D rigidbody = autoSequence.Rigidbody2D;
            Transform parent = autoSequence.transformCached.parent;

            switch (_type)
            {
                case Type.Position:
                    {
                        Vector3 end = rigidbody.transform.localPosition + _end;

                        if (_duration > 0f)
                        {
                            Vector3 pos = autoSequence.transformCached.localPosition;

                            tween = DOTween.To(() => pos,
                                    (x) =>
                                    {
                                        Vector3 next = parent == null ? x : parent.TransformPoint(x);

                                        rigidbody.MovePosition(next);
                                    },
                                    end, _duration).SetUpdate(UpdateType.Fixed);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { rigidbody.MovePosition(end); });
                        }

                        rigidbody.transform.localPosition = end;
                    }
                    break;
                case Type.Rotation:
                    {
                        Vector3 end = rigidbody.transform.localEulerAngles + _end;

                        if (_duration > 0f)
                        {
                            Vector3 rot = autoSequence.transformCached.localEulerAngles;

                            tween = DOTween.To(() => rot,
                                    (x) =>
                                    {
                                        Vector3 next = parent == null ? x : parent.TransformVector(x);

                                        rigidbody.MoveRotation(Quaternion.Euler(next));
                                    },
                                    end, _duration).SetUpdate(UpdateType.Fixed);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { rigidbody.MoveRotation(Quaternion.Euler(end)); });
                        }

                        rigidbody.transform.localEulerAngles = end;
                    }
                    break;
            }

            return tween;
        }

        public override Vector3 GetDeltaPosition()
        {
            return _type == Type.Position ? _end : Vector3.zero;
        }
    }
}
