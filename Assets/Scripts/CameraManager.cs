using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cams//미리 정의되어야만 하는 데이터
{
    MainCam,
    SubCam1,
    SubCam2,
    SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    //private List<Camera> listCam = new List<Camera>();//직렬화를 하거나 공개, 인스펙터
    [SerializeField] List<Camera> listCam;
    [SerializeField] List<Button> listBtns;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }



    void Start()
    {
        //Camera[] arrCams = FindObjectsOfType<Camera>();Gameobject.Find와 비슷
        //listCam.AddRange(arrCams); - SerializeField화 하는 편이 오류가 없다

        //int enumCount = System.Enum.GetValues(typeof(Cams)).Length;

        //int intEnum = (int)Cams.MainCam;
        //Cams enumData = (Cams)intEnum;

        //string stringEnum = Cams.MainCam.ToString();
        //enumData = (Cams)System.Enum.Parse(typeof(Cams), stringEnum);

        switchCamera(Cams.MainCam);
        initBitns();
    }

    private void initBitns()
    {
        int count = listBtns.Count;
        for (int iNum = 0; iNum < count; iNum ++)//람다식이 for문을 만났을 때 조건이되는 변수가 계속 변하는데 그 변하는 데이터의 주소를 계속 전달하기 때문에 문제를 야기함
        {
            int num = iNum;
            listBtns[iNum].onClick.AddListener(() => switchCamera(num));
        }
        
        //listBtns[0].onClick.AddListener(()=> switchCamera(0));
        //listBtns[1].onClick.AddListener(()=> switchCamera(1));
        //listBtns[2].onClick.AddListener(()=> switchCamera(2));
        //listBtns[3].onClick.AddListener(()=> switchCamera(3));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { 
            switchCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { 
            switchCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { 
            switchCamera(3);
        }
    }

    /// <summary>
    /// 기능 : 매개변수로 전달받은 카메라는 켜주고, 나머지 카메라는 꺼줍니다
    /// </summary>
    /// <param name="_value"> 켜줄 카메라</param>
    private void switchCamera(Cams _value)
    {
        int count = listCam.Count;
        int findNum = (int)_value;
        for (int iNum = 0; iNum < count; iNum++)
        {
            //listCam[iNum]
            Camera cam = listCam[iNum];
            cam.enabled = iNum == findNum;//if문으로 돌리는 것과 같은 결과
            //if (iNum == findNum)
            //{
            //    cam.enabled = true;
            //}
            //else
            //{
            //    cam.enabled = false;
            //}
        }
    }
    
    private void switchCamera(int _value)
    {
        int count = listCam.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            //listCam[iNum]
            Camera cam = listCam[iNum];
            cam.enabled = iNum == _value;
        }
    }
}
