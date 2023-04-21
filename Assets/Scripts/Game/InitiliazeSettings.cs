﻿using System;
using UnityEngine;

namespace Chaos.Escape
{
    public class InitiliazeSettings : MonoBehaviour
    {
        #region UNITY METHODS

        private void Awake()
        {
            SetFrameRate(144);
            DisableCursor();
        }

        #endregion
        
        #region PRIVATE METHODS
        
        private void SetFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
        
        private void DisableCursor()
        {
            Cursor.visible = false;
        }
        
        #endregion
    }
}