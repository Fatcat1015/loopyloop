using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCastFPS : MonoBehaviour
{
    public GameObject interacting_object = null;
    public LayerMask Interactable_Layer;

    public Material outline;
    public Material Default_noOutline;
    public Ray ray;
    public int rayhitdistance = 25;

    public TMP_Text crosshair_txt;

    private void Update()
    {
        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayhitdistance, Interactable_Layer))
        {
            interacting_object = hit.transform.gameObject;
            if (interacting_object.GetComponent<Renderer>().sharedMaterial != outline) Default_noOutline = interacting_object.GetComponent<Renderer>().sharedMaterial;
            interacting_object.GetComponent<Renderer>().sharedMaterial = outline;
        }


        else if (interacting_object != null)
        {
            interacting_object.GetComponent<Renderer>().sharedMaterial = Default_noOutline;
            Default_noOutline = null;
            interacting_object = null;
        }

        if (interacting_object != null)
        {
            crosshair_txt.text = interacting_object.gameObject.name;
        }
        else
        {
            crosshair_txt.text = "";
        }
    }

}
