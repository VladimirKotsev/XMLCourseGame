using UnityEngine;

public class RewardColllision : MonoBehaviour
{
    private UIManager uiManager;
    private bool playerNearInteractable = false;

    public AudioSource fallAudio;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
        if (playerNearInteractable && uiManager.State == UIState.NearInteractable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiManager.State = UIState.Interacting;
            }
        }
    }
}
