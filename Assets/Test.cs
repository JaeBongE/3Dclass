using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //mouseFunction();

        //transform.position += transform.forward * Time.deltaTime;//�������� �̵�

        //transform.position += Rotation * transforom.position;
        //transform.position += transform.rotation * Vector3.forward * Time.deltaTime;//�������� �̵�, ������ �߿� ���͸� ���� �� �� ����
        //transform.position += transform.TransformDirection(Vector3.forward);
    }

    private void mouseFunction()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)//���콺�� ���̰� ���� ������ ����
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
