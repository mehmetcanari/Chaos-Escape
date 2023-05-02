using System;
using UnityEngine;

namespace Chaos.Escape
{
    public class InitiliazeSettings : MonoBehaviour
    {
        #region UNITY METHODS

        private void Awake()
        {
            SetFrameRate(120);
            EnableCursor(true);
        }

        #endregion
        
        #region PRIVATE METHODS
        
        private void SetFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
        
        private void EnableCursor(bool disable)
        {
            Cursor.visible = disable;
        }
        
        #endregion
    }
}