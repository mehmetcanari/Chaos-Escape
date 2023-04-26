using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaos.Escape
{
    public class IKFormer : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private List<Transform> bones;
        [SerializeField] private Transform target;
        private const float InitiliazeTime = 0.1f;

        #endregion

        #region UNITY METHODS

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(InitiliazeTime);
            SetBonesParentToTarget();
        }

        #endregion

        #region PRIVATE METHODS

        private void SetBonesParentToTarget()
        {
            bones.ForEach(bone => bone.parent = target);
        }

        #endregion
    }
}