using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private int shootDelay = 0;
    private float spawnDistance = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootDelay <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 fireDirection = transform.forward;
            Vector3 spawnPosition = transform.position + fireDirection * spawnDistance;
            Instantiate(bullet, spawnPosition, transform.rotation);
        }
    }
}
