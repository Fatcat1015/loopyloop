using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactables : MonoBehaviour
{
    public TextMeshProUGUI descriptions;
    [TextArea(3, 10)]
    public string[] interactions;
    public float textSpeed;

    private int index;

    void Start()
    {
        descriptions.text = string.Empty;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (RayCastFPS.description_object != null)
        {
           
            if (RayCastFPS.description_object.gameObject.name == "Newspaper")
            {
                Debug.Log("News");
                index = 0;
                StartCoroutine(TypeLine());
            }
            if (RayCastFPS.description_object.gameObject.name == "Loose Light")
            {
                index = 1;
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            descriptions.text = string.Empty;
            //dialogueBox.SetActive(false);
            this.gameObject.SetActive(false);
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
