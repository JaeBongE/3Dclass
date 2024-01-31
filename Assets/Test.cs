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

        //transform.position += transform.forward * Time.deltaTime;//전방으로 이동

        //transform.position += Rotation * transforom.position;
        //transform.position += transform.rotation * Vector3.forward * Time.deltaTime;//전방으로 이동, 순서가 중요 벡터를 먼저 쓸 수 없음
        //transform.position += transform.TransformDirection(Vector3.forward);
    }

    private void mouseFunction()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)//마우스가 보이고 조절 가능한 상태
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
