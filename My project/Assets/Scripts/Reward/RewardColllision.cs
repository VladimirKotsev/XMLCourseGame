using System.Linq;
using UnityEngine;

public class RewardColllision : MonoBehaviour
{
    private UIManager uiManager;
    private InventoryManager inventoryManager;
    private InventoryUI inventoryUI;
    private GameManager gameManager;

    private bool playerNearInteractable = false;
    private bool isTaken = false;

    // TODO: Load item from xml
    public InventoryItem item = new InventoryItem { Name = "Change me"};
    public AudioSource fallAudio;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        inventoryUI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<InventoryUI>();
        gameManager = GameObject.FindGameObjectWithTag("GameLoader").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            fallAudio.Play();
        }
        if (other.CompareTag("Player"))
        {
            playerNearInteractable = true;
            uiManager.State = UIState.NearInteractable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearInteractable = false;
            uiManager.State = UIState.Crosshair;
        }
    }

    private void Update()
    {
        if (playerNearInteractable) 
        {
            if (uiManager.State == UIState.NearInteractable && Input.GetKeyDown(KeyCode.E))
            {
                uiManager.State = UIState.Interacting;
                this.UpdateItems();
                if (!this.isTaken) 
                {
                    this.inventoryUI.ShowItem();
                }
            }
            else if (uiManager.State == UIState.Interacting && Input.GetKeyDown(KeyCode.E))
            {
                if (isTaken)
                {
                    this.inventoryManager.RemoveItem(this.item.Name);
                    this.inventoryUI.ShowItem();
                    isTaken = false;
                }
                else 
                {
                    this.inventoryManager.AddItem(this.item);
                    this.inventoryUI.HideItem();
                    isTaken = true;
                    this.gameManager.CheckForLevelCompletion(this.inventoryManager.GetAll().Count());
                }
                this.UpdateItems();
            }
        }
    }

    private void UpdateItems() 
    {
        inventoryUI.ClearItems();
        inventoryUI.RenderItems();
    }
}
