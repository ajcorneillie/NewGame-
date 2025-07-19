using UnityEngine;
using UnityEngine.UIElements;

public class ItemUse : MonoBehaviour
{
    public ItemEnum ItemName;

    [SerializeField] GameObject stunGrenade;
    [SerializeField] GameObject Player;
    [SerializeField] Camera playerCamera;

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
                Vector3 aimDirection = playerCamera.transform.forward;
                GameObject item = Instantiate(stunGrenade, Player.transform.position + (playerCamera.transform.forward * 1f), Quaternion.identity);
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(aimDirection.normalized * 10f, ForceMode.Impulse);
                    Vector3 randomSpin = new Vector3(0.5f, 0f, 0.5f).normalized * 1f;
                    rb.AddTorque(randomSpin, ForceMode.Impulse);
                }

                break;
        }

        itemUsed.AddData(GameplayEventData.Item, gameObject.name);
        itemUsed.Invoke(itemUsed.Data);
    }
}
