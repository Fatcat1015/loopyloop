using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCastFPS : MonoBehaviour
{
    [SerializeField]
    public  GameObject interacting_object = null;
    [SerializeField]
    public  GameObject description_object = null;
    public LayerMask Interactable_Layer;
    public LayerMask descriptionOnly_Layer;

    public Ray ray;
    public int rayhitdistance = 25;

    public TMP_Text crosshair_txt;

    public DialogueUI dm;
    public Deathreset dr;
    public gameManagersubway gm;

    private void Update()
    { 
        //actual code
        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;


        //interactable obj
        if (Physics.Raycast(ray, out hit, rayhitdistance, Interactable_Layer))
        {
            interacting_object = hit.transform.gameObject;
        }
        else if (interacting_object != null )//get rid of outline
        {
                interacting_object = null;
        }

        //display object name

        if (interacting_object != null)
        {
            crosshair_txt.text = "press E to " + interacting_object.name;
                //interact with objects
            if (Input.GetKeyDown(KeyCode.E))
            {
              
                //load dialogues for interacting if there's one.

                if (interacting_object.GetComponent<DialogueLoad>() != null)
                {
                    if (!dm.speaking)
                    {
                        dm.dialogueLoader = interacting_object;
                        dm.LoadDialogue(null);
                    }
                }
                //kill player if its death object
                if (interacting_object.CompareTag("death"))//ice tea death
                {
                    //killplayer
                    dr.StartDying();
                    gm.vendingMachine_death = true;
                }
                
                if (interacting_object.CompareTag("VM"))
                {
                    gm.IceTea.SetActive(true);
                }

                if (interacting_object.CompareTag("ad"))
                {
                    gm.RemoveAD(interacting_object);
                }

                if (interacting_object.CompareTag("Security Cam"))
                {
                    interacting_object.SetActive(false);
                    interacting_object = null;
                    gm.Cameras_investigated += 1;
                }
            }
        }
        else
        {
            crosshair_txt.text = "";
        }

        //description obj
        if (Physics.Raycast(ray, out hit, rayhitdistance, descriptionOnly_Layer))
        {
            description_object = hit.transform.gameObject;
        }
        else
        {
            if(description_object != null)
            {
                    description_object = null;      
            }
            
        }

        //see description of objects
        if(description_object != null)
        {
            crosshair_txt.text = "press E to look at " + description_object.name;

            if (!dm.speaking)
                {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dm.dialogueLoader = description_object;
                    dm.LoadDialogue(null);
                    
                }
                }
        }

    }
}
