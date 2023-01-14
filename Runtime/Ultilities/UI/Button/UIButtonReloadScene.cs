using UnityEngine.SceneManagement;

namespace Bounce.Framework
{
    public class UIButtonReloadScene : UIButtonBase
    {
        protected override void Button_OnClick()
        {
            base.Button_OnClick();

            SceneLoaderHelper.Reload();
        }
    }
}