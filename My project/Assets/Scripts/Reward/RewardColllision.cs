using UnityEngine;

public class RewardColllision : MonoBehaviour
{
    private UIManager uiManager;
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.State = UIState.NearInteractable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.State = UIState.Crosshair;
        }
    }
}
