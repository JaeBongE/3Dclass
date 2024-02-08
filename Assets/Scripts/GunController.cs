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
    /// 총기가 카메라 한 가운데 보이는 오브젝트를 노리도록 만들어줌
    /// </summary>
    private void gunPointer()
    {
        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);
        }
        else // 그라운드 오브젝트에 레이캐스트가 닿지 않았을 때
        {
            Vector3 lookPos = camMain.transform.position + camMain.transform.forward * distance;//카메라가 앞으로 가는 거리 * 정해진 거리
            
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
        
        if (isGrenade == true)//유탄이라면
        {
            sc.AddForce(gunForce * 0.5f);
        }
        else if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
        {
            sc.SetDestination(hit.point, gunForce);
        }
        else // 그라운드 오브젝트에 레이캐스트가 닿지 않았을 때
        {
            Vector3 lookPos = camMain.transform.position + camMain.transform.forward * 1000.0f;//카메라가 앞으로 가는 거리 * 정해진 거리

            sc.SetDestination(lookPos, gunForce);
        }
    }
}
