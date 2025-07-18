using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{


    List<GameObject> Slots = new List<GameObject>();

    private void Start()
    {


        EventManager.AddListener(GameplayEvent.PickupItemAttempt, PickUpItemAttempt);
    }
    private void Update()
    {
        
    }

    void PickUpItemAttempt(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Item, out object output);
        GameObject item = (GameObject)output;

        foreach (GameObject slot in Slots)
        {
            if (slot.GetComponent<Slot>().isFull == false)
            {
                slot.GetComponent<Slot>().isFull = true;
                slot.GetComponent<Slot>().myIcon.color = item.GetComponent<DropedItem>().myIcon.color;
                slot.GetComponent<Slot>().myObject = item.GetComponent<DropedItem>().myObject;
                Destroy(item);
                break;
            }
        }

    }

    public void InitializeMe(List<GameObject> slots)
    {
        foreach (GameObject slot in slots)
        {
            Slots.Add(slot);
        }
    }
}
