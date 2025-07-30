using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class SentinelEnemy : MonoBehaviour
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

    [SerializeField] GameObject lightObj;

    bool checkAreaDelay;

    public float stunCycle = 5f;
    public float chaseDuration = 10f;

    Timer checkDelay;
    Timer stunTimer;
    Timer chaseTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EventManager.AddInvoker(GameplayEvent.)
        chaseTimer = gameObject.AddComponent<Timer>();
        chaseTimer.Duration = chaseDuration;

        checkDelay = gameObject.AddComponent<Timer>();
        checkDelay.Duration = checkDelayDuration;
        checkDelay.Run();

        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Duration = stunCycle;
        stunTimer.Run();
        EventManager.AddListener(GameplayEvent.SoundCreated, HearSound);
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSoundRange == false && checkDelay.Finished)
        {
            Patrolling();
        }

        if (playerInSoundRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange)
        {
            AttackPlayer();
        }

        if (stunTimer.Finished)
        {
            lightObj.SetActive(true);
            lightObj.GetComponent<SentinelStun>().stunTimer.Run();
            stunTimer.Run();
        }

        if (chaseTimer.Finished == true)
        {
            if (checkAreaDelay)
            {
                checkDelay.Run();
                checkAreaDelay = false;
            }
            agent.speed = 3f;
            playerInSoundRange = false;
        }
    }

    void Patrolling()
    {

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distancetoWalkPoint = transform.position - walkPoint;

        if(distancetoWalkPoint.magnitude < 5f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(walkPoint, out hit, 2f, NavMesh.AllAreas))
        {
            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                agent.speed = 3f;
                walkPointSet = true;
            }
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
            checkAreaDelay = true;
            agent.speed = detectedSpeed;
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //AttackCodeHere
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void HearSound(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.SoundLocation, out object output);
        GameObject soundLocation = (GameObject)output;
        if (Vector3.Distance(transform.position, soundLocation.transform.position) <= soundRange)
        {
            playerInSoundRange = true;
            chaseTimer.Run();
        }
        
    }
}