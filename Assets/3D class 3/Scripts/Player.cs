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

    private float waitingTime;//도착 후에 잠깐 기다리는 시간
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
        //1. 동적으로 플레이중에 여러분들의 알고리즘에 의해서 삭제
        //UnitManager.Instance.RemoveUnit(this);

        //2. 어떤 조건에 의해서 데이터가 삭제되어야 할때 ex) 에디터에서 플레이가 끝났을 때
        if (UnitManager.Instance != null)//예외처리
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
        //if (isArrive() == true)//새로운 이동 위치를 잡아야 함
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
    /// true가 되면 기다려야 함, false가 되면 이동해도 됨
    /// </summary>
    /// <returns></returns>
    private bool checkWaitTime()
    {
        if (waitingTime >= 0.0f)//기다려야 하는 시간이 0.0이 아니라면
        {
            waitingTime -= Time.deltaTime;//시간을 감소시킨다
            if (waitingTime <= 0.0f)//만약 감소시킨 시간이 0.0이하가 된다면
            {
                setNewWaitTIme();//새 기다리는 시간을 정의
                return false;//이동하라고 전달
            }

            return true;//멈추라고 전달
        }

        return false;//이동하라고 전달
    }

    private void setNewPath()
    {
        vecDestination = getRandomPoint();
        agent.SetDestination(vecDestination);
    }

    /// <summary>
    /// npc가 도착했는지 확인합니다.
    /// </summary>
    /// <returns></returns>
    private bool isArrive()
    {
        if (agent.velocity == Vector3.zero)//가만히 있는 상태, 이동불가능 상황에 닥쳐서 멈춰있음
        {
            return true;
        }

        //if (Vector3.Distance(vecDestination, transform.position) == 0.0f) 위와 동일
        //{
        //    return true;
        //}

        return false;
    }

    /// <summary>
    /// 에이전트가 이동 가능한 위치를 스스로 체크해서 전달합니다.
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
