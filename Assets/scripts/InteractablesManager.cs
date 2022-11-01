using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractablesManager : MonoBehaviour
{
    public TMP_Text descriptions;
    [TextArea(3, 10)]
    public string[] interactions;
    public float textSpeed;
    public RayCastFPS rcfps;

    private int index;

    void Start()
    {
        descriptions.text = string.Empty;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (rcfps.description_object != null)
        {
           
            if (rcfps.description_object.gameObject.name == "Newspaper")
            {
                index = 2;
                StartCoroutine(TypeLine());
            }
            if (rcfps.description_object.gameObject.name == "Loose Light")
            {
                index = 1;
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            descriptions.text = string.Empty;
            //dialogueBox.SetActive(false);
            //this.gameObject.SetActive(false);
        }
    }

    //typing effect
    IEnumerator TypeLine()
    {
        foreach (char c in interactions[index].ToCharArray())
        {
            descriptions.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
