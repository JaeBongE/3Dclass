using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform trsLookAt;
    [SerializeField, Range(0.0f, 1.0f)] float lookAtWeight;

    List<string> listDanceStateName = new List<string>();

    [SerializeField] GameObject objInven;
    [SerializeField] GameObject objButton;

    Dictionary<string, string> dicNameValue = new Dictionary<string, string>();

    [SerializeField, Range(0.0f, 1.0f)] float distanceToGround;

    private bool doChangeState = false;
    private float mouseVertical = 0f;

    private void OnAnimatorIK(int layerIndex)
    {
        if (trsLookAt != null)
        {
            anim.SetLookAtWeight(lookAtWeight);
            anim.SetLookAtPosition(trsLookAt.position);
        }

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);


        if (Physics.Raycast(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down, out RaycastHit leftHit, distanceToGround + 1.0f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = leftHit.point;
            footPos.y += distanceToGround;

            anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);

            anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, leftHit.normal), leftHit.normal));
        }

        if (Physics.Raycast(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down, out RaycastHit rightHit, distanceToGround + 1.0f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = rightHit.point;
            footPos.y += distanceToGround;

            anim.SetIKPosition(AvatarIKGoal.RightFoot, footPos);

            anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, rightHit.normal), rightHit.normal));
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();

        dicNameValue.Add("Dance_1", "SomeThing");
        dicNameValue.Add("Dance_2", "SomeThing22");
        dicNameValue.Add("Dance_3", "SomeThing2233");
    }

    void Start()
    {
        initDance();
        createDanceUi();
    }

    void Update()
    {
        moving();
        doDance();
        activeDanceInventory();
        checkAim();
    }

    private void moving()
    {
        anim.SetFloat("SpeedVertical", Input.GetAxis("Vertical"));
        anim.SetFloat("SpeedHorizontal", Input.GetAxis("Horizontal"));

        //anim.SetBool("Front", Input.GetKey(KeyCode.W)) ;
        //anim.SetBool("Back", Input.GetKey(KeyCode.S)) ;
        //anim.SetBool("Left", Input.GetKey(KeyCode.A)) ;
        //anim.SetBool("Right", Input.GetKey(KeyCode.D)) ;
    }

    private void activeDanceInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = objInven.activeSelf;
            objInven.gameObject.SetActive(!isActive);//! - 앞의 값을 반대로 취한다.
        }
    }

    private void initDance()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;//Dance_
        int count = clips.Length;
        for (int iNum = 0; iNum < count; iNum++)
        {
            string animName = clips[iNum].name;
            if (animName.Contains("Dance_"))// 어떤 문자열을 포함하는지 확인하는 함수
            {
                listDanceStateName.Add(animName);
            }
        }
    }

    private void createDanceUi()
    {
        Transform parents = objInven.transform;
        int count = listDanceStateName.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            int Number = iNum;
            //이름, 기능
            GameObject obj = Instantiate(objButton, parents);

            TMP_Text objText = obj.GetComponentInChildren<TMP_Text>();
            string curName = listDanceStateName[Number];
            objText.text = dicNameValue[curName];


            Button objBtn = obj.GetComponent<Button>();
            objBtn.onClick.AddListener(() =>
            {
                anim.CrossFade(listDanceStateName[Number], 0.1f);
            });
        }
    }

    private void doDance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //anim.Play("Dance1"); 애니메이션을 바로 바꿔주는 코드 ex) 캐릭터 사망
            anim.CrossFade("Dance_1", 0.2f);//절대값 아님, 0 ~ 1 애니메이션이 점점 바뀌게 만드는 코드
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //anim.Play("Dance2");
            anim.CrossFade("Dance_2", 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //anim.Play("Dance3");
            anim.CrossFade("Dance_3", 0.2f);
        }

        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            anim.Play("Move");
        }
    }

    private void checkAim()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && doChangeState == false)
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;//마우스 커서가 보이지 않아야 함
                //layer weight를 1로
                StartCoroutine(changeState(true));
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                //layer weight를 0으로
                StartCoroutine(changeState(false));
            }
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseVertical += Input.GetAxis("Mouse Y") * Time.deltaTime;
            mouseVertical = Mathf.Clamp(mouseVertical, -1f, 1f);
            anim.SetFloat("MouseVertical", mouseVertical);

            if (Input.GetMouseButtonDown(0))
            {
                //anim.Play("원하는 애니메이션 이름");
            }
        }

    }

    //private IEnumerator corFunction()
    //{
    //    //1
    //    //2
    //    yield return null;//다른 코루틴이 있으면 그 친구가 실행되도록 양보, 이 차례에서 대기 하고 다른 코루틴에게 차례를 넘겨줌
    //    //3
    //    //4

    //}
    
    //private IEnumerator corFunction2()
    //{
    //    //1
    //    //2
    //    yield return null;
    //    //3
    //    //4
    //}

    IEnumerator changeState(bool _upper)
    {
        //yield return new WaitForSeconds(1.0f);//1초를 기다림

        //yield return new WaitForEndOfFrame();//1frm을 넘겨줌

        //while(true)
        //{
        //    yield return null;
        //}

        float ratio = 0f;
        doChangeState = true;

        if (_upper)
        {
            while(anim.GetLayerWeight(1) < 1.0f)
            {
                ratio += Time.deltaTime * 5f;
                anim.SetLayerWeight(1, Mathf.Lerp(0f, 1f, ratio));//0에서 1까지 ratio로 올라감
                yield return null;
            }
        }
        else
        {
            while(anim.GetLayerWeight(1) > 0f)
            {
                ratio += Time.deltaTime * 5f;
                anim.SetLayerWeight(1, Mathf.Lerp(1f, 0f, ratio));
                yield return null;
            }
        }

        doChangeState = false;
    }
}
