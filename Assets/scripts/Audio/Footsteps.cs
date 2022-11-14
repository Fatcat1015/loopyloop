using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
   public class Footsteps : MonoBehaviour
    {
        [SerializeField]EventReference inputSound;
        public static bool playerismoving;
        public float walkingspeed;
        public static bool isDead;

        private void Update()
        {         
            if(Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
            {
                //Debug.Log("move");
                playerismoving = true;
            }
            else if(Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
            {
                //Debug.Log("not move");
                playerismoving = false;
            }
        }

        void CallFootsteps()
        {
            if(playerismoving == true)
            {
                Debug.Log("move");
                FMODUnity.RuntimeManager.PlayOneShotAttached(inputSound, gameObject);
            }
        }

        private void Start()
        {
            InvokeRepeating("CallFootsteps", 0, walkingspeed);
        }

        private void OnDisable()
        {
            playerismoving = false;
        }
    }
}

