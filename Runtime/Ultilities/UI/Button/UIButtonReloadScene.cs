using UnityEngine.SceneManagement;

namespace Bounce.Framework
{
    public class UIButtonReloadScene : UIButtonBase
    {
        public override void Button_OnClick()
        {
            base.Button_OnClick();

            SceneLoaderHelper.Reload();
        }
    }
}