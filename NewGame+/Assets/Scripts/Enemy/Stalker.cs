using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{
    //Camera Information
    [Header("Vision")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float visibleThreshold = 5f;
    float visibleTimer;

    //different speeds
    [Header("Movement")]
    [SerializeField] float farSpeed = 20f;
    [SerializeField] float nearSpeed = 8f;
    [SerializeField] float slowDownRange = 25f;
    [SerializeField] float fleeDistance = 20f;

    //navmesh information
    [SerializeField] NavMeshSurface NevMesh;
    public NavMeshAgent agent;
    public Transform player;
    [SerializeField] NavMeshSurface navMesh;

    public LayerMask whatIsGround, whatIsPlayer;

    //controls attack delay
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float attackRange;
    bool playerInAttackRange;
    bool detected;

    //path updating delay
    float pathUpdateCooldown = 0.75f;
    float pathTimer;

    //event for taking damage
    GameEvent damage = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initializing navmesh and events
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, damage);
        NevMesh.BuildNavMesh();
        player = GameObject.Find("Player").transform;
        playerCamera = Camera.main;
        navMesh.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if in attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //checks distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Speed scaling
        agent.speed = distanceToPlayer > slowDownRange ? farSpeed : nearSpeed;

        // Visibility tracking
        if (IsVisibleToPlayer())
        {
            agent.speed = 0;
        }
        else
        {
            //changes speed to 0 when player is looking at enemy
            agent.speed = nearSpeed;
        }
        //checks if player is in attack range
        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            //Chase sequence
            Chase();
        }
        //helps get back on navmesh
        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(Jump());
        }
    }

    //detects if the player can see them
    bool IsVisibleToPlayer()
    {
        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(transform.position);

        // Outside camera view
        if (viewportPoint.z < 0 ||
            viewportPoint.x < 0 || viewportPoint.x > 1 ||
            viewportPoint.y < 0 || viewportPoint.y > 1)
            return false;

        // Raycast check
        Vector3 dir = transform.position - playerCamera.transform.position;
        if (Physics.Raycast(playerCamera.transform.position, dir, out RaycastHit hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    //moves towards player
    void StalkPlayer()
    {
        agent.SetDestination(player.position);
    }

    //Moves towards player and updates destination with a delay
    void Chase()
    {
        pathTimer -= Time.deltaTime;
        if (pathTimer > 0f) return;

        pathTimer = pathUpdateCooldown;
        
        agent.SetDestination(player.transform.position);
        
    }

    //attacks the player
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //AttackCodeHere
            alreadyAttacked = true;
            damage.AddData(GameplayEventData.health, -29f);
            damage.AddData(GameplayEventData.Collision, player);
            damage.Invoke(damage.Data);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    //resets the attack delay
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //controls the jump on navmesh
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
