using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    public Deathreset d;

    public void KillPlayer()
    {
        d.StartDying();
    }
}
