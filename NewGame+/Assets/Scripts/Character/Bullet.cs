using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    GameEvent damage = new GameEvent();
    int lifetimeFrames = 180;
    private void Start()
    {
        EventManager.AddInvoker(GameplayEvent.BulletDamage, damage);
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        lifetimeFrames--;
        if (lifetimeFrames <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage.AddData(GameplayEventData.health, 1);
        damage.AddData(GameplayEventData.Collision, collision.gameObject);
        damage.Invoke(damage.Data);
        if (collision.gameObject.layer == 12)
        {
            Destroy(gameObject);
        }
        
    }
}
