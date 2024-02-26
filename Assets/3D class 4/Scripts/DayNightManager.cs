using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    private Light directionalLight;
    [SerializeField, Range(0f, 24f)] private float timeOfday;

    public bool isNight = false;
    private string keyIsNight = "isNight";
    public bool AutoChange = false;//자동 낮밤 변경

    List<FunctionLight> listLights = new List<FunctionLight>();

    private void OnApplicationQuit()
    {
        string value = isNight == true ? "t" : "f";
        PlayerPrefs.SetString(keyIsNight, value);
    }

    private void Awake()
    {
        string value = PlayerPrefs.GetString(keyIsNight, "f");
        isNight = value == "t" ? true : false;

        if (directionalLight == null)
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            int count = lights.Length;
            for (int iNum = 0; iNum < count; iNum++)
            {
                Light light = lights[iNum];
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                }
                else
                {
                    listLights.Add(light.transform.GetComponent<FunctionLight>());
                }
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        timeOfday %= 24;//0 ~ 24 사이의 값만 저장됨(나머지만 저장)
        if (AutoChange == true)
        {
            timeOfday += Time.deltaTime;//시간이 자동으로 증가
        }
        else//시간을 직접 관리
        {
            if (isNight == true)
            {
                timeOfday = 19;//밤
            }
            else
            {
                timeOfday = 14;//낮
            }
        }
    }
}
