using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chaos.Escape
{
    public abstract class Input : MonoBehaviour
    {
        #region PLAYER INPUTS

        protected bool IsPressedHold(KeyCode keyCode)
        {
            if (UnityEngine.Input.GetKey(keyCode)) { return true; }
            return false;
        }

        #endregion
    }
}

