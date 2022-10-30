using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathreset : MonoBehaviour
{
    public GameObject respawnPos;
    public static int deathTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "death")
        {
            this.gameObject.transform.position = respawnPos.transform.position;
            deathTime ++;
            Debug.Log(deathTime);
        }
    }
}
