using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private List<Player> listPlayer = new List<Player>();

    public void AddUnit(Player _unit)
    {
        //if (listPlayer.Exists((x) => x == _unit) == false)//����Ʈ �ȿ� �ߺ����� ���� �Ǵ°��� ����
        //{
            listPlayer.Add(_unit);
        //}

    }

    public void RemoveUnit(Player _unit)
    {
        listPlayer.Remove(_unit);
    }

    public void MovePosition(Vector3 _pos)
    {
        int count = listPlayer.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Player unit = listPlayer[iNum];
            if (unit.Select == true)
            {
                unit.SetDestination(_pos);
            }
        }

        //Ȥ��
        //foreach (Player unit in listPlayer)
        //{
        //    unit.SetDestination(_pos);
        //}
    }

    public void ClearAllSelectUnit()
    {
        int count = listPlayer.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Player sc = listPlayer[iNum];
            sc.Select = false;
        }
    }
    
    public void SelectUnit(Rect _rect)
    {
        int count = listPlayer.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Player sc = listPlayer[iNum];
            if (_rect.Contains(Camera.main.WorldToScreenPoint(sc.transform.position)) == true)//��Ʈ �ȿ� ���ִ��� ���
            {
                sc.Select = true;
            }
        }
    }
}
