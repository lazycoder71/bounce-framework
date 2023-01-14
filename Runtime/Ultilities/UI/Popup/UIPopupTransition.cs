using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public abstract class UIPopupTransition : MonoBehaviour
    {
        public abstract Tween ConstructTransition(UIPopupBehaviour popup);
    }
}