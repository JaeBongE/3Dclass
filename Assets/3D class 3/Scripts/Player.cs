using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 vecDestination;
    private Vector3 startPosition;
    [SerializeField] float randomRadiusRange = 30f;
    [SerializeField] bool selected = false;

    private float waitingTime;//���� �Ŀ� ��� ��ٸ��� �ð�
    [SerializeField] Vector2 vecWaitngMinMax;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, randomRadiusRange, NavMesh.AllAreas))
        {
            startPosition = hit.position;
        }
        else
        {
            startPosition = transform.position;
        }
    }

    private void OnDestroy()
    {
        //1. �������� �÷����߿� �����е��� �˰��� ���ؼ� ����
        //UnitManager.Instance.RemoveUnit(this);

        //2. � ���ǿ� ���ؼ� �����Ͱ� �����Ǿ�� �Ҷ� ex) �����Ϳ��� �÷��̰� ������ ��
        if (UnitManager.Instance != null)//����ó��
        {
            UnitManager.Instance.RemoveUnit(this);
        }
    }

    private void Start()
    {
        //setNewPath();
        //setNewWaitTIme();
        if (selected == false) return;

        UnitManager.Instance.AddUnit(this);
    }


    void Update()
    {
        //if (isArrive() == true)//���ο� �̵� ��ġ�� ��ƾ� ��
        //{
        //    if (checkWaitTime() == true) return;

        //    setNewPath();
        //}
    }

    private void setNewWaitTIme()
    {
        waitingTime = Random.Range(vecWaitngMinMax.x, vecWaitngMinMax.y);
    }

    /// <summary>
    /// true�� �Ǹ� ��ٷ��� ��, false�� �Ǹ� �̵��ص� ��
    /// </summary>
    /// <returns></returns>
    private bool checkWaitTime()
    {
        if (waitingTime >= 0.0f)//��ٷ��� �ϴ� �ð��� 0.0�� �ƴ϶��
        {
            waitingTime -= Time.deltaTime;//�ð��� ���ҽ�Ų��
            if (waitingTime <= 0.0f)//���� ���ҽ�Ų �ð��� 0.0���ϰ� �ȴٸ�
            {
                setNewWaitTIme();//�� ��ٸ��� �ð��� ����
                return false;//�̵��϶�� ����
            }

            return true;//���߶�� ����
        }

        return false;//�̵��϶�� ����
    }

    private void setNewPath()
    {
        vecDestination = getRandomPoint();
        agent.SetDestination(vecDestination);
    }

    /// <summary>
    /// npc�� �����ߴ��� Ȯ���մϴ�.
    /// </summary>
    /// <returns></returns>
    private bool isArrive()
    {
        if (agent.velocity == Vector3.zero)//������ �ִ� ����, �̵��Ұ��� ��Ȳ�� ���ļ� ��������
        {
            return true;
        }

        //if (Vector3.Distance(vecDestination, transform.position) == 0.0f) ���� ����
        //{
        //    return true;
        //}

        return false;
    }

    /// <summary>
    /// ������Ʈ�� �̵� ������ ��ġ�� ������ üũ�ؼ� �����մϴ�.
    /// </summary>
    /// <returns></returns>
    private Vector3 getRandomPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * randomRadiusRange;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, randomRadiusRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return startPosition;
    }

    public void SetDestination(Vector3 _pos)
    {
        agent.SetDestination(_pos);
    }
}
