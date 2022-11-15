using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoad : MonoBehaviour
{
    [SerializeField]
    public List<string> Names = new List<string>();
    //[TextArea(3, 10)]
    public List<string> Dialogues = new List<string>();
    //[TextArea(3, 10)]
    public bool KillPlayer_afterDialogue = false;

    private void Start()
    {
        if(Names.Count < Dialogues.Count)
        {
            for (int i = Names.Count; i < Dialogues.Count; i ++)
            {
                Names.Add(" ");
            }
        }
    }
}
