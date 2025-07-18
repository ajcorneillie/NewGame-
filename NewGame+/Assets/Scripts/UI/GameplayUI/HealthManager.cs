using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] GameObject Player;
    [SerializeField] Slider healthBar;

    public float maxHealth = 100;
    public float currentHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        EventManager.AddListener(GameplayEvent.HealthUpdate, UpdateHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealth(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.health, out object output);
        float health = (float)output;

        data.TryGetValue(GameplayEventData.Player, out  output);
        GameObject player = (GameObject)output;

            currentHealth = currentHealth + health;
            float healthPercent = currentHealth / maxHealth;
            healthBar.value = healthPercent;
            if (currentHealth <= 0)
            {
                print("you died");
            }
    }
}
