using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextBox : MonoBehaviour
{
    public GameObject Text1;
    public GameObject dialogueBox;
    void Update()
    {
        if(RayCastFPS.interacting_object != null)
        {
            if (RayCastFPS.interacting_object.gameObject.name == "Friend Temp" && Input.GetKeyDown(KeyCode.E))
            {
                Text1.SetActive(true);
                dialogueBox.SetActive(true);
            }
        }
        else
        {
            dialogueBox.SetActive(false);
            Text1.SetActive(false);
            Debug.Log("Not hit");
        }
    }
}
