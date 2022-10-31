using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathreset : MonoBehaviour
{
    public GameObject respawnPos;
    public GameObject blackscreen;

    public static int deathTime = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death")
        {
            StartCoroutine(deathBlack());
            //this.gameObject.transform.position = respawnPos.transform.position;
            deathTime ++;
            Debug.Log(deathTime);
        }
    }

    IEnumerator deathBlack()
    {
        //stop player control
        gameObject.GetComponent<PlayerMovementFPS>().enabled = false;
        //black screen
        blackscreen.SetActive(true);
        yield return new WaitForSeconds(5);
        //recovers
        gameManagersubway.initiatedLoop = true;
        blackscreen.SetActive(false);
        gameObject.GetComponent<PlayerMovementFPS>().enabled = true;
        gameObject.transform.position = respawnPos.transform.position;
    }
}
