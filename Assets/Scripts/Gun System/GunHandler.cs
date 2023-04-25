using UnityEngine;

namespace Chaos.Escape
{
    public abstract class GunHandler : MonoBehaviour
    {
        #region PISTOL FUNCTIONS

        protected bool IsClicked()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) { return true; }
            return false;
        }

        #endregion

        #region RIFLE FUNCTIONS

        protected bool IsClickedHold()
        {
            if (UnityEngine.Input.GetMouseButton(0)) { return true; }
            return false;
        }

        #endregion
        
    }
}