using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Name;
    public string Description;
    public string IconName;

    public TextMeshProUGUI NameText;
    public Image IconImage;
    public Sprite DefaultIcon;

    [Header("Tooltip")]
    public GameObject Tooltip;
    public TextMeshProUGUI TooltipText;

    private void Start()
    {
        UpdateMetaData();
    }

    public void UpdateMetaData() 
    {
        NameText.text = Name;
        TooltipText.text = Description;
        Tooltip.SetActive(false);

        LoadIcon();
    }

    private void LoadIcon()
    {
        Sprite icon = Resources.Load<Sprite>($"ItemIcons/{IconName}");

        if (icon != null)
        {
            IconImage.sprite = icon;
        }
        else
        {
            IconImage.sprite = DefaultIcon;
        }
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
