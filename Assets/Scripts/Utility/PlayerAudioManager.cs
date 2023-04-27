using UnityEngine;

namespace Chaos.Escape
{
    public class PlayerAudioManager : MonoBehaviour
    {
        #region INSPECTOR FIELDS

        [SerializeField] private AudioSource audioSource;

        #endregion

        #region PRIVATE FIELDS

        private bool _isPlaying;

        #endregion

        #region PUBLIC METHODS

        public void PlayStepSound()
        {
            if (_isPlaying) return;
            audioSource.Play();
            _isPlaying = true;
        }
        
        public void StopStepSound()
        {
            if (!_isPlaying) return;
            audioSource.Stop();
            _isPlaying = false;
        }

        #endregion
    }
}