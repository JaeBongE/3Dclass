using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float mouseMoveSpeed = 5f;
    private Vector3 rotateValue;

    void Start()
    {
        rotateValue = transform.rotation.eulerAngles;//���ʹϾ��� vector3�� ����ȯ

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void checkMouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;//���콺�� ������ �ʵ��� �׻� ���콺�� ȭ���� ��� ����
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        
    }

    void Update()
    {
        //Debug.Log((int)(1 / Time.deltaTime));
        moving();//�̵����
        rotating();//ȸ�����
        
        checkMouse();
    }

    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //����� ��ǥ ����
            //transform.position += transform.forward * mouseMoveSpeed * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
            transform.position += transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;

            //�۷ι� ����
            //transform.position += Vector3.forward * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += -transform.forward * mouseMoveSpeed * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
            transform.position += transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += -transform.right * mouseMoveSpeed * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
            transform.position += transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += transform.right * mouseMoveSpeed * Time.deltaTime;
            //transform.position += transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
            transform.position += transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += Vector3.down * mouseMoveSpeed * Time.deltaTime;
        }
    }

    private void rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);

        //if (rotateValue.x > 90) -90 ~ 90
        //{
        //    rotateValue.x = 90;
        //}
        //else if (rotateValue.x < -90)
        //{
        //    rotateValue.x = -90;
        //}

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);//Mathf.Clamp - � ���� �ִ� �ּ� ���� �����ϴ� �Լ�, ������ ������ �� �ִ�.

        transform.rotation = Quaternion.Euler(rotateValue);

        //������Ʈ�� Ȱ�� / ��Ȱ��
        //gameObject.SetActive(false);
        //gameObject.SetActive(true);

        //������Ʈ ���� ������Ʈ Ȱ�� / ��Ȱ��
        //BoxCollider box = GetComponent<BoxCollider>();
        //box.enabled = false;
        //box.enabled = true;
    }

    //private bool checkFrame(int _limitFrame)
    //{
    //    int curFrame = (int)(1 / Time.deltaTime);
    //    return _limitFrame < curFrame;


    //    //if (_limitFrame <curFrame)
    //    //{
    //    //    return true;
    //    //}
    //    //else
    //    //{
    //    //    return false;
    //    //}
    //}
}