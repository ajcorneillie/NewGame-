using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunManager : MonoBehaviour
{

    [SerializeField] GameObject Player;
    [SerializeField] Slider stunBar;

    public float maxTime;
    public float currentTime;

    GameEvent stunEnd = new GameEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rt = stunBar.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.x = 0f;
        rt.sizeDelta = size;

        EventManager.AddInvoker(GameplayEvent.StunEnd, stunEnd);
        EventManager.AddListener(GameplayEvent.StunStart, AddStun);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            stunBar.value = currentTime / maxTime;
            currentTime = currentTime - 0.005f;
        }
        if(currentTime <= 0)
        {
            RectTransform rt = stunBar.GetComponent<RectTransform>();
            Vector2 size = rt.sizeDelta;
            size.x = 1f;
            rt.sizeDelta = size;
            maxTime = 0;
        }

        if(maxTime > 20)
        {
            maxTime = 20;
            RectTransform rt = stunBar.GetComponent<RectTransform>();
            Vector2 size = rt.sizeDelta;
            size.x = (25f * maxTime);
            rt.sizeDelta = size;
        }

        if ((currentTime > 20))
        {
            currentTime = 20;
            RectTransform rt = stunBar.GetComponent<RectTransform>();
            Vector2 size = rt.sizeDelta;
            size.x = (25f * maxTime);
            rt.sizeDelta = size;
        }
    }

    void AddStun(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Time, out object output);
        float newTime = (float)output;

        maxTime = maxTime + newTime;
        currentTime = currentTime + newTime;
        RectTransform rt = stunBar.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.x = size.x + (25f * newTime);
        rt.sizeDelta = size;

    }
}
