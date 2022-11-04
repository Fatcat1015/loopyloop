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

    private void Start()
    {
        interact_layernum = Mathf.RoundToInt(Mathf.Log(Interactable_Layer.value, 2));
        description_layernum = Mathf.RoundToInt(Mathf.Log(descriptionOnly_Layer.value,2));
        pp_layernum = Mathf.RoundToInt(Mathf.Log(postprocessing_layer.value,2));
    }

    private void Update()
    {
        //actual code
        RaycastCheck(Interactable_Layer, interacting_object, pp_layernum);
        //RaycastCheck(descriptionOnly_Layer, description_object, pp_layernum);
    }

    //function to check raycast in a specific layer, and displaying the name of the object into the UI
    void RaycastCheck(LayerMask layer_checking, GameObject selected_obj, int changing_layer)
    {
        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayhitdistance, layer_checking))
        {
            Debug.Log("Hit");
            //get rid of outline
            if (selected_obj != null && selected_obj != hit.transform.gameObject)
            {
                SetLayerAllChildren(selected_obj.transform, 0);
            }
            selected_obj = hit.transform.gameObject;
            //change outline postprosessing layer of the selected object

            SetLayerAllChildren(selected_obj.transform, changing_layer);
        }
        else
        {
            if (selected_obj != null)
            {
                SetLayerAllChildren(selected_obj.transform, 0);
                selected_obj.layer = Mathf.RoundToInt(Mathf.Log(Interactable_Layer.value, 2));
                selected_obj = null;
            }

        }

        //display object name
        if (selected_obj != null)
        {
            crosshair_txt.text = "press E to interact with " + selected_obj.name;
            //check for e if there's an object selected
            EforDialogue(selected_obj);
        }
        else
        {
            crosshair_txt.text = "";
        }


    }

    void EforDialogue(GameObject selected_obj)
    {
        if (selected_obj != null)
        {
            //interact with objects
            if (Input.GetKeyDown(KeyCode.E))
            {
                //load dialogues for interacting if there's one.
                if (selected_obj.GetComponent<DialogueLoad>() != null)
                {
                    if (!dm.speaking)
                    {
                        dm.dialogueLoader = selected_obj;
                        dm.LoadDialogue();
                    }
                }
            }
        }
        
    }

    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            //Debug.Log(child.name);
            child.gameObject.layer = layer;
        }
    }
}
