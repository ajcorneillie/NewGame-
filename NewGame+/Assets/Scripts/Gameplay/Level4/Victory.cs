using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] GameObject VictoryText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VictoryText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            VictoryText.SetActive(true);
        }
    }
}
