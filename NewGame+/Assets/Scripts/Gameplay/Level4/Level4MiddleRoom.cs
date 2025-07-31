using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Level4MiddleRoom : MonoBehaviour
{
    [SerializeField] GameObject NEDoor;
    [SerializeField] GameObject SEDoor;
    [SerializeField] GameObject SWDoor;
    [SerializeField] GameObject NWDoor;

    [SerializeField] GameObject NETunnel;
    [SerializeField] GameObject SETunnel;
    [SerializeField] GameObject SWTunnel;
    [SerializeField] GameObject NWTunnel;

    int notNullDoors;

    List<GameObject> myDoors = new List<GameObject>();
    List<GameObject> myTunnels = new List<GameObject>();
    GameEvent roomInitialized = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myDoors.Add(NEDoor);
        myDoors.Add(SEDoor);
        myDoors.Add(SWDoor);
        myDoors.Add(NWDoor);

        myTunnels.Add(NETunnel);
        myTunnels.Add(SETunnel);
        myTunnels.Add(SWTunnel);
        myTunnels.Add(NWTunnel);
        EventManager.AddInvoker(GameplayEvent.RoomInitialized, roomInitialized);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeMe()
    {
        foreach (GameObject tunnel in myTunnels)
        {
            if (tunnel != null)
            {
                if (tunnel.GetComponent<Tunnel>().isActive)
                {
                    int index = myTunnels.IndexOf(tunnel);
                    myDoors[index].gameObject.SetActive(false);
                    myDoors[index] = null;
                }

            }
        }
        foreach (GameObject door in myDoors)
        {
            if (door != null)
            {
                notNullDoors++;
            }
        }
        if (notNullDoors > 3)
        {
            for (int i = 0; i < 1;)
            {
                int randomDoorToActivate = Random.Range(0, myDoors.Count);

                if (myDoors[randomDoorToActivate] != null)
                {
                    myDoors[randomDoorToActivate].gameObject.SetActive(false);
                    myTunnels[randomDoorToActivate].GetComponent<Tunnel>().isActive = true;
                    myDoors[randomDoorToActivate] = null;
                    i++;
                }
            }
        }

        foreach (GameObject tunnel in myTunnels)
        {
            if (tunnel != null)
            {
                Destroy(tunnel);
            }
        }
        roomInitialized.Invoke(roomInitialized.Data);
    }
}
