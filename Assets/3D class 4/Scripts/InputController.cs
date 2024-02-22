using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform trsLookAt;
    [SerializeField, Range(0.0f, 1.0f)] float lookAtWeight;

    List<string> listDanceStateName = new List<string>();

    private void OnAnimatorIK(int layerIndex)
    {
        if (trsLookAt != null)
        {
            anim.SetLookAtWeight(lookAtWeight);
            anim.SetLookAtPosition(trsLookAt.position);
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
    }

    void Update()
    {
        moving();
        doDance();
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

    private void doDance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //anim.Play("Dance1"); 애니메이션을 바로 바꿔주는 코드 ex) 캐릭터 사망
            anim.CrossFade("Dance1", 0.2f);//절대값 아님, 0 ~ 1 애니메이션이 점점 바뀌게 만드는 코드
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //anim.Play("Dance2");
            anim.CrossFade("Dance2", 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //anim.Play("Dance3");
            anim.CrossFade("Dance3", 0.2f);
        }

        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            anim.Play("Move");
        }
    }
}
