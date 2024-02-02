using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;

    private float verticalVelocity = 0f;
    private float gravity = 9.81f;
    private bool isSlope = false;
    private Vector3 slopeVelocity;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;
    

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        checkGround();
        moving();
        jump();
        checkGravitiy();
    }

    private void checkGround()
    {
        isGround = false;
        if (verticalVelocity <= 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        //isGround = characterController.isGrounded; - 만약 잘 들어오면 사용가능
    }

    private void moving()
    {
        moveDir = new Vector3(inputHorizontal(), 0f, inputVertical());

        characterController.Move(transform.rotation * moveDir * Time.deltaTime);

    }

    private float inputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }
    private float inputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void jump()
    {
        if (isGround == false) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravitiy()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if (isGround == true)
        {
            verticalVelocity = 0f;
        }

        if (isJump == true)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }
}
