using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cams//�̸� ���ǵǾ�߸� �ϴ� ������
{
    MainCam,
    SubCam1,
    SubCam2,
    SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    //private List<Camera> listCam = new List<Camera>();//����ȭ�� �ϰų� ����, �ν�����
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
        //Camera[] arrCams = FindObjectsOfType<Camera>();Gameobject.Find�� ���
        //listCam.AddRange(arrCams); - SerializeFieldȭ �ϴ� ���� ������ ����

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
        for (int iNum = 0; iNum < count; iNum ++)//���ٽ��� for���� ������ �� �����̵Ǵ� ������ ��� ���ϴµ� �� ���ϴ� �������� �ּҸ� ��� �����ϱ� ������ ������ �߱���
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
    /// ��� : �Ű������� ���޹��� ī�޶�� ���ְ�, ������ ī�޶�� ���ݴϴ�
    /// </summary>
    /// <param name="_value"> ���� ī�޶�</param>
    private void switchCamera(Cams _value)
    {
        int count = listCam.Count;
        int findNum = (int)_value;
        for (int iNum = 0; iNum < count; iNum++)
        {
            //listCam[iNum]
            Camera cam = listCam[iNum];
            cam.enabled = iNum == findNum;//if������ ������ �Ͱ� ���� ���
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
