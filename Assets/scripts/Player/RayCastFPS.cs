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

    //public Material outline;
    //public Material Default_noOutline;
    //public Texture noOutline_tex;
    public Ray ray;
    public int rayhitdistance = 25;

    public TMP_Text crosshair_txt;

    public DialogueUI dm;

    private void Update()
    { 
        //actual code
        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        //interactable obj
        if (Physics.Raycast(ray, out hit, rayhitdistance, Interactable_Layer))
        {
            interacting_object = hit.transform.gameObject;
            //change outline texture of the selected object
            /*if (interacting_object.GetComponent<Renderer>().sharedMaterial != outline)
            {
                Default_noOutline = interacting_object.GetComponent<Renderer>().sharedMaterial;
                noOutline_tex = Default_noOutline.mainTexture;
            }
            if (noOutline_tex != null) outline.SetTexture("_Texture2D", noOutline_tex);
            interacting_object.GetComponent<Renderer>().sharedMaterial = outline;*/
        }
        else if (interacting_object != null)//get rid of outline texture
        {
            /*interacting_object.GetComponent<Renderer>().sharedMaterial = Default_noOutline;
            noOutline_tex = null;
            outline.SetTexture("_Texture2D", null);
            Default_noOutline = null;
            interacting_object = null;*/
        }

        //display object name

        if (interacting_object != null)
        {
            crosshair_txt.text = "press E to interact";
                //interact with objects
            if (Input.GetKeyDown(KeyCode.E))
            {
                //load dialogues for interacting if there's one.
                if (interacting_object.GetComponent<DialogueLoad>() != null)
                {
                    if (!dm.speaking)
                    {
                        dm.dialogueLoader = interacting_object;
                        dm.LoadDialogue();
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
        }
        else
        {
            description_object = null;
        }

        //see description of objects
        if(description_object != null)
        {
                if (!dm.speaking)
                {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dm.dialogueLoader = description_object;
                    dm.LoadDialogue();
                }
                }
        }

    }
}
