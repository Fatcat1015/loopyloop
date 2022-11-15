using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathreset : MonoBehaviour
{
    public GameObject respawnPos;
    public GameObject blackscreen;
    public GameObject FPP;
    public gameManagersubway gm;

    //public GameObject restartPrompt;
    public FMODUnity.Footsteps footstepScript;
    public FMODUnity.runSteps runScript;
    public bool noSteps;

    private AudioSource audioS;
    public AudioClip lightClip;
    public AudioClip trainClip;
    public AudioClip killerClip;
    public AudioClip electricClip;
    public AudioClip chokingClip;

    bool laughDeath = false;
    bool electricDeath = false;
    public static bool teaDeath = false;
    bool lightfallDeath = false;
    //bool trainDeath = false;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        footstepScript.enabled = true;
        runScript.enabled = false;
        noSteps = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runScript.enabled = true;
            footstepScript.enabled = false;
        }
        else
        {
            runScript.enabled = false;
            if(noSteps)
            {
                footstepScript.enabled = false;
            }
            else
            {
                footstepScript.enabled = true;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death")//if collided with death object 
        {
            StartDying();
            //specify which death trigger
            if (other.gameObject.name == "killer trigger")
            {
                gm.killer_death = true;
                laughDeath = true;
            }
            if (other.gameObject.name == "light trigger")
            {
                gm.light_death = true;
                lightfallDeath = true;
            }

            if (other.gameObject.name == "electricity") gm.electricity_death = true;

        }

    }

    public void StartDying()
    {
        gm.initiatedLoop = true;
    }

    public void predeath()
    {
        gameObject.GetComponent<PlayerMovementFPS>().enabled = false;
        blackscreen.SetActive(true);
        footstepScript.enabled = false;
        runScript.enabled = false;
        noSteps = true;
        StartCoroutine(waitforAudio(gm.audiotime));
        gameObject.transform.position = respawnPos.transform.position;
        FPP.transform.rotation = respawnPos.transform.rotation;


        if (teaDeath)
        {
            Debug.Log("ice tea");
            audioS.PlayOneShot(chokingClip);
            teaDeath = false;
        }
        else if (gm.light_death || lightfallDeath)
        {
            Debug.Log("Light");
            audioS.PlayOneShot(lightClip);
            lightfallDeath = false;
        }
        else if (laughDeath)
        {
            Debug.Log("Killer");
            audioS.PlayOneShot(killerClip);
            laughDeath = false;
        }
        else if (electricDeath)
        {
            Debug.Log("Electrocated");
            audioS.PlayOneShot(electricClip);
            electricDeath = false;
        }
        else
        {
            Debug.Log("train come");
            audioS.PlayOneShot(trainClip);
            //trainDeath = false;
        }
        
    }

    public void resetPos()
    {
        gm.dead = false;
        blackscreen.SetActive(false);
        gameObject.GetComponent<PlayerMovementFPS>().enabled = true;
        footstepScript.enabled = true;
        noSteps = false;
    }

    IEnumerator waitforAudio(int time)
    {
        yield return new WaitForSeconds(time);
        gm.dead = true;
    }
}
