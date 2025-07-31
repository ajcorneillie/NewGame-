using UnityEngine;
using System.Collections.Generic;


public class Level4Room : MonoBehaviour
{
    [SerializeField] GameObject NDoor;
    [SerializeField] GameObject NEDoor;
    [SerializeField] GameObject EDoor;
    [SerializeField] GameObject SEDoor;
    [SerializeField] GameObject SDoor;
    [SerializeField] GameObject SWDoor;
    [SerializeField] GameObject WDoor;
    [SerializeField] GameObject NWDoor;

    [SerializeField] GameObject Stairs;
    [SerializeField] GameObject FloorStairs;
    [SerializeField] public GameObject floor;
    [SerializeField] GameObject roomAbove;

    [SerializeField] GameObject NTunnel;
    [SerializeField] GameObject NETunnel;
    [SerializeField] GameObject ETunnel;
    [SerializeField] GameObject SETunnel;
    [SerializeField] GameObject STunnel;
    [SerializeField] GameObject SWTunnel;
    [SerializeField] GameObject WTunnel;
    [SerializeField] GameObject NWTunnel;

    bool hasNDoor;
    bool hasNEdoor;
    bool hasEdoor;
    bool hasSEDoor;
    bool hasSDoor;
    bool hasSWDoor;
    bool hasWDoor;
    bool hasNWDoor;

    public bool hasStairs;
    public bool isMiddleRoom;

    int notNullDoors = 0;
    int activeDoors;
    int index;

    int indexDoor1 = -1;
    int indexDoor2 = -1;

    List<GameObject> myDoors = new List<GameObject>();
    List<GameObject> myTunnels = new List<GameObject>();
    GameEvent roomInitialized = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (NDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(NDoor);
        }
        if (NEDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(NEDoor);
        }
        if (EDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(EDoor);
        }
        if (SEDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(SEDoor);
        }
        if (SDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(SDoor);
        }
        if (SWDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(SWDoor);
        }
        if (WDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(WDoor);
        }
        if (NWDoor == null)
        {
            myDoors.Add(null);
        }
        else
        {
            myDoors.Add(NWDoor);
        }



        if (NTunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(NTunnel);
        }
        if (NETunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(NETunnel);
        }
        if (ETunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(ETunnel);
        }
        if (SETunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(SETunnel);
        }
        if (STunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(STunnel);
        }
        if (SWTunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(SWTunnel);
        }
        if (WTunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(WTunnel);
        }
        if (NWTunnel == null)
        {
            myTunnels.Add(null);
        }
        else
        {
            myTunnels.Add(NWTunnel);
        }
        EventManager.AddInvoker(GameplayEvent.RoomInitialized, roomInitialized);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeMe(bool isStairs, int myRoomNum)
    {
        if (isStairs)
        {
            hasStairs = true;
        }
        foreach(GameObject tunnel in myTunnels)
        {
            if (tunnel != null)
            {
                if (tunnel.GetComponent<Tunnel>().isActive)
                {
                    if (myDoors[index] != null)
                    {
                        myDoors[index].gameObject.SetActive(false);
                        myDoors[index] = null;
                    }
                    
                }
                else if (tunnel.activeInHierarchy == false)
                {
                    myDoors[index] = null;
                }
                
            }
            else
            {
                myDoors[index] = null;
            }
            index++;
        }
        foreach (GameObject door in myDoors)
        {
            if (door != null)
            {
                notNullDoors++;
            }
        }
        if (!isMiddleRoom)
        {
            if (notNullDoors > 2)
            {
                for (int i = 0; i < 2;)
                {
                    int randomDoorToActivate = Random.Range(0, myDoors.Count);

                    if (myDoors[randomDoorToActivate] != null && randomDoorToActivate != indexDoor1)
                    {
                        myDoors[randomDoorToActivate].gameObject.SetActive(false);
                        myTunnels[randomDoorToActivate].GetComponent<Tunnel>().isActive = true;
                        i++;
                        if (i == 1)
                        {
                            indexDoor1 = randomDoorToActivate;
                        }
                        if (i == 2)
                        {
                            indexDoor2 = randomDoorToActivate;
                        }
                    }
                    
                }
            }
            else if (notNullDoors > 1)
            {
                for (int i = 0; i < 1;)
                {
                    int randomDoorToActivate = Random.Range(0, myDoors.Count);

                    if (myDoors[randomDoorToActivate] != null && randomDoorToActivate != indexDoor1)
                    {
                        myDoors[randomDoorToActivate].gameObject.SetActive(false);
                        myTunnels[randomDoorToActivate].GetComponent<Tunnel>().isActive = true;
                        i++;
                        if (i == 1)
                        {
                            indexDoor1 = randomDoorToActivate;
                        }
                    }

                }
            }
            else
            {
                foreach (GameObject door in myDoors)
                {
                    if (door != null)
                    {
                        indexDoor1 = myDoors.IndexOf(door);
                    }
                }
            }
        }
        else
        {
            if (notNullDoors > 3)
            {
                for (int i = 0; i < 1;)
                {
                    int randomDoorToActivate = Random.Range(0, myDoors.Count);

                    if (myDoors[randomDoorToActivate] != null)
                    {
                        indexDoor1 = randomDoorToActivate;
                        myDoors[randomDoorToActivate].gameObject.SetActive(false);
                        myTunnels[randomDoorToActivate].GetComponent<Tunnel>().isActive = true;
                        i++;
                    }
                }
            }
            else if (notNullDoors > 2)
            {
                for (int i = 0; i < 2;)
                {
                    int randomDoorToActivate = Random.Range(0, myDoors.Count);

                    if (myDoors[randomDoorToActivate] != null && randomDoorToActivate != indexDoor1)
                    {
                        myDoors[randomDoorToActivate].gameObject.SetActive(false);
                        myTunnels[randomDoorToActivate].GetComponent<Tunnel>().isActive = true;
                        i++;
                        if (i == 1)
                        {
                            indexDoor1 = randomDoorToActivate;
                        }
                        if (i == 2)
                        {
                            indexDoor2 = randomDoorToActivate;
                        }
                    }

                }
            }
        }

        foreach (GameObject door in myDoors)
        {
            if (myDoors.IndexOf(door) == indexDoor1 || myDoors.IndexOf(door) == indexDoor2)
            {
                if (door != null)
                {
                    myDoors[myDoors.IndexOf(door)].gameObject.SetActive(false);
                    myTunnels[myDoors.IndexOf(door)].GetComponent<Tunnel>().isActive = true;
                }
            }
            else if (myRoomNum == 40 || myRoomNum == 39 || myRoomNum == 38 || myRoomNum == 37)
            {
                if (door != null)
                {
                    myDoors[myDoors.IndexOf(door)].gameObject.SetActive(false);
                    myTunnels[myDoors.IndexOf(door)].GetComponent<Tunnel>().isActive = true;
                }
            }
            else
            {
                int newIndex = myDoors.IndexOf(door);
                if (myTunnels[newIndex] != null)
                {
                    if (myTunnels[newIndex].GetComponent<Tunnel>().isActive != true)
                    {
                        myTunnels[newIndex].SetActive(false);
                    }
                }   
            }
        }

        if (hasStairs && !isMiddleRoom)
        {
            Vector3 newPosition = new Vector3(floor.transform.position.x, floor.transform.position.y + 6.5f, floor.transform.position.z);
            Instantiate(Stairs, newPosition, transform.rotation);
            if (roomAbove != null)
            {
                
                Instantiate(FloorStairs, roomAbove.GetComponent<Level4Room>().floor.transform.position, transform.rotation);
                roomAbove.GetComponent<Level4Room>().floor.SetActive(false);
            }
        }
        roomInitialized.Invoke(roomInitialized.Data);
    }
}
