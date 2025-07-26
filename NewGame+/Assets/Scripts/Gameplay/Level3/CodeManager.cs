using System.Collections.Generic;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] GameObject dial1;
    [SerializeField] GameObject dial2;
    [SerializeField] GameObject dial3;
    [SerializeField] GameObject dial4;
    [SerializeField] GameObject dial5;

    [SerializeField] GameObject door;
    public bool dial1Active;
    public bool dial2Active;
    public bool dial3Active;
    public bool dial4Active;
    public bool dial5Active;

    [SerializeField] Material code1;
    [SerializeField] Material code2;
    [SerializeField] Material code3;
    [SerializeField] Material code4;
    [SerializeField] Material code5;
    [SerializeField] Material code6;
    [SerializeField] Material code7;
    [SerializeField] Material code8;
    [SerializeField] Material code9;
    [SerializeField] Material code10;
    [SerializeField] Material code11;
    [SerializeField] Material code12;

    float speed = 2f;

    GameEvent makeCode = new GameEvent();
    List<GameObject> dials = new List<GameObject>();
    List <Material> materials = new List<Material>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddListener(GameplayEvent.CodeActive, CodeActive);
        EventManager.AddListener(GameplayEvent.CodeDeactive, CodeDeactive);
        EventManager.AddInvoker(GameplayEvent.DialCodes, makeCode);

        dials.Add(dial1);
        dials.Add(dial2);
        dials.Add(dial3);
        dials.Add(dial4);
        dials.Add(dial5);

        materials.Add(code1);
        materials.Add(code2);
        materials.Add(code3);
        materials.Add(code4);
        materials.Add(code5);
        materials.Add(code6);
        materials.Add(code7);
        materials.Add(code8);
        materials.Add(code9);
        materials.Add(code10);
        materials.Add(code11);
        materials.Add(code12);

        List<Color> colors = new List<Color>();
        colors.Add(Color.red);
        colors.Add(Color.yellow);
        colors.Add(Color.green);
        colors.Add(Color.blue);
        colors.Add(Color.magenta);
        int colorIndex = 0;

        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("Computer");
        List<GameObject> taggedObjects = new List<GameObject>(foundObjects);

        foreach (GameObject dial in dials)
        {
            int randomCode = Random.Range(1, 13);
            makeCode.AddData(GameplayEventData.Dial, dial);
            makeCode.AddData(GameplayEventData.DialCode, randomCode);
            makeCode.Invoke(makeCode.Data);

            GameObject randomObject = taggedObjects[Random.Range(0, taggedObjects.Count)];
            Renderer rend = randomObject.GetComponent<Renderer>();
            rend.material = materials[randomCode - 1];
            rend.material.color = colors[colorIndex];
            colorIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dial1Active == true && dial2Active == true && dial3Active == true && dial4Active == true && dial5Active == true)
        {
            Vector3 targetPosition = new Vector3(door.transform.position.x, door.transform.position.y - 1, door.transform.position.z);
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void CodeDeactive(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Dial, out object output);
        GameObject dial = (GameObject)output;

        if (dial == dial1)
        {
            dial1Active = false;
        }
        if (dial == dial2)
        {
            dial2Active = false;
        }
        if (dial == dial3)
        {
            dial3Active = false;
        }
        if (dial == dial4)
        {
            dial4Active = false;
        }
        if (dial == dial5)
        {
            dial5Active = false;
        }


    }

    void CodeActive(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Dial, out object output);
        GameObject dial = (GameObject)output;

        if (dial == dial1)
        {
            dial1Active = true;
        }
        if (dial == dial2)
        {
            dial2Active = true;
        }
        if (dial == dial3)
        {
            dial3Active = true;
        }
        if (dial == dial4)
        {
            dial4Active = true;
        }
        if (dial == dial5)
        {
            dial5Active = true;
        }
    }
}
