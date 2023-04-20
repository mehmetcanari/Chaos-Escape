using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaos.Escape
{
    public abstract class Input : MonoBehaviour
    {
        #region PLAYER INPUTS

        protected bool IsPressed(KeyCode _keyCode)
        {
            if (UnityEngine.Input.GetKeyDown(_keyCode)) { return true; }
            return false;
        }
        
        protected bool IsPressedHold(KeyCode _keyCode)
        {
            if (UnityEngine.Input.GetKey(_keyCode)) { return true; }
            return false;
        }

        #endregion
    }
}

