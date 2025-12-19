using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HordeEnemy : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float detectedSpeed;
    public float checkDelayDuration;
    public float soundRange, attackRange;
    public bool playerInSoundRange, playerInAttackRange;

    public float moveSpeedDetect;
    Vector3 startLocation;

    [SerializeField] GameObject lightObj;

    bool checkAreaDelay;
    int health = 3;

    GameEvent damage = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, damage);
        startLocation = transform.position;
        EventManager.AddListener(GameplayEvent.BulletDamage, BulletDamage);
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSoundRange = Physics.CheckSphere(transform.position, soundRange, whatIsPlayer);

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else if(playerInSoundRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInSoundRange == false)
        {
            Patrolling();
            if (agent.speed != 3f)
            {
                agent.speed = 3f;
            }      
        }



    }

    void Patrolling()
    {

        if (transform.position != startLocation)
        {
            agent.SetDestination(startLocation);
        }
    }

    void ChasePlayer()
    {
        ChaseSpeedUpdate();
        agent.SetDestination(player.transform.position);
    }

    void ChaseSpeedUpdate()
    {
        if (agent.speed != detectedSpeed)
        {
            agent.speed = detectedSpeed;
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            damage.AddData(GameplayEventData.health, -9f);
            damage.AddData(GameplayEventData.Collision, player);
            damage.Invoke(damage.Data);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void BulletDamage(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Collision, out object output);
        GameObject collision = (GameObject)output;
        if (collision == gameObject)
        {
            data.TryGetValue(GameplayEventData.health, out output);
            int Health = (int)output;
            health = health - Health;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
