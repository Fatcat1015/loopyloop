using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForce : MonoBehaviour
{
    Rigidbody rb;
    public int forceSpeed = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddForce(Vector3.right * forceSpeed);
        rb.useGravity = true;
    }
}
