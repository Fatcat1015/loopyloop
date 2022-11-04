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
             gm.initiatedLoop = true;
            //specify which death trigger
            if(other.gameObject.name == "killer trigger")
            gm.killer_death = true;
        }

    }


    public void predeath()
    {
        gameObject.GetComponent<PlayerMovementFPS>().enabled = false;
        blackscreen.SetActive(true);
    }

    public void resetPos()
    {
        gameObject.transform.position = respawnPos.transform.position;
        blackscreen.SetActive(false);
        gameObject.GetComponent<PlayerMovementFPS>().enabled = true;
    }
}
