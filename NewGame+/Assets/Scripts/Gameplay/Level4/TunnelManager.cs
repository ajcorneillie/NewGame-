using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TunnelManager : MonoBehaviour
{
    [SerializeField] NavMeshSurface navMesh;

    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;
    [SerializeField] GameObject room3;
    [SerializeField] GameObject room4;
    [SerializeField] GameObject room5;
    [SerializeField] GameObject room6;
    [SerializeField] GameObject room7;
    [SerializeField] GameObject room8;
    [SerializeField] GameObject room9;
    [SerializeField] GameObject room10;
    [SerializeField] GameObject room11;
    [SerializeField] GameObject room12;
    [SerializeField] GameObject room13;
    [SerializeField] GameObject room14;
    [SerializeField] GameObject room15;
    [SerializeField] GameObject room16;
    [SerializeField] GameObject room17;
    [SerializeField] GameObject room18;
    [SerializeField] GameObject room19;
    [SerializeField] GameObject room20;
    [SerializeField] GameObject room21;
    [SerializeField] GameObject room22;
    [SerializeField] GameObject room23;
    [SerializeField] GameObject room24;
    [SerializeField] GameObject room25;

    [SerializeField] GameObject middleRoom1;
    [SerializeField] GameObject middleRoom2;
    [SerializeField] GameObject middleRoom3;
    [SerializeField] GameObject middleRoom4;
    [SerializeField] GameObject middleRoom5;
    [SerializeField] GameObject middleRoom6;
    [SerializeField] GameObject middleRoom7;
    [SerializeField] GameObject middleRoom8;
    [SerializeField] GameObject middleRoom9;
    [SerializeField] GameObject middleRoom10;
    [SerializeField] GameObject middleRoom11;
    [SerializeField] GameObject middleRoom12;
    [SerializeField] GameObject middleRoom13;
    [SerializeField] GameObject middleRoom14;
    [SerializeField] GameObject middleRoom15;
    [SerializeField] GameObject middleRoom16;


    public int floorNum;
    int stairNum;
    bool isStair = false;
    int currentIndex = 0;
    bool hasAssignedStair = false;

    List<GameObject> roomList = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddListener(GameplayEvent.RoomInitialized, RoomInitialized);
        roomList.Add(room1);
        roomList.Add(middleRoom1);
        roomList.Add(room2);
        roomList.Add(middleRoom2);
        roomList.Add(room3);
        roomList.Add(middleRoom3);
        roomList.Add(room4);
        roomList.Add(middleRoom4);
        roomList.Add(room5);

        roomList.Add(room6);
        roomList.Add(middleRoom5);
        roomList.Add(room7);
        roomList.Add(middleRoom6);
        roomList.Add(room8);
        roomList.Add(middleRoom7);
        roomList.Add(room9);
        roomList.Add(middleRoom8);
        roomList.Add(room10);

        roomList.Add(room11);
        roomList.Add(middleRoom9);
        roomList.Add(room12);
        roomList.Add(middleRoom10);
        roomList.Add(room13);
        roomList.Add(middleRoom11);
        roomList.Add(room14);
        roomList.Add(middleRoom12);
        roomList.Add(room15);

        roomList.Add(room16);
        roomList.Add(middleRoom13);
        roomList.Add(room17);
        roomList.Add(middleRoom14);
        roomList.Add(room18);
        roomList.Add(middleRoom15);
        roomList.Add(room19);
        roomList.Add(middleRoom16);
        roomList.Add(room20);

        roomList.Add(room21);
        roomList.Add(room22);
        roomList.Add(room23);
        roomList.Add(room24);
        roomList.Add(room25);

        stairNum = Random.Range(0, roomList.Count);

        while (hasAssignedStair == false)
        {
            if (roomList[stairNum].GetComponent<Level4Room>().isMiddleRoom)
            {
                stairNum = Random.Range(0, roomList.Count);
            }
            else
            {
                roomList[stairNum].GetComponent<Level4Room>().hasStairs = true;
                hasAssignedStair = true;
            }
        }
        
        if (currentIndex == stairNum)
        {
            isStair = true;
        }
        else
        {
            isStair = false;
        }
        roomList[currentIndex].GetComponent<Level4Room>().InitializeMe(isStair, currentIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RoomInitialized(Dictionary<System.Enum, object> data)
    {
        currentIndex++;
        if (roomList.Count > currentIndex)
        {
            if (currentIndex == stairNum)
            {
                isStair = true;
            }
            else
            {
                isStair = false;
            }
            roomList[currentIndex].GetComponent<Level4Room>().InitializeMe(isStair, currentIndex);
        }
    }
}
