using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] GameObject fabBall;//������
    [SerializeField] GameObject objCursor;//���콺 Ŀ��
    [SerializeField] Transform shootPoint;//�߻� ��ġ
    [SerializeField] LayerMask layer;//Ÿ���� ���̾�

    [SerializeField] Transform trsCannon;//ĳ��
    [SerializeField] float distanceCannon;

    Camera cam;

    [SerializeField] float time;
    [SerializeField, Range(0f, 1f)] float ratio;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        checkCannon();
    }

    private void checkCannon()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);//ī�޶�� ���� ���콺 �����ͷ� �� ��ġ Ȯ��
        if (Physics.Raycast(ray, out RaycastHit hit, 15f, layer))
        {
            if (Vector3.Distance(hit.point, trsCannon.position) < distanceCannon)
            {
                objCursor.SetActive(false);
                return;
            }

            objCursor.SetActive(true);
            objCursor.transform.position = hit.point + Vector3.up * 0.001f;//*0.001f �� z������ ������

            Vector3 vo = calculateVelocity(hit.point);
            trsCannon.transform.rotation = Quaternion.LookRotation(vo);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject objBall = Instantiate(fabBall, shootPoint.position, Quaternion.identity);
                Rigidbody rigid = objBall.GetComponent<Rigidbody>();
                rigid.velocity = vo;
            }
        }
        else
        {
            objCursor.SetActive(false);
        }
    }

    private Vector3 calculateVelocity(Vector3 _target)//������ ����
    {
        Vector3 distance = _target - trsCannon.position;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + ratio * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
}
