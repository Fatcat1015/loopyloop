using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI sentences;
    [TextArea(3, 10)]
    public string[] lines;
    public float textSpeed;

    private int index;

    void Start()
    {
        sentences.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (sentences.text == lines[index])
            {
                NextLine();
            }
            else
            {
                //fast forward through the typing
                StopAllCoroutines();
                sentences.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    //typing effect
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            sentences.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            sentences.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
