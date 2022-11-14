using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
    public class runSteps : MonoBehaviour
    {
        [SerializeField] EventReference inputSound;
        public static bool playerismoving;
        public float runSpeed;
        public static bool isDead;

        private void Update()
        {
            if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
            {
                //Debug.Log("move");
                playerismoving = true;
            }
            else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
            {
                //Debug.Log("not move");
                playerismoving = false;
            }
        }

        void CallFootsteps()
        {
            if (playerismoving == true)
            {
                Debug.Log("move");
                FMODUnity.RuntimeManager.PlayOneShotAttached(inputSound, gameObject);
            }
        }

        private void Start()
        {
            InvokeRepeating("CallFootsteps", 0, runSpeed);
        }

        private void OnDisable()
        {
            playerismoving = false;
        }
    }
}

