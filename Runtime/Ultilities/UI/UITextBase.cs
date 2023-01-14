using TMPro;
using UnityEngine;

namespace Bounce.Framework
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextBase : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public TextMeshProUGUI text
        {
            get
            {
                if (_text == null)
                    _text = GetComponent<TextMeshProUGUI>();

                return _text;
            }
        }

        protected virtual void Awake()
        {
            _text = text;
        }
    }
}
