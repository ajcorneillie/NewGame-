using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

    public bool has1Camera;

    int maxNum = 100;
    int chanceNum = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
