using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextBox : MonoBehaviour
{
    public GameObject Text1;
    public GameObject dialogueBox;
    public RayCastFPS rcfps;

    void Update()
    {
        if(rcfps.interacting_object != null)
        {
            if (rcfps.interacting_object.gameObject.name == "Friend Temp" && Input.GetKeyDown(KeyCode.E))
            {
                Text1.SetActive(true);
                dialogueBox.SetActive(true);
            }
        }
        else
        {
            dialogueBox.SetActive(false);
            Text1.SetActive(false);
        }
    }
}
