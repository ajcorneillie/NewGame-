using UnityEngine;

[CreateAssetMenu(fileName = "Level1Rooms", menuName = "Scriptable Objects/Level1Rooms")]
public class Level1Rooms : ScriptableObject
{
    public int northDoorLevel;
    public int southDoorLevel;

    public bool hasKey;
    public int keyLevel;
    public Level1Keys KeyScriptable;
}
