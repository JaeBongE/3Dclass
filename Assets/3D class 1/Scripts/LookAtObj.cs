using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObj : MonoBehaviour
{
    [SerializeField] Transform trsLookAt;


    void Update()
    {
        if (trsLookAt == null) return;
        transform.LookAt(trsLookAt);
        //transform.LookAt(trsLookAt.position);
    }
}
