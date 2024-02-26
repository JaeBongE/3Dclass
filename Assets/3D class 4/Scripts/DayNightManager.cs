using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    private Light directionalLight;
    [SerializeField, Range(0f, 24f)] private float timeOfday;

    public bool isNight = false;
    private string keyIsNight = "isNight";
    public bool AutoChange = false;//�ڵ� ���� ����

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
        timeOfday %= 24;//0 ~ 24 ������ ���� �����(�������� ����)
        if (AutoChange == true)
        {
            timeOfday += Time.deltaTime;//�ð��� �ڵ����� ����
        }
        else//�ð��� ���� ����
        {
            if (isNight == true)
            {
                timeOfday = 19;//��
            }
            else
            {
                timeOfday = 14;//��
            }
        }
    }
}
