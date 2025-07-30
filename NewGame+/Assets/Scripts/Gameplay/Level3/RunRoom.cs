using UnityEngine;

public class RunRoom : MonoBehaviour
{


    [SerializeField] public GameObject forwardPath;
    [SerializeField] public GameObject leftPath;
    [SerializeField] public GameObject rightPath;

    bool isCorrectPath;

    int numOfObstacles;
    [SerializeField] GameObject obstacle1;
    [SerializeField] GameObject obstacle2;
    [SerializeField] GameObject obstacle3;
    [SerializeField] GameObject obstacle4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numOfObstacles = Random.Range(0, 10);
        if (numOfObstacles < 3)
        {
            obstacle1.SetActive(false);
            obstacle2.SetActive(false);
        }
        else if (numOfObstacles < 7)
        {
            obstacle1.SetActive(false);
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
