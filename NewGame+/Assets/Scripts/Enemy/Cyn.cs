using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Cyn : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    [SerializeField] NavMeshSurface navMesh;

    public float viewRadius = 40f;
    public float viewAngle = 120f;
    public LayerMask obstacleMask;

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

    bool checkAreaDelay;

    public float stunCycle = 5f;
    public float chaseDuration = 3f;

    Timer checkDelay;
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

        player = GameObject.Find("Player").transform;
        navMesh.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (playerInSoundRange == false && checkDelay.Finished)
        //{
        //    Patrolling();
        //}

        if (playerInSoundRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        //if (playerInAttackRange)
        //{
        //    AttackPlayer();
        //}

        //if (checkDelay.Finished != true)
        //{
        //    transform.Rotate(0f, 0.5f * Time.deltaTime, 0f);
        //}

        //if (chaseTimer.Finished == true)
        //{
        //    if (checkAreaDelay)
        //    {
        //        checkDelay.Run();
        //        checkAreaDelay = false;
        //    }
        //    agent.speed = 3f;
        //    playerInSoundRange = false;
        //}

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        // Check if player is within cone angle and radius
        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2f &&
            distanceToPlayer <= viewRadius)
        {
            // Raycast to check if something is blocking view
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask) && !playerInAttackRange)
            {
                playerInSoundRange = true;
            }
            else
            {
                AttackPlayer();
                playerInSoundRange = false;
            }
        }

        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(Jump());
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

        if (distancetoWalkPoint.magnitude < 5f)
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);
    }

    IEnumerator Jump()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = agent.currentOffMeshLinkData.endPos;
        float jumpHeight = 2f;
        float duration = 0.5f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            yield return null;
        }

        agent.CompleteOffMeshLink();
    }
}
