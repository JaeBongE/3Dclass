using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    [SerializeField] int testNumber;


    private void OnDestroy()//���� �ɶ� �ҷ����� �Լ�
    {
        CameraManager.Instance.RemoveAction(() =>
        {

        });
        
    }

    void Start()
    {
        CameraManager.Instance.Action = () => 
        {
            Debug.Log($"���� �׽�Ʈ�׼��̰� ���� ���� {testNumber}�� ������ �ֽ��ϴ�.");
        };
    }

    void Update()
    {
        
    }
}
