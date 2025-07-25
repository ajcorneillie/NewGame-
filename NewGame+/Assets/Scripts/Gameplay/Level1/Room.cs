using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField] GameObject myKey;
    [SerializeField] GameObject keyLocation;
    [SerializeField] GameObject myNorthDoor;
    [SerializeField] GameObject mySouthDoor;

    [SerializeField] Material doorLevel1Material;
    [SerializeField] Material doorLevel2Material;
    [SerializeField] Material doorLevel3Material;
    [SerializeField] Material doorLevel4Material;
    [SerializeField] Material doorLevel5Material;
    [SerializeField] Material doorLevel6Material;
    [SerializeField] Material doorLevel7Material;
    [SerializeField] Material doorLevel8Material;

    List<Material> levelMaterialList = new List<Material>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        levelMaterialList.Add(doorLevel1Material);
        levelMaterialList.Add(doorLevel2Material);
        levelMaterialList.Add(doorLevel3Material);
        levelMaterialList.Add(doorLevel4Material);
        levelMaterialList.Add(doorLevel5Material);
        levelMaterialList.Add(doorLevel6Material);
        levelMaterialList.Add(doorLevel7Material);
        levelMaterialList.Add(doorLevel8Material);

        EventManager.AddListener(GameplayEvent.InitializeRoom, InitializeMe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeMe(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.RoomInfo, out object output);
        Level1Rooms myRoom = (Level1Rooms)output;
        data.TryGetValue(GameplayEventData.Room, out output);
        GameObject TheRoom = (GameObject)output;

        if (TheRoom == gameObject)
        {
            if (myRoom.hasKey == false)
            {
                keyLocation.SetActive(false);
            }
            else
            {
                keyLocation.SetActive(true);
                myKey = myRoom.KeyScriptable.myObject;
                Instantiate(myKey, keyLocation.transform.position, keyLocation.transform.rotation);
                keyLocation.SetActive(false);
            }

            mySouthDoor.GetComponent<MeshRenderer>().material = levelMaterialList[myRoom.southDoorLevel - 1];
            mySouthDoor.GetComponent<Door>().myDoorLevel = myRoom.southDoorLevel;
            myNorthDoor.GetComponent<MeshRenderer>().material = levelMaterialList[myRoom.northDoorLevel - 1];
            myNorthDoor.GetComponent<Door>().myDoorLevel = myRoom.northDoorLevel;
        }
    }
}
