using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathreset : MonoBehaviour
{
    public GameObject respawnPos;
    public GameObject blackscreen;
    public GameObject FPP;
    public gameManagersubway gm;

    public GameObject restartPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death")//if collided with death object 
        {
            StartDying();            
            //specify which death trigger
            if(other.gameObject.name == "killer trigger")gm.killer_death = true;

            if (other.gameObject.name == "light trigger") gm.light_death = true;
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
        StartCoroutine(waitforAudio(gm.audiotime));
        gameObject.transform.position = respawnPos.transform.position;
        FPP.transform.rotation = respawnPos.transform.rotation;
    }

    public void resetPos()
    {
        gm.dead = false;
        blackscreen.SetActive(false);
        restartPrompt.SetActive(false);
        gameObject.GetComponent<PlayerMovementFPS>().enabled = true;
    }

    IEnumerator waitforAudio(int time)
    {
        yield return new WaitForSeconds(time);
        gm.dead = true;
        restartPrompt.SetActive(true);
    }
}
