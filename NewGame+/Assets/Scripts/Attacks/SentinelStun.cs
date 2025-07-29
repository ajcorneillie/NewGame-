using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SentinelStun : MonoBehaviour
{
    public bool playerInside = false;
    [SerializeField] GameObject mySentinel;

    private Collider zoneCollider;

    bool hasActivated = false;

    [SerializeField] GameObject player;

    public Timer stunTimer;
    GameEvent activateStun = new GameEvent();
    private void Start()
    {
        zoneCollider = GetComponent<Collider>();
        EventManager.AddInvoker(GameplayEvent.StunStart, activateStun);
        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Duration = 0.25f;
        stunTimer.Run();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void Update()
    {
        if (stunTimer.Finished)
        {
            hasActivated = true;
            gameObject.SetActive(false);
        }
        if (playerInside == true)
        {

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            Collider targetCollider = mySentinel.GetComponent<Collider>();

            if (GeometryUtility.TestPlanesAABB(planes, targetCollider.bounds))
            {
                activateStun.AddData(GameplayEventData.Player, player);
                activateStun.AddData(GameplayEventData.Time, 5f);
                activateStun.Invoke(activateStun.Data);
                hasActivated = true;
            }
        }
        if (zoneCollider.bounds.Contains(player.transform.position) && hasActivated == false)
        {
            playerInside = true;
        }
        else
        {
            playerInside = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
