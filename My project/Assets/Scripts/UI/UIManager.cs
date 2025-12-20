using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIState state;
    public UIState State
    {
        get => state;
        set
        {
            if (state == value) return;

            state = value;
            UpdateUI();
        }
    }

    private Dictionary<UIState, GameObject> stateDictionary;
    public List<UIStateElementPair> stateUIElementsPairs;
    // TODO: Pass last message

    private void Awake()
    {
        stateDictionary = new Dictionary<UIState, GameObject>();

        foreach (var pair in stateUIElementsPairs)
        {
            if (!stateDictionary.ContainsKey(pair.state))
            {
                stateDictionary.Add(pair.state, pair.uiElement);
            }
        }
    }

    private void Start()
    {
        State = UIState.Crosshair;
    }

    private void UpdateUI()
    {
        foreach (var kvp in stateDictionary)
        {
            kvp.Value.SetActive(kvp.Key == State);
        }
    }
}
