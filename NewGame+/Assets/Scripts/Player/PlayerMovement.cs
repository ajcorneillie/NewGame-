using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject pickUpMessage;
    [SerializeField] public GameObject openDoorMessage;
    [SerializeField] private float maxRange = 2f;

    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;
    [SerializeField] GameObject slot3;
    [SerializeField] GameObject slot4;
    [SerializeField] GameObject slot5;
    [SerializeField] GameObject slot6;

    [SerializeField] GameObject visionVirus;
    [SerializeField] GameObject rebootKit;
    [SerializeField] GameObject healingJuice;
    [SerializeField] GameObject staminaBatteries;
    [SerializeField] GameObject stunGrenade;
    [SerializeField] GameObject redKeycard;
    [SerializeField] GameObject orangeKeycard;
    [SerializeField] GameObject yellowKeycard;
    [SerializeField] GameObject greenKeycard;
    [SerializeField] GameObject blueKeycard;
    [SerializeField] GameObject purpleKeycard;
    [SerializeField] GameObject pinkKeycard;
    [SerializeField] GameObject omniKeycard;

    [SerializeField] GameObject visionVirusPickUp;
    [SerializeField] GameObject rebootKitPickUp;
    [SerializeField] GameObject healingJuicePickUp;
    [SerializeField] GameObject staminaBatteriesPickUp;
    [SerializeField] GameObject stunGrenadePickUp;
    [SerializeField] GameObject redKeycardPickUp;
    [SerializeField] GameObject orangeKeycardPickUp;
    [SerializeField] GameObject yellowKeycardPickUp;
    [SerializeField] GameObject greenKeycardPickUp;
    [SerializeField] GameObject blueKeycardPickUp;
    [SerializeField] GameObject purpleKeycardPickUp;
    [SerializeField] GameObject pinkKeycardPickUp;
    [SerializeField] GameObject omniKeycardPickUp;

    [SerializeField] GameObject player;

    [SerializeField] GameObject blindingLight;
    
    [SerializeField] GameObject inventoryManager;

    [SerializeField] private LayerMask targetLayers;

    [SerializeField] private int currentItemIndex = 0;
    [SerializeField] private int totalItems = 0;

    GameObject currentObject;

    bool isRedLight = false;
    public bool isSupplier = false;

    List<GameObject> slots = new List<GameObject>();
    List<GameObject> itemTypes = new List<GameObject>();
    List<GameObject> itemTypesDropped = new List<GameObject>();

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 0.5f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    public LayerMask obstacleMask;

    float noVisionTime;
    public bool canMove = true;
    public bool noVision = false;
    public bool staminaLock = false;
    bool isRunning = false;
    public bool isStunned;
    public bool canStand;

    GameEvent Running = new GameEvent();
    GameEvent pickUpAttempt = new GameEvent();
    GameEvent stimUsed = new GameEvent();
    GameEvent Stun = new GameEvent();
    

    void Start()
    {
        
        EventManager.AddInvoker(GameplayEvent.HealthUpdate, stimUsed);
        EventManager.AddInvoker(GameplayEvent.StunStart, Stun);
        

        itemTypes.Add(rebootKit);
        itemTypes.Add(visionVirus);
        itemTypes.Add(stunGrenade);
        itemTypes.Add(staminaBatteries);
        itemTypes.Add(healingJuice);
        itemTypes.Add(redKeycard);
        itemTypes.Add(orangeKeycard);
        itemTypes.Add(yellowKeycard);
        itemTypes.Add(greenKeycard);
        itemTypes.Add(blueKeycard);
        itemTypes.Add(purpleKeycard);
        itemTypes.Add(pinkKeycard);
        itemTypes.Add(omniKeycard);

        itemTypesDropped.Add(rebootKitPickUp);
        itemTypesDropped.Add(visionVirusPickUp);
        itemTypesDropped.Add(stunGrenadePickUp);
        itemTypesDropped.Add(staminaBatteriesPickUp);
        itemTypesDropped.Add(healingJuicePickUp);
        itemTypesDropped.Add(redKeycardPickUp);
        itemTypesDropped.Add(orangeKeycardPickUp);
        itemTypesDropped.Add(yellowKeycardPickUp);
        itemTypesDropped.Add(greenKeycardPickUp);
        itemTypesDropped.Add(blueKeycardPickUp);
        itemTypesDropped.Add(purpleKeycardPickUp);
        itemTypesDropped.Add(pinkKeycardPickUp);
        itemTypesDropped.Add(omniKeycardPickUp);

        foreach (GameObject item in itemTypes)
        {
            item.SetActive(false);
        }

        EventManager.AddListener(GameplayEvent.StunStart, StartStun);
        EventManager.AddListener(GameplayEvent.StunEnd, EndStun);
        EventManager.AddListener(GameplayEvent.VisionActivate, VisionBlockActivate);
        EventManager.AddInvoker(GameplayEvent.Running,Running);
        EventManager.AddInvoker(GameplayEvent.PickupItemAttempt, pickUpAttempt);
        EventManager.AddListener(GameplayEvent.UseItem, ItemUsed);
        EventManager.AddListener(GameplayEvent.RedLight, RedLight);
        EventManager.AddListener(GameplayEvent.GreenLight, GreenLight);

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        slots.Add(slot1);
        slots.Add(slot2);
        slots.Add(slot3);

        slot1.GetComponent<Slot>().myBorder.SetActive(true);
        currentObject = slot1;
        slot2.GetComponent<Slot>().myBorder.SetActive(false);
        slot3.GetComponent<Slot>().myBorder.SetActive(false);
        slot4.GetComponent<Slot>().myBorder.SetActive(false);
        slot5.GetComponent<Slot>().myBorder.SetActive(false);
        slot6.GetComponent<Slot>().myBorder.SetActive(false);

        if (isSupplier == true)
        {
            totalItems = 6;
            slots.Add(slot4);
            slots.Add(slot5);
            slots.Add(slot6);
        }
        else
        {
            totalItems = 3;
            Destroy(slot4);
            Destroy(slot5);
            Destroy(slot6);
        }

        inventoryManager.GetComponent<InventoryManager>().InitializeMe(slots);
        UpdateInventorySelection();
        blindingLight.SetActive(false);
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        if (staminaLock == false)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            isRunning = false;
        }

        if (isRunning == true && canMove)
        {
            Running.AddData(GameplayEventData.stamina, 3f);
            Running.AddData(GameplayEventData.Player, gameObject);
            Running.Invoke(Running.Data);
        }

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && staminaLock == false)
        {
            moveDirection.y = jumpPower;
            Running.AddData(GameplayEventData.stamina, 100f);
            Running.AddData(GameplayEventData.Player, gameObject);
            Running.Invoke(Running.Data);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.C) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
            Running.AddData(GameplayEventData.stamina, -1f);
            Running.AddData(GameplayEventData.Player, gameObject);
            Running.Invoke(Running.Data);
        }
        else
        {
            Vector3 bottom = transform.position + Vector3.up * crouchHeight;
            Vector3 top = transform.position + Vector3.up * defaultHeight;

            // Check for obstacles between crouching height and standing height
            if (!Physics.CheckCapsule(bottom, top, characterController.radius, obstacleMask))
            {
                characterController.height = defaultHeight;
                walkSpeed = 6f;
                runSpeed = 12f;
            }

        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRange, targetLayers) == true)
        {
            Debug.Log("You're pointing at: " + hit.collider.name);

            if (hit.collider.gameObject.CompareTag("Door"))
            {
                openDoorMessage.SetActive(true);
            }
            else
            {
                pickUpMessage.SetActive(true);
            }
            
            if (Input.GetKey(KeyCode.E) && hit.collider.gameObject.CompareTag("Door") == false)
            {
                
                pickUpAttempt.AddData(GameplayEventData.Item, hit.collider.gameObject);
                pickUpAttempt.Invoke(pickUpAttempt.Data);
                UpdateInventorySelection();
            }
        }
        else
        {
            pickUpMessage.SetActive(false);
            openDoorMessage.SetActive(false);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll < 0f && canMove)
        {
            // Scroll up
            currentItemIndex++;
            if (currentItemIndex >= totalItems)
                currentItemIndex = 0;
            UpdateInventorySelection();
        }
        else if (scroll > 0f && canMove)
        {
            // Scroll down
            currentItemIndex--;
            if (currentItemIndex < 0)
                currentItemIndex = totalItems - 1;
            UpdateInventorySelection();
        }

        if (Input.GetKey(KeyCode.Q) && canMove)
        {
            if (currentObject.GetComponent<Slot>().myObject != null)
            {
                foreach (GameObject item in itemTypes)
                {
                    if (item.GetComponent<ItemUse>().name == currentObject.GetComponent<Slot>().myObject.name)
                    {
                        currentObject.GetComponent<Slot>().myObject = null;
                        currentObject.GetComponent<Slot>().myIcon.color = Color.white;
                        currentObject.GetComponent<Slot>().isFull = false;
                        int currentCount = itemTypes.IndexOf(item);
                        GameObject droppedItem = itemTypesDropped[currentCount];
                        Vector3 spawnPosition = transform.position + transform.forward * 1f;
                        Instantiate(droppedItem, spawnPosition, Quaternion.identity);
                        UpdateInventorySelection();
                        break;
                    }
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            stimUsed.AddData(GameplayEventData.health, -15f);
            stimUsed.AddData(GameplayEventData.Player, gameObject);
            stimUsed.Invoke(stimUsed.Data);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Stun.AddData(GameplayEventData.Time, 5f);
            Stun.AddData(GameplayEventData.Player, gameObject);
            Stun.Invoke(Stun.Data);
        }

        if (noVision == true)
        {
            if (noVisionTime <= 0)
            {
                noVision = false;
                blindingLight.SetActive(false);
            }
            noVisionTime--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItemIndex = 0;
            UpdateInventorySelection();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItemIndex = 1;
            UpdateInventorySelection();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItemIndex = 2;
            UpdateInventorySelection();
        }
        if (isSupplier)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentItemIndex = 3;
                UpdateInventorySelection();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentItemIndex = 4;
                UpdateInventorySelection();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                currentItemIndex = 5;
                UpdateInventorySelection();
            }
        }

        if (isRedLight == true && Input.anyKey)
        {
            stimUsed.AddData(GameplayEventData.health, -0.1f);
            stimUsed.AddData(GameplayEventData.Player, gameObject);
            stimUsed.Invoke(stimUsed.Data);
        }
    }

    void UpdateInventorySelection()
    {
        if (currentObject != null)
        {
            currentObject.GetComponent<Slot>().myBorder.SetActive(false);
        }

        Debug.Log("Current item index: " + currentItemIndex);
        currentObject = slots[currentItemIndex];
        currentObject.GetComponent<Slot>().myBorder.SetActive(true);

        if (currentObject.GetComponent<Slot>().myObject != null)
        {
            foreach (GameObject item in itemTypes)
            {
                if (item.GetComponent<ItemUse>().name == currentObject.GetComponent<Slot>().myObject.name)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject item in itemTypes)
            {
                item.SetActive(false);
            }
        }
    }

    void ItemUsed(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Item, out object output);
        string itemName = (string)output;

        foreach (GameObject item in itemTypes)
        {
            if (item.GetComponent<ItemUse>().name == itemName)
            {
                currentObject.GetComponent<Slot>().myObject = null;
                currentObject.GetComponent<Slot>().myIcon.color = Color.white;
                currentObject.GetComponent<Slot>().isFull = false;
                UpdateInventorySelection();
                break;
            }
        }

    }

    void StartStun(Dictionary<System.Enum, object> data)
    {
        canMove = false;
    }

    void EndStun(Dictionary<System.Enum, object> data)
    {
        canMove = true;
    }
    void VisionBlockActivate(Dictionary<System.Enum, object> data)
    {
        data.TryGetValue(GameplayEventData.Time, out object output);
        float time = (float)output;
        data.TryGetValue(GameplayEventData.Collision, out output);
        GameObject collision = (GameObject)output;

        if (collision == player)
        {
            noVision = true;
            noVisionTime = time * 100f;
            blindingLight.SetActive(true);
        }
    }

    void RedLight(Dictionary<System.Enum, object> data)
    {
        isRedLight = true;
    }

    void GreenLight(Dictionary<System.Enum, object> data)
    {
        isRedLight = false;
    }

}
