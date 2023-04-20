using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chaos.Escape
{
    public abstract class Input : MonoBehaviour
    {
        protected UnityAction OnRelease;
        
        #region PLAYER INPUTS

        protected bool IsPressed(KeyCode keyCode)
        {
            if (UnityEngine.Input.GetKeyDown(keyCode)) { return true; }
            return false;
        }
        
        protected bool IsPressedHold(KeyCode keyCode)
        {
            if (UnityEngine.Input.GetKey(keyCode)) { return true; }
            return false;
        }

        protected bool IsReleased(KeyCode keyCode)
        {
            if (UnityEngine.Input.GetKeyUp(keyCode)) { return true; }

            return false;
        }

        #endregion
    }
}

