using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class TrudgeEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    [SerializeField] GameObject player;
    [SerializeField] NavMeshSurface navMesh;
    public LayerMask whatIsGround, whatIsPlayer;

    public float attackCooldown = 5f;
    bool alreadyAttacked;
    public bool playerInAttackRange;
    float attackRange = 5f;

    GameEvent damagePlayers = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddListener(GameplayEvent.UpdateNavMesh, UpdateNavMesh);
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, damagePlayers);
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInAttackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }
    }
    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(Player);
        if (!alreadyAttacked)
        {
            damagePlayers.AddData(GameplayEventData.health, -100f);
            damagePlayers.AddData(GameplayEventData.Player, player);
            damagePlayers.Invoke(damagePlayers.Data);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void Chase()
    {
        agent.SetDestination(Player.position);
    }

    void UpdateNavMesh(Dictionary<System.Enum, object> data)
    {
        navMesh.BuildNavMesh();
    }
}
