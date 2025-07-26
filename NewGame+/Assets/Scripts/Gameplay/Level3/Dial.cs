using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public int myNum;
    public int currentNum = 12;

    GameEvent codeActive = new GameEvent();
    GameEvent codeDeactive = new GameEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddInvoker(GameplayEvent.CodeActive, codeActive);
        EventManager.AddInvoker(GameplayEvent.CodeDeactive, codeDeactive);
        EventManager.AddListener(GameplayEvent.TurnDial, TurnDial);
        EventManager.AddListener(GameplayEvent.DialCodes, DialCodes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnDial(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Dial, out object output);
        GameObject dial = (GameObject)output;

        if (dial == gameObject)
        {
            transform.Rotate(0f, 0f, 30f);
            currentNum++;
            if (currentNum > 12)
            {
                currentNum = 1;
            }
            
            if(currentNum == myNum)
            {
                codeActive.AddData(GameplayEventData.Dial, gameObject);
                codeActive.Invoke(codeActive.Data);
            }
            else
            {
                codeDeactive.AddData(GameplayEventData.Dial, gameObject);
                codeDeactive.Invoke(codeDeactive.Data);
            }
        }

    }

    void DialCodes(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.DialCode, out object output);
        int dialCode = (int)output;

        data.TryGetValue(GameplayEventData.Dial, out output);
        GameObject dial = (GameObject)output;

        if (dial == gameObject)
        {
            myNum = dialCode;
        }


        if (myNum == currentNum)
        {
            codeActive.AddData(GameplayEventData.Dial, gameObject);
            codeActive.Invoke(codeActive.Data);
        }
    }
}
