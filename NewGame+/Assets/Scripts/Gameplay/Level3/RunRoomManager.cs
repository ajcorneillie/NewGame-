using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class RunRoomManager : MonoBehaviour
{

    [SerializeField] GameObject roomForward;
    [SerializeField] GameObject roomLeft;
    [SerializeField] GameObject roomRight;
    [SerializeField] GameObject roomForwardLeft;
    [SerializeField] GameObject roomForwardRight;
    [SerializeField] GameObject roomLeftDead;
    [SerializeField] GameObject roomRightDead;

    [SerializeField] GameObject door;

    [SerializeField] GameObject plane2;

    [SerializeField] NavMeshSurface navMesh;
    public int numRooms = 20;
    int currentIndex = 0;

    float speed = 2f;
    bool hasBaked = false;
    bool hasDeadEnd;
    bool random;
    bool currentRandom;

    public int numOfTurns;
    int numOfNoDeadEnds;
    bool spawnObject = true;
    RunRoom rr;
    GameObject currentRoom;
    GameObject previousRoom;

    float newRotation;
    float newDeadRotation;

    Vector3 spawnRoomLocation = new Vector3(10,0,30);
    Vector3 Addxy;
    Vector3 AddDeadxy;

    bool isReady = false;
    List<GameObject> roomPrefabs = new List<GameObject>();
    List<GameObject> deadRoomPrefabs = new List<GameObject>();
    List<GameObject> occupiedTiles = new List<GameObject>();
    private void Awake()
    {
        roomPrefabs.Add(roomForward);
        roomPrefabs.Add(roomLeft);
        roomPrefabs.Add(roomRight);
        roomPrefabs.Add(roomForwardLeft);
        roomPrefabs.Add(roomForwardRight);

        deadRoomPrefabs.Add(roomLeftDead);
        deadRoomPrefabs.Add(roomRightDead);

        occupiedTiles.Add(gameObject);
        occupiedTiles.Add(plane2);

        currentRoom = Instantiate(roomForward, spawnRoomLocation, Quaternion.Euler(0f, 0f, 0f));
        previousRoom = currentRoom;
        rr = currentRoom.GetComponent<RunRoom>();
        isReady = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex < numRooms && isReady == true)
        {
            spawnObject = true;
            currentRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

            

            if (rr.forwardPath && rr.leftPath && random == false)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newDeadRotation = 90f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newDeadRotation = -90f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newDeadRotation = 180f;
                }
                else
                {
                    newDeadRotation = 0f;
                }

                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = 90f;
                }
                else
                {
                    newRotation = -90f;
                }
                hasDeadEnd = true;
            }
            else if (rr.forwardPath && rr.leftPath && random == true)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 90f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = -90f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = 180f;
                }
                else
                {
                    newRotation = 0f;
                }

                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newDeadRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newDeadRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newDeadRotation = 90f;
                }
                else
                {
                    newDeadRotation = -90f;
                }
                hasDeadEnd = true;
            }

            else if (rr.forwardPath && rr.rightPath && random == false)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newDeadRotation = 90f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newDeadRotation = -90f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newDeadRotation = 180f;
                }
                else
                {
                    newDeadRotation = 0f;
                }

                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = -90f;
                }
                else
                {
                    newRotation = 90f;
                }
                hasDeadEnd = true;
            }

            else if (rr.forwardPath && rr.rightPath && random == true)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 90f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = -90f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = 180f;
                }
                else
                {
                    newRotation = 0f;
                }

                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newDeadRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newDeadRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newDeadRotation = -90f;
                }
                else
                {
                    newDeadRotation = 90f;
                }
                hasDeadEnd = true;
            }
            else if (rr.leftPath)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = 90f;
                }
                else
                {
                    newRotation = -90f;
                }
                hasDeadEnd = false;
            }
            else if (rr.rightPath)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 180f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = 0f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = -90f;
                }
                else
                {
                    newRotation = 90f;
                }
                hasDeadEnd = false;
            }
            else if (rr.forwardPath)
            {
                if (previousRoom.transform.eulerAngles.y == 90f)
                {
                    newRotation = 90f;
                }
                else if (previousRoom.transform.eulerAngles.y == -90f || previousRoom.transform.eulerAngles.y == 270f)
                {
                    newRotation = -90f;
                }
                else if (previousRoom.transform.eulerAngles.y == 180f)
                {
                    newRotation = 180f;
                }
                else
                {
                    newRotation = 0f;
                }
                hasDeadEnd = false;
            }

            if (newRotation == 90)
            {
                Addxy = new Vector3(previousRoom.transform.position.x + 20, 0, previousRoom.transform.position.z);
            }
            else if (newRotation == -90)
            {
                Addxy = new Vector3(previousRoom.transform.position.x - 20, 0, previousRoom.transform.position.z);
            }
            else if (newRotation == 0)
            {
                Addxy = new Vector3(previousRoom.transform.position.x, 0, previousRoom.transform.position.z + 20);
            }
            else if (newRotation == 180)
            {
                Addxy = new Vector3(previousRoom.transform.position.x, 0, previousRoom.transform.position.z - 20);
            }

            if (currentRoom.GetComponent<RunRoom>().forwardPath && previousRoom.GetComponent<RunRoom>().forwardPath && !currentRoom.GetComponent<RunRoom>().leftPath
                && !previousRoom.GetComponent<RunRoom>().leftPath && !currentRoom.GetComponent<RunRoom>().rightPath && !previousRoom.GetComponent<RunRoom>().rightPath)
            {
                spawnObject = false;
            }
            
            if (numOfTurns < 0 && currentRoom.GetComponent<RunRoom>().leftPath)
            {
                spawnObject = false;
            }
            if (numOfTurns > 1 && currentRoom.GetComponent<RunRoom>().rightPath)
            {
                spawnObject = false;
            }

            if (hasDeadEnd == false)
            {
                if(numOfNoDeadEnds > 2)
                {
                    spawnObject = false;
                }
            }

            if (currentRoom.GetComponent<RunRoom>().leftPath && currentRoom.GetComponent<RunRoom>().forwardPath)
            {
                if (Random.Range(0, 2) == 1)
                {
                    currentRandom = false;
                }
                else
                {
                    currentRandom = true;
                }

                if (numOfTurns < 0 && currentRandom == false)
                {
                    spawnObject = false;
                }
            }
            if (currentRoom.GetComponent<RunRoom>().rightPath && currentRoom.GetComponent<RunRoom>().forwardPath)
            {
                if (Random.Range(0, 2) == 1)
                {
                    currentRandom = false;
                }
                else
                {
                    currentRandom = true;
                }

                if (numOfTurns > 1 && currentRandom == false)
                {
                    spawnObject = false;
                }
            }

            if (occupiedTiles != null)
            {
                foreach (GameObject deadEnd in occupiedTiles)
                {
                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x && deadEnd.transform.position.z == previousRoom.transform.position.z + 40)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 40 && deadEnd.transform.position.z == previousRoom.transform.position.z)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x && deadEnd.transform.position.z == previousRoom.transform.position.z - 40)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 40 && deadEnd.transform.position.z == previousRoom.transform.position.z)
                    {
                        spawnObject = false;
                    }


                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x && deadEnd.transform.position.z == previousRoom.transform.position.z + 60)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 60 && deadEnd.transform.position.z == previousRoom.transform.position.z)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x && deadEnd.transform.position.z == previousRoom.transform.position.z - 60)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().forwardPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 60 && deadEnd.transform.position.z == previousRoom.transform.position.z)
                    {
                        spawnObject = false;
                    }




                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }


                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 40 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 40)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 40 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().leftPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 40)
                    {
                        spawnObject = false;
                    }




                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }


                    if (newRotation == 0 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 40 && deadEnd.transform.position.z == previousRoom.transform.position.z + 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 90 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x + 20 && deadEnd.transform.position.z == previousRoom.transform.position.z - 40)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == 180 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 40 && deadEnd.transform.position.z == previousRoom.transform.position.z - 20)
                    {
                        spawnObject = false;
                    }
                    if (newRotation == -90 && currentRoom.GetComponent<RunRoom>().rightPath && deadEnd.transform.position.x == previousRoom.transform.position.x - 20 && deadEnd.transform.position.z == previousRoom.transform.position.z + 40)
                    {
                        spawnObject = false;
                    }
                }
            }

            if (currentRoom.GetComponent<RunRoom>().rightPath && !currentRoom.GetComponent<RunRoom>().leftPath && !currentRoom.GetComponent<RunRoom>().forwardPath)
            {
                numOfNoDeadEnds++;
                if (numOfNoDeadEnds > 2)
                {
                    spawnObject = false;
                    numOfNoDeadEnds--;
                }     
            }
            else if (!currentRoom.GetComponent<RunRoom>().rightPath && currentRoom.GetComponent<RunRoom>().leftPath && !currentRoom.GetComponent<RunRoom>().forwardPath)
            {
                numOfNoDeadEnds++;
                if (numOfNoDeadEnds > 2)
                {
                    spawnObject = false;
                    numOfNoDeadEnds--;
                }
            }
            else if (!currentRoom.GetComponent<RunRoom>().rightPath && !currentRoom.GetComponent<RunRoom>().leftPath && currentRoom.GetComponent<RunRoom>().forwardPath)
            {
                numOfNoDeadEnds++;
                if (numOfNoDeadEnds > 2)
                {
                    spawnObject = false;
                    numOfNoDeadEnds--;
                }
            }
            else
            {
                numOfNoDeadEnds = 0;
            }


            if (spawnObject == false)
            {

            }           
            else
            {
                if (hasDeadEnd == true)
                {
                    if (newDeadRotation == 90)
                    {
                        AddDeadxy = new Vector3(previousRoom.transform.position.x + 20, 0, previousRoom.transform.position.z);
                    }
                    else if (newDeadRotation == -90)
                    {
                        AddDeadxy = new Vector3(previousRoom.transform.position.x - 20, 0, previousRoom.transform.position.z);
                    }
                    else if (newDeadRotation == 0)
                    {
                        AddDeadxy = new Vector3(previousRoom.transform.position.x, 0, previousRoom.transform.position.z + 20);
                    }
                    else if (newDeadRotation == 180)
                    {
                        AddDeadxy = new Vector3(previousRoom.transform.position.x, 0, previousRoom.transform.position.z - 20);
                    }
                }

                if (hasDeadEnd)
                {
                    GameObject deadRoom = Instantiate(deadRoomPrefabs[Random.Range(0, deadRoomPrefabs.Count)], AddDeadxy, Quaternion.Euler(0f, newDeadRotation, 0f));
                    occupiedTiles.Add(deadRoom);
                }
                currentRoom = Instantiate(currentRoom, Addxy, Quaternion.Euler(0f, newRotation, 0f));

                if (currentRoom.GetComponent<RunRoom>().leftPath && !currentRoom.GetComponent<RunRoom>().forwardPath)
                {
                    numOfTurns--;
                }
                if (currentRoom.GetComponent<RunRoom>().rightPath && !currentRoom.GetComponent<RunRoom>().forwardPath)
                {
                    numOfTurns++;
                }
                if (currentRoom.GetComponent<RunRoom>().leftPath && currentRoom.GetComponent<RunRoom>().forwardPath && currentRandom == false)
                {
                    numOfTurns--;
                }
                if (currentRoom.GetComponent<RunRoom>().rightPath && currentRoom.GetComponent<RunRoom>().forwardPath && currentRandom == false)
                {
                    numOfTurns++;
                }

                random = currentRandom;
                if (currentRoom.GetComponent<RunRoom>().forwardPath && !currentRoom.GetComponent<RunRoom>().leftPath && !currentRoom.GetComponent<RunRoom>().rightPath)
                {
                    currentRoom.GetComponent<RunRoom>().forwardPath.SetActive(false);
                }
                if (random == false && currentRoom.GetComponent<RunRoom>().forwardPath)
                {
                    currentRoom.GetComponent<RunRoom>().forwardPath.SetActive(false);
                }
                if (random == true && currentRoom.GetComponent<RunRoom>().forwardPath)
                {
                    if (currentRoom.GetComponent<RunRoom>().leftPath)
                    {
                        currentRoom.GetComponent<RunRoom>().leftPath.SetActive(false);
                    }

                    if (currentRoom.GetComponent<RunRoom>().rightPath)
                    {
                        currentRoom.GetComponent<RunRoom>().rightPath.SetActive(false);
                    }

                }

                occupiedTiles.Add(currentRoom);
                float test = previousRoom.transform.eulerAngles.y;
                previousRoom = currentRoom;
                rr = currentRoom.GetComponent<RunRoom>();
                print("roomSpawned");
                currentIndex++;
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(door.transform.position.x, door.transform.position.y - 1, door.transform.position.z);
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, speed * Time.deltaTime);
            if(hasBaked == false && door.transform.position.y < -5.25)
            {
                navMesh.BuildNavMesh();
                hasBaked = true;
            }
        }
    }
}
