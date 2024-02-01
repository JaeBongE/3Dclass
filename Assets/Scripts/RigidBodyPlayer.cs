using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    private float gravity = 9.81f;
    private float verticalVelocity = 0f;//�������� �޴� ��
    [SerializeField] private bool isGround = false;
    private bool isJump = false;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [Header("�÷��̾� ����")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField,Tooltip("���콺�� ����")] float mouseSensitivity = 5f;
    private Vector2 rotateValue;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        checkGround();
        moving();
        jumping();
        checkGravity();
    }

    private void checkGround()
    {
        if (rigid.velocity.y < 0)//verticalVelocity < 0 �� ������ ���
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cap.height * 0.5f, LayerMask.GetMask("Ground"));
        }
    }

    private void moving()
    {
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = transform.rotation * moveDir * moveSpeed;
    }

    //private float inputHorizontal()
    //{
    //    float value = Input.GetAxisRaw("Horizontal");
    //    return value;
    //}

    private void jumping()
    {
        if (isGround == false) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }


    private void checkGravity()
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

        rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
    }

}
