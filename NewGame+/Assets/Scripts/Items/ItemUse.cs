using UnityEngine;
using UnityEngine.UIElements;

public class ItemUse : MonoBehaviour
{
    public ItemEnum ItemName;

    [SerializeField] GameObject stunGrenade;
    [SerializeField] GameObject visionVirusProjectile;
    [SerializeField] GameObject Player;
    [SerializeField] Camera playerCamera;

    float doorOpenRange = 2;

    GameEvent itemUsed = new GameEvent();
    GameEvent StaminaBatteriesUsed = new GameEvent();
    GameEvent stimUsed = new GameEvent();
    GameEvent openDoorAttempt = new GameEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.UseItem, itemUsed);
        EventManager.AddInvoker(GameplayEvent.StaminaIncrease, StaminaBatteriesUsed);
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, stimUsed);
        EventManager.AddInvoker(GameplayEvent.OpenDoor, openDoorAttempt);
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

                GameObject projectile = Instantiate(visionVirusProjectile, gameObject.transform.position + (playerCamera.transform.forward * 1f), Quaternion.identity);


                Rigidbody rib = projectile.GetComponent<Rigidbody>();
                if (rib != null)
                {
                    rib.linearVelocity = Camera.main.transform.forward * 10f;
                }

                itemUsed.AddData(GameplayEventData.Item, gameObject.name);
                itemUsed.Invoke(itemUsed.Data);

                break;
            case ItemEnum.RebootKit:

                itemUsed.AddData(GameplayEventData.Item, gameObject.name);
                itemUsed.Invoke(itemUsed.Data);

                break;
            case ItemEnum.StaminaBatteries:

                StaminaBatteriesUsed.AddData(GameplayEventData.stamina, 500f);
                StaminaBatteriesUsed.Invoke(StaminaBatteriesUsed.Data);

                itemUsed.AddData(GameplayEventData.Item, gameObject.name);
                itemUsed.Invoke(itemUsed.Data);

                break;
            case ItemEnum.HealingJuice:
                stimUsed.AddData(GameplayEventData.health, 20f);
                stimUsed.Invoke(stimUsed.Data);

                itemUsed.AddData(GameplayEventData.Item, gameObject.name);
                itemUsed.Invoke(itemUsed.Data);

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

                itemUsed.AddData(GameplayEventData.Item, gameObject.name);
                itemUsed.Invoke(itemUsed.Data);

                break;
            case ItemEnum.RedKeycard:
                Collider[] hitColliders = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 1);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }

                break;
            case ItemEnum.OrangeKeycard:
                Collider[] hitColliders1 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders1)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 2);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }

                break;
            case ItemEnum.YellowKeycard:
                Collider[] hitColliders2 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders2)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 3);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
            case ItemEnum.GreenKeycard:
                Collider[] hitColliders3 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders3)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 4);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
            case ItemEnum.BlueKeycard:
                Collider[] hitColliders4 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders4)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 5);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
            case ItemEnum.PurpleKeycard:
                Collider[] hitColliders5 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders5)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 6);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
            case ItemEnum.PinkKeycard:
                Collider[] hitColliders6 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders6)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 7);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
            case ItemEnum.OmniKeycard:
                Collider[] hitColliders7 = Physics.OverlapSphere(Player.transform.position, doorOpenRange);
                foreach (var hit in hitColliders7)
                {
                    if (hit.CompareTag("Door"))
                    {
                        Debug.Log("Found a door: " + hit.name);
                        openDoorAttempt.AddData(GameplayEventData.KeycardLevel, 8);
                        openDoorAttempt.AddData(GameplayEventData.Door, hit.gameObject);
                        openDoorAttempt.Invoke(openDoorAttempt.Data);
                    }
                }
                break;
        }
    }
}
