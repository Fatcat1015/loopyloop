using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathreset : MonoBehaviour
{
    public GameObject respawnPos;
    public GameObject blackscreen;
    public gameManagersubway gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death")//if collided with death object 
        {
            StartDying();            
            //specify which death trigger
            if(other.gameObject.name == "killer trigger")
            gm.killer_death = true;
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
        gameObject.transform.position = respawnPos.transform.position;
    }

    public void resetPos()
    {
        gm.dead = false;
        blackscreen.SetActive(false);
        gameObject.GetComponent<PlayerMovementFPS>().enabled = true;
    }
}
