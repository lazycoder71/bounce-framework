using TMPro;
using UnityEngine;

namespace Bounce.Framework
{
    public class UIPopupMessage : UIPopupBehaviour
    {
        [Header("Reference")]
        [SerializeField] TextMeshProUGUI _txtContent;

        public void Construct(string msg)
        {
            _txtContent.text = msg;
        }
    }
}