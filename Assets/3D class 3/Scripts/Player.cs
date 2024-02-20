using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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

    [Header("����������")]
    OffMeshLinkData linkData;
    [SerializeField] float JumpSpeed = 0.0f;
    private float JumpRatio = 0.0f;
    private float JumpMaxHeight = 0.0f;
    [SerializeField] float JumpHeight = 5f;
    private bool setOffMesh = false;
    private Vector3 offMeshStart;
    private Vector3 offMeshEnd;

    Material matUnit;//Ŭ���Ǿ����� Ȯ�ο� ���׸���

    private bool select = false;
    public bool Select
    {
        set
        {
            select = value;
            if (matUnit != null)
            {
                if (select == true)
                {
                    matUnit.color = Color.green;
                }
                else
                {
                    matUnit.color = Color.white;
                }
            }
        }
        get
        {
            return select;
        }
    }

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

        //NavMesh.RemoveAllNavMeshData();//�׺�Ž��� ����
        //NavMeshSurface surface = GetComponent<NavMeshSurface>();
        //surface.BuildNavMesh();

        MeshRenderer mr = GetComponent<MeshRenderer>();
        matUnit = Instantiate(mr.material);
        mr.material = matUnit;
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

        if (agent.isOnOffMeshLink == true)
        {
            doOffMesh();
        }
    }

    private void doOffMesh()
    {
        if (setOffMesh == false)//�����ϱ��� ����
        {
            setOffMesh = true;
            linkData = agent.currentOffMeshLinkData;

            offMeshStart = transform.position;
            offMeshEnd = linkData.endPos + new Vector3(0, agent.height * 0.5f, 0);

            agent.isStopped = true;//������Ʈ ����
            JumpSpeed = Vector3.Distance(offMeshStart, offMeshEnd) / agent.speed;
            //float distance = (offMeshStart - offMeshEnd).magnitude; vector3.distance�� ������ ����� ��
            JumpMaxHeight = (offMeshEnd - offMeshStart).y + JumpHeight;
        }

        JumpRatio += (Time.deltaTime / JumpSpeed);

        Vector3 movePos = Vector3.Lerp(offMeshStart, offMeshEnd, JumpRatio);
        movePos.y = offMeshStart.y + JumpMaxHeight * JumpRatio + -JumpHeight * Mathf.Pow(JumpRatio, 2);
        transform.position = movePos;

        if (JumpRatio >= 1.0f)//������ ��
        {
            JumpRatio = 0.0f;
            agent.CompleteOffMeshLink();
            agent.isStopped = false;//�ٽ� ����
            setOffMesh = false;
        }
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
