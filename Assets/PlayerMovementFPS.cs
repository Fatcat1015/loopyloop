using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFPS : MonoBehaviour
{
    public CharacterController cc;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move*speed*Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity *-1);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * gravity * -0.001f) ;
    }
}
