using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFPS : MonoBehaviour
{
    [Header("basic Functions")]
    public CharacterController cc;
    public float walk_speed = 12f;
    public float run_speed = 50f;
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.04f;
    public LayerMask groundMask;

    public static bool isGrounded;

    

    void Update()
    {
       

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run_speed;
        }
        else
        {
            speed = walk_speed;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            cc.Move(move * speed * Time.deltaTime);
        
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -5f);
        }

        velocity.y += gravity * Time.deltaTime * 2f;
        cc.Move(velocity * Time.deltaTime);

       
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        
    }

    
}
