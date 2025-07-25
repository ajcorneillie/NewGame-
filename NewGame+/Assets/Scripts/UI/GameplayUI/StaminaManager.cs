using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public float maxStamina;
    public float currentStamina;
    public float staminaRegenRate = 1f;
    bool staminaLock = false;

    [SerializeField] GameObject Player;

    [SerializeField] Slider staminaBar;
    [SerializeField] Slider extraStaminaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        extraStaminaBar.gameObject.SetActive(false);
        currentStamina = maxStamina;
        EventManager.AddListener(GameplayEvent.Running, IsRunning);
        EventManager.AddListener(GameplayEvent.StaminaIncrease, StaminaIncrease);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina = currentStamina + staminaRegenRate;
            if (currentStamina < 0)
            {
                currentStamina = 0;
                staminaLock = true;
            }
            SetStamina();
        }
        
        if (staminaLock == true)
        {
            Player.GetComponent<PlayerMovement>().staminaLock = true;
        }

        if (currentStamina == maxStamina)
        {
            staminaLock = false;
            Player.GetComponent<PlayerMovement>().staminaLock = false;
        }

        if (currentStamina > maxStamina)
        {
            staminaBar.value = 1f;
            extraStaminaBar.gameObject.SetActive(true);
            float extraStamina = currentStamina - maxStamina;
            RectTransform rt = extraStaminaBar.GetComponent<RectTransform>();
            Vector2 size = rt.sizeDelta;
            size.x = 0.45f * extraStamina;
            rt.sizeDelta = size;
        }
        else
        {
            extraStaminaBar.gameObject.SetActive(false);
        }

    }

    void IsRunning(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.stamina, out object output);
        float dropRate = (float)output;

        data.TryGetValue(GameplayEventData.Player, out output);
        GameObject player = (GameObject)output;

        if (player == Player)
        {
            if (dropRate > 0)
            {
                currentStamina = currentStamina - dropRate;
            }
            else
            {
                if (currentStamina < maxStamina)
                {
                    currentStamina = currentStamina - dropRate;
                }
            }
        }
        
    }

    void SetStamina()
    {
        float staminaBarPercent = currentStamina / maxStamina;
        staminaBar.value = staminaBarPercent;
    }

    void StaminaIncrease(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.stamina, out object output);
        float StaminaAmount = (float)output;

        currentStamina = currentStamina + StaminaAmount;

    }
}
