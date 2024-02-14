using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject objBullet;
    [SerializeField] Transform trsMuzzle;
    [SerializeField] Transform trsDynamic;
    private Camera camMain;
    private float distance = 250f;
    [SerializeField] private float gunForce = 100f;
    [Space]
    [SerializeField] private bool isGrenade;

    private void Start()
    {
        camMain = Camera.main;
    }


    void Update()
    {
        gunPointer();
        checkFire();
        checkGrenade();
    }

    /// <summary>
    /// �ѱⰡ ī�޶� �� ��� ���̴� ������Ʈ�� �븮���� �������
    /// </summary>
    private void gunPointer()
    {
        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);
        }
        else // �׶��� ������Ʈ�� ����ĳ��Ʈ�� ���� �ʾ��� ��
        {
            Vector3 lookPos = camMain.transform.position + camMain.transform.forward * distance;//ī�޶� ������ ���� �Ÿ� * ������ �Ÿ�
            
            transform.LookAt(lookPos);
        }
    }

    private void checkFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }
    }

    private void checkGrenade()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = true;
        }
    }

    private void shootBullet()
    {
        GameObject go = Instantiate(objBullet, trsMuzzle.position, trsMuzzle.rotation, trsDynamic);
        BulletController sc = go.GetComponent<BulletController>();
        
        if (isGrenade == true)//��ź�̶��
        {
            sc.AddForce(gunForce * 0.5f);
        }
        else if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
        {
            sc.SetDestination(hit.point, gunForce);
        }
        else // �׶��� ������Ʈ�� ����ĳ��Ʈ�� ���� �ʾ��� ��
        {
            Vector3 lookPos = camMain.transform.position + camMain.transform.forward * 1000.0f;//ī�޶� ������ ���� �Ÿ� * ������ �Ÿ�

            sc.SetDestination(lookPos, gunForce);
        }
    }
}
