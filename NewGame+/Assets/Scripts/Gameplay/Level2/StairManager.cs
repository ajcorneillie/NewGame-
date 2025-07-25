using UnityEngine;

public class StairManager : MonoBehaviour
{
    [SerializeField] GameObject floor1Stair1;
    [SerializeField] GameObject floor1Stair2;
    [SerializeField] GameObject floor1Stair3;

    [SerializeField] GameObject floor2Stair1;
    [SerializeField] GameObject floor2Stair2;
    [SerializeField] GameObject floor2Stair3;

    [SerializeField] GameObject floor3Stair1;  
    [SerializeField] GameObject floor3Stair2;
    [SerializeField] GameObject floor3Stair3;

    [SerializeField] GameObject floor4Stair1;
    [SerializeField] GameObject floor4Stair2;
    [SerializeField] GameObject floor4Stair3;

    [SerializeField] GameObject floor5Stair1;
    [SerializeField] GameObject floor5Stair2;
    [SerializeField] GameObject floor5Stair3;

    [SerializeField] GameObject floor6Stair1;
    [SerializeField] GameObject floor6Stair2;

    [SerializeField] GameObject floor7Stair1;
    [SerializeField] GameObject floor7Stair2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floor1Stair1.SetActive(false);
        floor1Stair2.SetActive(false);
        floor1Stair3.SetActive(false);

        floor2Stair1.SetActive(false);
        floor2Stair2.SetActive(false);
        floor2Stair3.SetActive(false);

        floor3Stair1.SetActive(false);
        floor3Stair2.SetActive(false);
        floor3Stair3.SetActive(false);

        floor4Stair1.SetActive(false);
        floor4Stair2.SetActive(false);
        floor4Stair3.SetActive(false);

        floor5Stair1.SetActive(false);
        floor5Stair2.SetActive(false);
        floor5Stair3.SetActive(false);

        floor6Stair1.SetActive(false);
        floor6Stair2.SetActive(false);
        
        floor7Stair1.SetActive(false);
        floor7Stair2.SetActive(false);

        int stair = Random.Range(0, 3);
        switch (stair) 
        { 
            case 0:
                floor1Stair1.SetActive(true);
                break;
            case 1:
                floor1Stair2.SetActive(true);
                break;
            case 2:
                floor1Stair3.SetActive(true);
                break;
        }

        stair = Random.Range(0, 3);
        switch (stair)
        {
            case 0:
                floor2Stair1.SetActive(true);
                break;
            case 1:
                floor2Stair2.SetActive(true);
                break;
            case 2:
                floor2Stair3.SetActive(true);
                break;
        }

        stair = Random.Range(0, 3);
        switch (stair)
        {
            case 0:
                floor3Stair1.SetActive(true);
                break;
            case 1:
                floor3Stair2.SetActive(true);
                break;
            case 2:
                floor3Stair3.SetActive(true);
                break;
        }

        stair = Random.Range(0, 3);
        switch (stair)
        {
            case 0:
                floor4Stair1.SetActive(true);
                break;
            case 1:
                floor4Stair2.SetActive(true);
                break;
            case 2:
                floor4Stair3.SetActive(true);
                break;
        }

        stair = Random.Range(0, 3);
        switch (stair)
        {
            case 0:
                floor5Stair1.SetActive(true);
                break;
            case 1:
                floor5Stair2.SetActive(true);
                break;
            case 2:
                floor5Stair3.SetActive(true);
                break;
        }

        stair = Random.Range(0, 2);
        switch (stair)
        {
            case 0:
                floor6Stair1.SetActive(true);
                break;
            case 1:
                floor6Stair2.SetActive(true);
                break;
        }

        stair = Random.Range(0, 2);
        switch (stair)
        {
            case 0:
                floor7Stair1.SetActive(true);
                break;
            case 1:
                floor7Stair2.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
