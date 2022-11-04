using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DialogueUI : MonoBehaviour
{
    public GameObject parentobject;
    public TMP_Text NameText;
    public TMP_Text DialogueText;

    public List<Sprite> allPortraits = new List<Sprite>();
    public List<string> allCharaterNames = new List<string>();

    public Image DialogueBox;

    //put your dialogue path here
    [SerializeField]public string ReadingTextPath;

    public bool speaking = false;
    
    private List<string> allNames = new List<string>();
    public List<string> allDialogue = new List<string>();

    public int sentenceCount = 0;

    public bool typewriterEffect = true;
    public float typewriterEffectTime = 0.01f;
    public bool typewriting = false;

    public GameObject dialogueLoader;

    public RayCastFPS rc;

    private void Start()
    {
        //hide all dialogue components
        parentobject.SetActive(false);
    }

    private void Update()
    {
        //to trigger the dialogue
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (speaking)
            {
                NextSentence();
            }
        }
    }

    public void LoadDialogue(DialogueLoad DL)
    {
        /*
        StreamReader inp_stm = new StreamReader(filepath);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            // Do Something with the input. 
            var arr = inp_ln.Split(":");
            allNames.Add(arr[0]);
            allDialogue.Add(arr[1]);
        }
        //finish reading
        inp_stm.Close();
        */

        if(allDialogue.Count == 0)
        {
                DialogueLoad dl = DL == null ?  dialogueLoader.GetComponent<DialogueLoad>() : DL;

            for (int i = 0; i < dl.Dialogues.Count; i++)
            {
                allNames.Add(dl.Names[i]);
                allDialogue.Add(dl.Dialogues[i]);
            }
            dialogueLoader = null;
            if (allDialogue.Count > 0)
            {
                StartDialogue();
            }
        }
    }

    //function to start dialogues
    public void StartDialogue()
    {
        speaking = true;
        parentobject.SetActive(true);

        NameText.text = allNames[0];
        sentenceCount = 0;

        //changePortrait();

        if (!typewriterEffect)
        {
            DialogueText.text = allDialogue[0];
        }
        else
        {
            StartCoroutine(TypeWriterText());
        }
        

    }

    //function to proceed with the dialogue
    public void NextSentence()
    {

        if (typewriting)
        {
            StopAllCoroutines();
            DialogueText.text = allDialogue[sentenceCount];
            typewriting = false;
        }
        else
        {
            
            if (sentenceCount < allNames.Count - 1)
            {
                sentenceCount += 1;
                NameText.text = allNames[sentenceCount];
                //changePortrait();
                if (typewriterEffect)
                {
                    StartCoroutine(TypeWriterText());
                }
                else
                {
                    DialogueText.text = allDialogue[sentenceCount];
                }

            }
            else
            {
                EndDialogue();
            }
        }
        
        
    }

    public void EndDialogue()//hides all dialogue components
    {
        allNames.Clear();
        allDialogue.Clear();
        StartCoroutine(CoolDown());

        parentobject.SetActive(false);
    }

    IEnumerator CoolDown()
    {
        //see item description cool down
        yield return new WaitForSeconds(0.1f);
        speaking = false;
    }

    IEnumerator TypeWriterText()
    {
        typewriting = true;
        DialogueText.text = "";
        foreach (char c in allDialogue[sentenceCount])
        {
            DialogueText.text += c;
            
            yield return new WaitForSeconds(typewriterEffectTime);
        }
        typewriting = false;
    }

    /*
    void changePortrait()
    {
        for (int i = 0; i < allCharaterNames.Count; i++)
        {
            if (allCharaterNames[i] == NameText.text)
            {
                // Yes
                Portrait.sprite = allPortraits[i];
                break; //don't need to check the remaining ones now that we found one
            }
            else
            {
                Portrait.sprite = null;
            }
        }
    }*/


}
