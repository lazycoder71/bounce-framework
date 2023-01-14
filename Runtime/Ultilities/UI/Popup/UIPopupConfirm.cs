using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    public class UIPopupConfirm : UIPopupBehaviour
    {
        [Header("Reference")]
        [SerializeField] TextMeshProUGUI _txtHeader;
        [SerializeField] TextMeshProUGUI _txtContent;
        [SerializeField] Button _btnYes;
        [SerializeField] Button _btnNo;

        Action<bool> _onConfirm;
        bool _confirmed = false;

        protected override void Awake()
        {
            base.Awake();

            _btnNo.onClick.AddListener(ButtonClickNoCallback);
            _btnYes.onClick.AddListener(ButtonClickYesCallback);

            _onClosed.AddListener(() => { _onConfirm?.Invoke(_confirmed); });
        }

        public void Construct(string header, string content, Action<bool> onConfirm)
        {
            _txtHeader.text = header;
            _txtContent.text = content;

            _onConfirm = onConfirm;
        }

        void ButtonClickNoCallback()
        {
            ForceClose();
        }

        void ButtonClickYesCallback()
        {
            _confirmed = true;

            ForceClose();
        }
    }
}
