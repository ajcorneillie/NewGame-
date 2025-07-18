using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public ItemEnum ItemName;

    GameEvent itemUsed = new GameEvent();
    GameEvent StaminaBatteriesUsed = new GameEvent();
    GameEvent stimUsed = new GameEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.UseItem, itemUsed);
        EventManager.AddInvoker(GameplayEvent.StaminaIncrease, StaminaBatteriesUsed);
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, stimUsed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Activate();
        }
    }
    void Activate()
    {
        switch (ItemName)
        {
            case ItemEnum.VisionVirus:


                break;
            case ItemEnum.RebootKit:


                break;
            case ItemEnum.StaminaBatteries:

                StaminaBatteriesUsed.AddData(GameplayEventData.stamina, 500f);
                StaminaBatteriesUsed.Invoke(StaminaBatteriesUsed.Data);
                break;
            case ItemEnum.HealingJuice:
                stimUsed.AddData(GameplayEventData.health, 20f);
                stimUsed.Invoke(stimUsed.Data);

                break;
            case ItemEnum.StunGrenade:


                break;
        }

        itemUsed.AddData(GameplayEventData.Item, gameObject.name);
        itemUsed.Invoke(itemUsed.Data);
    }
}
