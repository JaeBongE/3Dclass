using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform trsPlayer;
    Bounds bounds;

    void Start()
    {
        cam = Camera.main;
        checkBound();
    }

    private void checkBound()
    {
        float height = cam.orthographicSize;
        //aspect = width / height
        float width = height * cam.aspect;

        bounds = boxCollider.bounds;

        float minX = bounds.min.x + width;
        float maxX = bounds.extents.x - width;

        float minY = bounds.min.y + height;
        float maxY = bounds.extents.y - height;

        bounds.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));
    }

    void Update()
    {
        if (trsPlayer == null) return;
        
        Vector3 curPos = cam.transform.position;
        curPos.x = trsPlayer.position.x;
        curPos.y = trsPlayer.position.y;


        cam.transform.position = new Vector3
            (
            Mathf.Clamp(curPos.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(curPos.y, bounds.min.y, bounds.max.y),
            curPos.z
            );
    }
}
