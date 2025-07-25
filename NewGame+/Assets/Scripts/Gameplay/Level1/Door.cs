using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public int myDoorLevel;
    public Material myMaterial;
    bool isMoving = false;
    float moveSpeed = 5f;
    GameEvent updateNavMesh = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.UpdateNavMesh, updateNavMesh);
        EventManager.AddListener(GameplayEvent.OpenDoor, OpenDoorAttempt);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position.y < -5)
            {
                updateNavMesh.Invoke(updateNavMesh.Data);
                Destroy(gameObject);
            }
        }
    }

    void OpenDoorAttempt(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Door, out object output);
        GameObject DoorRef = (GameObject)output;

        data.TryGetValue(GameplayEventData.KeycardLevel, out output);
        int keyCardLevel = (int)output;

        if (DoorRef == gameObject)
        {
            if (keyCardLevel == myDoorLevel)
            {
                isMoving = true;
            }
        }
    }
}
