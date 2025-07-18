using UnityEngine;


/// <summary>
/// enum for the event references for Gameplay events
/// </summary>

public enum GameplayEvent
{
    PickupItemAttempt,
    DropItem,
    UseItem,
    Running,
    PickUpSuccess,
    StaminaIncrease,
    HealthUpdate,
    StunEnd,
    StunStart,

}

public enum GameplayEventData
{
    Item,
    Player,
    ItemIcon,
    ItemName,
    stamina,
    health,
    Time,
}
