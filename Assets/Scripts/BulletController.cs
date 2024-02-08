using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 direction;
    private float force;
    Rigidbody rigid;
    private bool isGrenade = false;

    private void OnCollisionEnter(Collision collision) // 리지드 바디가 없으면 동작 안함
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        if (isGrenade == true) return;

        transform.position = Vector3.MoveTowards(transform.position, direction, force * Time.deltaTime);//어디로 갈 지 정하는 함수
        checkDestination();
    }

    private void checkDestination()
    {
        if (Vector3.Distance(transform.position, direction) == 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 _direction, float _force)
    {
        rigid.useGravity = false;
        //DestroyImmediate(gameObject); 바로 지우는 코드, 코드가 꼬일 가능성 있음
        direction = _direction;
        force = _force;
    }

    public void AddForce(float _force)
    {
        isGrenade = true;
        rigid.useGravity = true;
        rigid.AddForce(transform.rotation * Vector3.forward * _force, ForceMode.Impulse);
    }
}
