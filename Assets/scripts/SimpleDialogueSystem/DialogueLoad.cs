using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoad : MonoBehaviour
{
    [SerializeField]
    public List<string> Names = new List<string>();
    public List<string> Dialogues = new List<string>();
    public bool KillPlayer_afterDialogue = false;
}
