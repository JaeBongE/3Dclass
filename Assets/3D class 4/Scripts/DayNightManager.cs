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

    [SerializeField, Range(0f, 24f)] float dayTime = 14f;
    [SerializeField, Range(0f, 24f)] float nightTime = 23f;

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
                    listLights.Add(light.transform.parent.GetComponent<FunctionLight>());
                }
            }

            count = listLights.Count;
            for (int iNum = 0; iNum < count; iNum++)
            {
                FunctionLight light = listLights[iNum];
                light.init(isNight);
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

            if (timeOfday > dayTime)
            {
                isNight = true;
            }
            else if(timeOfday < nightTime)
            {
                isNight = false;
            }
        }
        else//�ð��� ���� ����
        {
            if (isNight == true)
            {
                timeOfday += Time.deltaTime;
                if (timeOfday >= nightTime)
                {
                    timeOfday = nightTime;//��
                }
            }
            else
            {
                timeOfday += Time.deltaTime;
                if (timeOfday < nightTime && timeOfday >= dayTime)
                {
                    timeOfday = dayTime;//��
                }
            }
        }

        if (timeOfday > 23.1f)
        {
            timeOfday = 4f;
        }

        updateLighting();
    }

    private void updateLighting()
    {
        if (directionalLight == null)
        {
            Debug.Log("���ӿ� �𷺼ų� ����Ʈ�� �������� �ʽ��ϴ�.");
            return;
        }

        float timePercent = timeOfday / 24f;//0 ~ 1
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(timePercent * 360f - 90f, 150f, 0));

        if (directionalLight.transform.eulerAngles.x >= 180f && directionalLight.color.r > 0.0f)//night
        {
            directionalLight.color -= new Color(1, 1, 1) * Time.deltaTime * 10.0f;
            if (directionalLight.color.r < 0.0f)
            {
                directionalLight.color = new Color(0, 0, 0);

                foreach (FunctionLight light in listLights)
                {
                    light.TurnOnLight(true);
                }
            }
        }
        else if (directionalLight.transform.localRotation.x >= 0.0f && directionalLight.color.r < 1.0f)//day
        {
            directionalLight.color += new Color(1, 1, 1) * Time.deltaTime * 10.0f;

            if (directionalLight.color.r > 1.0f)
            {
                directionalLight.color = new Color(1, 1, 1);

                foreach (FunctionLight light in listLights)
                {
                    light.TurnOnLight(false);
                }
            }

        }
    }
}
