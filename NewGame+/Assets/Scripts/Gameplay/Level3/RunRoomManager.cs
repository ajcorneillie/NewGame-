using System.Collections.Generic;
using UnityEngine;

public class RunRoomManager : MonoBehaviour
{

    [SerializeField] GameObject roomForward;
    [SerializeField] GameObject roomLeft;
    [SerializeField] GameObject roomRight;
    [SerializeField] GameObject roomForwardLeft;
    [SerializeField] GameObject roomForwardRight;
    [SerializeField] GameObject roomForwardDead;
    [SerializeField] GameObject roomLeftDead;
    [SerializeField] GameObject roomRightDead;
    [SerializeField] GameObject roomForwardLeftDead;
    [SerializeField] GameObject roomForwardRightDead;

    [SerializeField] GameObject plane2;

    public int numRooms = 20;
    int currentIndex = 0;

    bool hasDeadEnd;
    bool random;
    bool noStraight;

    public int numOfTurns;
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

        deadRoomPrefabs.Add(roomForwardDead);
        deadRoomPrefabs.Add(roomLeftDead);
        deadRoomPrefabs.Add(roomRightDead);
        deadRoomPrefabs.Add(roomForwardLeftDead);
        deadRoomPrefabs.Add(roomForwardRightDead);

        occupiedTiles.Add(gameObject);
        occupiedTiles.Add(plane2);

        currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count - 2)], spawnRoomLocation, Quaternion.Euler(0f, 0f, 0f));
        previousRoom = currentRoom;
        rr = currentRoom.GetComponent<RunRoom>();
        if (currentRoom.GetComponent<RunRoom>().leftPath)
        {
            numOfTurns--;
        }
        if (currentRoom.GetComponent<RunRoom>().rightPath)
        {
            numOfTurns++;
        }
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
            
   


            if (rr.forwardPath && rr.leftPath)
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
            else if (rr.forwardPath && rr.rightPath)
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




            if (numOfTurns < 0 && currentRoom.GetComponent<RunRoom>().leftPath)
            {
                spawnObject = false;
            }
            if (numOfTurns > 1 && currentRoom.GetComponent<RunRoom>().rightPath)
            {
                spawnObject = false;
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
                if (currentRoom.GetComponent<RunRoom>().leftPath)
                {
                    numOfTurns--;
                }
                if (currentRoom.GetComponent<RunRoom>().rightPath)
                {
                    numOfTurns++;
                }

                occupiedTiles.Add(currentRoom);
                float test = previousRoom.transform.eulerAngles.y;
                previousRoom = currentRoom;
                rr = currentRoom.GetComponent<RunRoom>();
                print("roomSpawned");
                currentIndex++;
            }
        }
    }
}
