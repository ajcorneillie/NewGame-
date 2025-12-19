using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HordeEnemy : MonoBehaviour
{
    //NavMesh Support
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack Buffer
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //AI Detection
    public float detectedSpeed;
    public float checkDelayDuration;
    public float soundRange, attackRange;
    public bool playerInSoundRange, playerInAttackRange;

    public float moveSpeedDetect;
    Vector3 startLocation;

    bool checkAreaDelay;
    int health = 3;

    //Event for Dealling Damage
    GameEvent damage = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets start point and readies event
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, damage);
        startLocation = transform.position;
        EventManager.AddListener(GameplayEvent.BulletDamage, BulletDamage);
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if player is in attack range and sound range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSoundRange = Physics.CheckSphere(transform.position, soundRange, whatIsPlayer);

        //Attacks if in attack range
        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        //Chases player if in sound range and not in attack range
        else if(playerInSoundRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        //returns to starting location
        else if (playerInSoundRange == false)
        {
            Patrolling();
            if (agent.speed != 3f)
            {
                agent.speed = 3f;
            }      
        }
    }

    //returns to start location
    void Patrolling()
    {
        if (transform.position != startLocation)
        {
            agent.SetDestination(startLocation);
        }
    }

    //Chases after player
    void ChasePlayer()
    {
        ChaseSpeedUpdate();
        agent.SetDestination(player.transform.position);
    }

    //updates speed when chasing
    void ChaseSpeedUpdate()
    {
        if (agent.speed != detectedSpeed)
        {
            agent.speed = detectedSpeed;
        }
    }

    //attacks player 
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //controls event to attack player and adds attack delay
            alreadyAttacked = true;
            damage.AddData(GameplayEventData.health, -9f);
            damage.AddData(GameplayEventData.Collision, player);
            damage.Invoke(damage.Data);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    //resets the attack
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //deals with being hit by a projectile logic
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
