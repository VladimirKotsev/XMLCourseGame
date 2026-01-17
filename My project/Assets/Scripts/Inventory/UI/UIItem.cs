using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Name;
    public string Description;
    public TextMeshProUGUI NameText;

    [Header("Tooltip")]
    public GameObject Tooltip;
    public TextMeshProUGUI TooltipText;

    private void Start()
    {
        NameText.text = Name;
        TooltipText.text = Description;
        Tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.SetActive(false);
    }
}
