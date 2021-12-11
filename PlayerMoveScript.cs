using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float jumpHight = 3f;
    public bool isDoubleJumping = false;

    public Transform GroundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public float gravity = -9.81f;
    public bool isGrounded;

    bool isSprinting = false;

    bool isCrouching = false;

    bool isJumping = false;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDis, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isCrouching)
        {
            speed = 3f;
        }

        if (!(isCrouching))
        {
            speed = 6f;
        }

        if (isSprinting)
        {
            speed = 12f;
        }

        if (!(isSprinting))
        {
            speed = 6f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if (!(Input.GetKey(KeyCode.LeftShift)))
        {
            isSprinting = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }

        if (!(Input.GetKey(KeyCode.LeftControl)))
        {
            isCrouching = false;
            isJumping = true;
        }

        float HorizontalXinput = Input.GetAxis("Horizontal");
        float VerticalZinput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * HorizontalXinput + transform.forward * VerticalZinput;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
