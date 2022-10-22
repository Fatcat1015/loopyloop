using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFPS : MonoBehaviour
{
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

    public bool isGrounded;
    public GameObject interacting_object = null;
    public LayerMask Interactable_Layer;

    public Material outline;
    public Material Default_noOutline;

    public Ray ray;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, Interactable_Layer))
        {
            interacting_object = hit.transform.gameObject;
            if(interacting_object.GetComponent<Renderer>().sharedMaterial != outline) Default_noOutline = interacting_object.GetComponent<Renderer>().sharedMaterial;
            interacting_object.GetComponent<Renderer>().sharedMaterial = outline;
        }
        
        else if (interacting_object != null)
        {
                    interacting_object.GetComponent<Renderer>().sharedMaterial = Default_noOutline;
                    Default_noOutline = null;
                    interacting_object = null;
        }
       

        

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
            velocity.y = -0.5f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -1);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * gravity * -0.001f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        Gizmos.DrawRay(ray);
    }
}
