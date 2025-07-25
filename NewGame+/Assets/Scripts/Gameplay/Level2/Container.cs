using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;
    [SerializeField] GameObject placeHolder;

    [SerializeField] GameObject visionVirusPickup;
    [SerializeField] GameObject rebootKitPickup;
    [SerializeField] GameObject healthJuicePickup;
    [SerializeField] GameObject staminaBatteriesPickup;
    [SerializeField] GameObject stunGrenadePickup;

    List<GameObject> items = new List<GameObject>();
    public bool has1Camera;

    int maxNum = 100;
    int chanceNum = 50;
    int itemChanceNum = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items.Add(visionVirusPickup);
        items.Add(healthJuicePickup);
        items.Add(staminaBatteriesPickup);
        items.Add(stunGrenadePickup);

        if (Random.Range(0, maxNum) < itemChanceNum)
        {
            int randomItem = Random.Range(0, items.Count);
            switch (randomItem)
            {
                case 0:
                    Instantiate(items[0], placeHolder.transform.position, placeHolder.transform.rotation);
                    break;
                case 1:
                    Instantiate(items[1], placeHolder.transform.position, placeHolder.transform.rotation);
                    break;
                case 2:
                    Instantiate(items[2], placeHolder.transform.position, placeHolder.transform.rotation);
                    break;
                case 3:
                    Instantiate(items[3], placeHolder.transform.position, placeHolder.transform.rotation);
                    break;
                case 4:
                    Instantiate(items[4], placeHolder.transform.position, placeHolder.transform.rotation);
                    break;
            }
        }
        placeHolder.SetActive(false);
        
        camera1.SetActive(false);
        camera2.SetActive(false);

        if (Random.Range(0, maxNum) < chanceNum + 1)
        {
            camera1.SetActive(true);
        }
        if (Random.Range(0, maxNum) < chanceNum + 1 && has1Camera == false)
        {
            camera2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
