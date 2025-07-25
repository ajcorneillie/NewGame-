using UnityEngine;

public class VisionVirusProjectile : MonoBehaviour
{
    float lifeTime = 400;
    GameEvent activateVision = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.VisionActivate, activateVision);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        lifeTime--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            activateVision.AddData(GameplayEventData.Collision, collision.gameObject);
            activateVision.AddData(GameplayEventData.Time, 5f);
            activateVision.Invoke(activateVision.Data);

            Destroy(gameObject);
        }
    }
}
