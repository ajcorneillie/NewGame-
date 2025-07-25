using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] Level1Rooms room1;
    [SerializeField] Level1Rooms room2;
    [SerializeField] Level1Rooms room3;
    [SerializeField] Level1Rooms room4;
    [SerializeField] Level1Rooms room5;
    [SerializeField] Level1Rooms room6;
    [SerializeField] Level1Rooms room7;
    [SerializeField] Level1Rooms room8;
    [SerializeField] Level1Rooms room9;
    [SerializeField] Level1Rooms room10;
    [SerializeField] Level1Rooms room11;
    [SerializeField] Level1Rooms room12;
    [SerializeField] Level1Rooms room13;
    [SerializeField] Level1Rooms room14;
    [SerializeField] Level1Rooms room15;
    [SerializeField] Level1Rooms room16;
    [SerializeField] Level1Rooms room17;
    [SerializeField] Level1Rooms room18;
    [SerializeField] Level1Rooms room19;
    [SerializeField] Level1Rooms room20;
    [SerializeField] Level1Rooms room21;
    [SerializeField] Level1Rooms room22;
    [SerializeField] Level1Rooms room23;
    [SerializeField] Level1Rooms room24;
    [SerializeField] Level1Rooms room25;
    [SerializeField] Level1Rooms room26;
    [SerializeField] Level1Rooms room27;
    [SerializeField] Level1Rooms room28;
    [SerializeField] Level1Rooms room29;
    [SerializeField] Level1Rooms room30;
    [SerializeField] Level1Rooms room31;
    [SerializeField] Level1Rooms room32;
    [SerializeField] Level1Rooms room33;
    [SerializeField] Level1Rooms room34;
    [SerializeField] Level1Rooms room35;

    [SerializeField] GameObject theRoom1;
    [SerializeField] GameObject theRoom2;
    [SerializeField] GameObject theRoom3;
    [SerializeField] GameObject theRoom4;
    [SerializeField] GameObject theRoom5;
    [SerializeField] GameObject theRoom6;
    [SerializeField] GameObject theRoom7;
    [SerializeField] GameObject theRoom8;
    [SerializeField] GameObject theRoom9;
    [SerializeField] GameObject theRoom10;
    [SerializeField] GameObject theRoom11;
    [SerializeField] GameObject theRoom12;
    [SerializeField] GameObject theRoom13;
    [SerializeField] GameObject theRoom14;
    [SerializeField] GameObject theRoom15;
    [SerializeField] GameObject theRoom16;
    [SerializeField] GameObject theRoom17;
    [SerializeField] GameObject theRoom18;
    [SerializeField] GameObject theRoom19;
    [SerializeField] GameObject theRoom20;
    [SerializeField] GameObject theRoom21;
    [SerializeField] GameObject theRoom22;
    [SerializeField] GameObject theRoom23;
    [SerializeField] GameObject theRoom24;
    [SerializeField] GameObject theRoom25;
    [SerializeField] GameObject theRoom26;
    [SerializeField] GameObject theRoom27;
    [SerializeField] GameObject theRoom28;
    [SerializeField] GameObject theRoom29;
    [SerializeField] GameObject theRoom30;
    [SerializeField] GameObject theRoom31;
    [SerializeField] GameObject theRoom32;
    [SerializeField] GameObject theRoom33;
    [SerializeField] GameObject theRoom34;
    [SerializeField] GameObject theRoom35;

    List<Level1Rooms> roomList = new List<Level1Rooms>();
    List<GameObject> theRoomList = new List<GameObject>();

    GameEvent InitializeRoom = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomList.Add(room1);
        roomList.Add(room2);
        roomList.Add(room3);
        roomList.Add(room4);
        roomList.Add(room5);
        roomList.Add(room6);
        roomList.Add(room7);
        roomList.Add(room8);
        roomList.Add(room9);
        roomList.Add(room10);
        roomList.Add(room11);
        roomList.Add(room12);
        roomList.Add(room13);
        roomList.Add(room14);
        roomList.Add(room15);
        roomList.Add(room16);
        roomList.Add(room17);
        roomList.Add(room18);
        roomList.Add(room19);
        roomList.Add(room20);
        roomList.Add(room21);
        roomList.Add(room21);
        roomList.Add(room23);
        roomList.Add(room24);
        roomList.Add(room25);
        roomList.Add(room26);
        roomList.Add(room27);
        roomList.Add(room28);
        roomList.Add(room29);
        roomList.Add(room30);
        roomList.Add(room31);
        roomList.Add(room32);
        roomList.Add(room33);
        roomList.Add(room34);
        roomList.Add(room35);

        theRoomList.Add(theRoom1);
        theRoomList.Add(theRoom2);
        theRoomList.Add(theRoom3);
        theRoomList.Add(theRoom4);
        theRoomList.Add(theRoom5);
        theRoomList.Add(theRoom6);
        theRoomList.Add(theRoom7);
        theRoomList.Add(theRoom8);
        theRoomList.Add(theRoom9);
        theRoomList.Add(theRoom10);
        theRoomList.Add(theRoom11);
        theRoomList.Add(theRoom12);
        theRoomList.Add(theRoom13);
        theRoomList.Add(theRoom14);
        theRoomList.Add(theRoom15);
        theRoomList.Add(theRoom16);
        theRoomList.Add(theRoom17);
        theRoomList.Add(theRoom18);
        theRoomList.Add(theRoom19);
        theRoomList.Add(theRoom20);
        theRoomList.Add(theRoom21);
        theRoomList.Add(theRoom22);
        theRoomList.Add(theRoom23);
        theRoomList.Add(theRoom24);
        theRoomList.Add(theRoom25);
        theRoomList.Add(theRoom26);
        theRoomList.Add(theRoom27);
        theRoomList.Add(theRoom28);
        theRoomList.Add(theRoom29);
        theRoomList.Add(theRoom30);
        theRoomList.Add(theRoom31);
        theRoomList.Add(theRoom32);
        theRoomList.Add(theRoom33);
        theRoomList.Add(theRoom34);
        theRoomList.Add(theRoom35);

        EventManager.AddInvoker(GameplayEvent.InitializeRoom, InitializeRoom);
        foreach (GameObject room in theRoomList)
        {
            int randomIndex = Random.Range(0, roomList.Count);
            InitializeRoom.AddData(GameplayEventData.RoomInfo, roomList[randomIndex]);
            InitializeRoom.AddData(GameplayEventData.Room, room);
            InitializeRoom.Invoke(InitializeRoom.Data);
            roomList.RemoveAt(randomIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
