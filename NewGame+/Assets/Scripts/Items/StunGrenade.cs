using UnityEngine;
using static UnityEngine.UI.Image;

public class StunGrenade : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    float stunRadius = 3f;
    public float stunDuration = 5f;
    float fuseTime = 1.5f;
    GameEvent stun = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.StunStart, stun);
    }

    // Update is called once per frame
    void Update()
    {
        if (fuseTime <= 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, stunRadius, targetLayers);

            foreach (var hit in hitColliders)
            {
                // If using component
                if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
                {
                    stun.AddData(GameplayEventData.Time, stunDuration);
                    stun.AddData(GameplayEventData.Player, hit.gameObject);
                    stun.Invoke(stun.Data);
                }
            }
            Destroy(gameObject);
        }
        fuseTime = fuseTime - 0.005f;
    }
}
