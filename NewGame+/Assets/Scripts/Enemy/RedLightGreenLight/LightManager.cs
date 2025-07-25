using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    UnityEngine.Light[] allLights;
    Timer greenLightTimer;
    Timer yellowLightTimer;
    Timer redLightTimer;
    GameEvent redLight = new GameEvent();
    GameEvent greenLight = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        allLights = FindObjectsByType<UnityEngine.Light>(FindObjectsSortMode.None);

        foreach (UnityEngine.Light light in allLights)
        {
            light.color = Color.green;
        }
        EventManager.AddInvoker(GameplayEvent.RedLight, redLight);
        EventManager.AddInvoker(GameplayEvent.GreenLight, greenLight);

        greenLightTimer = gameObject.AddComponent<Timer>();
        greenLightTimer.Duration = Random.Range(5, 15);
        greenLightTimer.Run();

        yellowLightTimer = gameObject.AddComponent<Timer>();
        
        redLightTimer = gameObject.AddComponent<Timer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (greenLightTimer.Finished)
        {
            foreach (UnityEngine.Light light in allLights)
            {
                light.color = Color.yellow;
            }
            yellowLightTimer.Duration = Random.Range(1, 2);
            yellowLightTimer.Run();
            greenLightTimer.Stop();
        }

        if (yellowLightTimer.Finished)
        {
            foreach (UnityEngine.Light light in allLights)
            {
                light.color = Color.red;
            }
            redLight.Invoke(redLight.Data);
            redLightTimer.Duration = Random.Range(2, 7);
            redLightTimer.Run();
            yellowLightTimer.Stop();
        }

        if (redLightTimer.Finished)
        {
            foreach (UnityEngine.Light light in allLights)
            {
                light.color = Color.green;
            }
            greenLight.Invoke(greenLight.Data);
            greenLightTimer.Duration = Random.Range(5, 15);
            greenLightTimer.Run();
            redLightTimer.Stop();
        }
    }
}
