using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionLight : MonoBehaviour
{
    public enum TypeLight
    {
        Disable,//ºñ»ç¿ë
        Always,//Ç×»ó
        OnlyNight,//¹ã¿¡¸¸
        OnlyDays,//³·¿¡¸¸
    }

    public TypeLight typeLight;
    Material matWindow;
    GameObject objLight;

    public void init(bool _isNight)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        matWindow = Instantiate(mr.material);
        mr.material = matWindow;

        objLight = GetComponentInChildren<Light>().gameObject;
        //objLight = transform.GetChild(0).gameObject;
        //objLight = transform.Find("Ponit Light").gameObject;

        //matWindow.EnableKeyword("_EMISSION");//ÄÓ¶§

        if ((_isNight == true && typeLight == TypeLight.OnlyNight) || (_isNight == false && typeLight == TypeLight.OnlyDays) || (typeLight == TypeLight.Always))
        {
            TurnOnLight(true);
        }
        else
        {
            TurnOnLight(false);
        }
        //else if (_isNight == false && typeLight == TypeLight.OnlyDays)
        //{
        //    matWindow.EnableKeyword("_EMISSION");
        //    objLight.SetActive(true);
        //}
        //else if (typeLight == TypeLight.Always)
        //{
        //    matWindow.EnableKeyword("_EMISSION");
        //    objLight.SetActive(true);
        //}

        //mr.material.DisableKeyword("_EMISSION");
    }

    public void TurnOnLight(bool _value)
    {
        if (_value == true)
        {
            matWindow.EnableKeyword("_EMISSION");//Å´
            objLight.SetActive(true);
        }
        else
        {
            matWindow.DisableKeyword("_EMISSION");
            objLight.SetActive(false);
        }
    }
}
