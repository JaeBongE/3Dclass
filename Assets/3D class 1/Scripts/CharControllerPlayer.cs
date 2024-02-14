using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;
    private Camera camMain;

    private float verticalVelocity = 0f;
    private float gravity = 9.81f;
    private bool isSlope = false;
    private Vector3 slopeVelocity;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;

    [SerializeField] private GameObject cam3rd;
    [SerializeField] private GameObject cam1st;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        checkMouseLock();
        rotation();

        checkGround();
        moving();
        jump();
        checkGravitiy();
        checkSlope();

        checkDetail();
    }

    private void checkMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            switch(Cursor.lockState)
            {
                case CursorLockMode.Locked: Cursor.lockState = CursorLockMode.None; break;
                case CursorLockMode.None: Cursor.lockState = CursorLockMode.Locked; break;
            }
        }
    }

    private void rotation()
    {
        transform.rotation = Quaternion.Euler(0f, camMain.transform.eulerAngles.y, 0f);
    }

    private void checkGround()
    {
        isGround = false;
        if (verticalVelocity < 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        //isGround = characterController.isGrounded; - 만약 잘 들어오면 사용가능
    }

    private void moving()
    {
        moveDir = new Vector3(inputHorizontal(), 0f, inputVertical());

        if (isSlope == true)
        {
            characterController.Move(-slopeVelocity * Time.deltaTime);
        }
        else
        {
            characterController.Move(transform.rotation * moveDir * Time.deltaTime);
        }

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
        if (isGround == false || isSlope == true) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravitiy()
    {
        if (isGround == true)
        {
            verticalVelocity = 0f;
        }

        if (isJump == true)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void checkSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, characterController.height, LayerMask.GetMask("Ground"))) ;
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle >= characterController.slopeLimit)
            {
                isSlope = true;
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0f, gravity, 0f), hit.normal);
            }
            else
            {
                isSlope = false;
            }
        }
    }

    private void checkDetail()
    {
        if (Input.GetMouseButton(1) && cam1st.activeSelf == false)// 0 왼쪽 1 오른쪽 0 휠
        {
            cam1st.SetActive(true);
            cam3rd.SetActive(false);
        }
        else if (Input.GetMouseButton(1) == false && cam3rd.activeSelf == false)//누르지 않고 있을 때
        {
            cam1st.SetActive(false);
            cam3rd.SetActive(true);
        }
    }
}
