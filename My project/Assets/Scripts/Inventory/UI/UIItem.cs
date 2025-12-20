using TMPro;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public string Name;
    public TextMeshProUGUI NameText;

    void Start()
    {
        NameText.text = Name;
    }
}
