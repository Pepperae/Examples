using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Treasure : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject chestGameObjectA;
    [SerializeField] private GameObject chestAnimator;

    private IInteractable chestA;
    private IInteractable chestB;
    
    public bool activeCollision = false;

    [Tooltip("Check for whether the chest is already opened or not.")]
    public bool isOpen = false;
    [Tooltip("Add: Open Chest")] public GameObject openChest;
    [Tooltip("Add: Closed Chest")] public GameObject closedChest;
    [Tooltip("Add: Item Acquired Panel")] public GameObject pup;
    [Tooltip("Add: Text: Item Name")] public TextMeshProUGUI dialogText;
    [Tooltip("Add: Player")] public PlayerMovement player;
    [Tooltip("Add: Player Inventory Scriptable")] public Inventory playerInventory;
    [Tooltip("Add: Item Scriptable for static loot.")] public Item contents;
    [Tooltip("Add: ReceivedItemSprite (Player Child)")] public SpriteRenderer receivedItemSprite;

    void Start()
    {
        UnopenedChest();
    }

    private void Awake()
    {
        chestA = chestGameObjectA.GetComponent<IInteractable>();
    }

    private void Update()
    {
        if (activeCollision)
        {
            // If the Chest is closed, Pup is not active and player presses F -> Open Chest
            if (!isOpen && !pup.activeInHierarchy && (Input.GetKeyDown(KeyCode.F)))
            {
                //Debug.Log("Enabled Chest");
                SetInteractableActive();
            }
            // If the Chest is open, Pup is active and player presses F -> Close Chest
            else if (isOpen && pup.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Disabled Chest");
                SetInteractableInactive();
            }
            // If the Chest is open, Pup is not active and player presses F -> Do nothing
            else if (isOpen && !pup.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Chest is already open.");
                return;
            }
        }
    }

    public void SetInteractableActive()
    {
        // Add random loot generator.
        // Set random loot generator to pop out its value and then turn it into a Sprite.

        // Swap the chest sprites from closed to open.
        closedChest.SetActive(false);
        openChest.SetActive(true);

        // Turn on Pop-up.
        pup.SetActive(true);

        // Set item's description as Dialog's text.
        dialogText.text = contents.itemDescription;

        // Add loot to inventory.
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        player.GainItem();

        // Set the chest to having been opened.
        isOpen = true;
    }

    public void SetInteractableInactive()
    {
        // Turn off Pop-up.
        pup.SetActive(false);

        // Set loot to empty.
        playerInventory.currentItem = null;

        // Resume player movement.
        player.currentState = PlayerState.walk;

        // Keep notice bubble turned off on return
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void UnopenedChest()
    {
        openChest.SetActive(false);
        closedChest.SetActive(true);
        pup.SetActive(false);
    }

    public void ToggleInteractable()
    {
        // Can only be opened once.
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeCollision = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeCollision = false;
        }
    }
}
