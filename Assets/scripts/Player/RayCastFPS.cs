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
    public LayerMask postprocessing_layer;
    public int interact_layernum;
    public int description_layernum;
    public int pp_layernum;

    public bool object_focused = false;

    public Ray ray;
    public int rayhitdistance = 25;

    public TMP_Text crosshair_txt;

    public DialogueUI dm;
    public Deathreset dr;

    private void Start()
    {
        interact_layernum = Mathf.RoundToInt(Mathf.Log(Interactable_Layer.value, 2));
        description_layernum = Mathf.RoundToInt(Mathf.Log(descriptionOnly_Layer.value,2));
        pp_layernum = Mathf.RoundToInt(Mathf.Log(postprocessing_layer.value,2));
    }

    private void Update()
    { 
        //actual code
        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        //determine if still focused on the previous object
        if (Physics.Raycast(ray, out hit, rayhitdistance, postprocessing_layer) )
        {
            if(hit.transform.gameObject == interacting_object || hit.transform.gameObject == description_object)
            {
                object_focused = true;
            }
            else
            {
                object_focused = false;
            }
            
        }
        else
        {
            object_focused = false;
        }

        //interactable obj
        if (Physics.Raycast(ray, out hit, rayhitdistance, Interactable_Layer))
        {
            interacting_object = hit.transform.gameObject;
            //change outline postprosessing layer of the selected object
            //if (interacting_object.layer != pp_layernum)
            //{
                //interacting_object.layer = pp_layernum;
            //}
        }


        else if (interacting_object != null )//get rid of outline
        {
            
                //interacting_object.layer = interact_layernum;
                interacting_object = null;
            
            
        }

        //display object name

        if (interacting_object != null)
        {
            crosshair_txt.text = "press E to " + interacting_object.name;
                //interact with objects
            if (Input.GetKeyDown(KeyCode.E))
            {
                //kill player if its death object
                if (interacting_object.CompareTag("death"))
                {
                    //killplayer
                    dr.StartDying();
                }else
                //load dialogues for interacting if there's one.

                if (interacting_object.GetComponent<DialogueLoad>() != null)
                {
                    if (!dm.speaking)
                    {
                        dm.dialogueLoader = interacting_object;
                        dm.LoadDialogue(null);
                    }
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
            //if(description_object.layer != pp_layernum)
            //description_object.layer = pp_layernum;
        }
        else
        {
            if(description_object != null)
            {
               
                    //description_object.layer = description_layernum;
                    description_object = null;
               
                    
            }
            
        }

        //see description of objects
        if(description_object != null)
        {
            crosshair_txt.text = "press E to interact with " + description_object.name;

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
