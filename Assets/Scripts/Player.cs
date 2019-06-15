using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
public class Player : MonoBehaviour
{
    // blah blah this was made by captain dynamite fabulous

    CharacterController cc;

    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = 9.81f;
    public float jumpHeight = 15f;
    public LayerMask groundLayer;
    public float groundRayDistance = 1.1f;

    public Vector3 motion;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        motion.y -= gravity * Time.deltaTime;

        if (IsGrounded())
        {
            motion.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        

        bool inputJump = Input.GetButtonDown("Jump");
        if (IsGrounded() && inputJump) // If Jump button is pressed (Space)
        {
            Jump(); // Make character jump
        }

        Move(input.x, input.y);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, groundRayDistance, groundLayer);

        /*
        if (Physics.Raycast(transform.position, -transform.up, groundRayDistance))
        {
            return true; // Exits function with value true
        }
        return false; // Exits function with value true
        */
    }

    public void Move(float ih, float iv)
    {
        Vector3 direction = new Vector3(ih, 0, iv);

        if (Input.GetButton("Fire3") && iv > 0.5)
        {
            motion.x = direction.x * runSpeed;
            motion.z = direction.z * runSpeed;
        }
        else
        {
            motion.x = direction.x * walkSpeed;
            motion.z = direction.z * walkSpeed;
        }

        motion = transform.rotation * motion;

        cc.Move(motion * Time.deltaTime);
    }

    public void Jump()
    {
        motion.y = jumpHeight;
    }
}
