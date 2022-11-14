using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FMODUnity {
    public class triggleAudio : MonoBehaviour
    {
        [SerializeField] EventReference Event;
        public bool PlayOnAwake;

        public void PlayOneShot()
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
        }

        private void Start()
        {
            if(PlayOnAwake)
            {
                PlayOneShot();
            }
        }
    }
}
