using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    [SerializeField] int testNumber;


    private void OnDestroy()//삭제 될때 불러지는 함수
    {
        CameraManager.Instance.RemoveAction(() =>
        {

        });
        
    }

    void Start()
    {
        CameraManager.Instance.Action = () => 
        {
            Debug.Log($"저는 테스트액션이고 저는 정수 {testNumber}를 가지고 있습니다.");
        };
    }

    void Update()
    {
        
    }
}
