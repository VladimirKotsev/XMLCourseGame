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

    private Dictionary<UIState, GameObject> stateDictionary = new Dictionary<UIState, GameObject>();
    public List<UIStateElementPair> stateUIElementsPairs;

    private void Start()
    {
        foreach (var pair in stateUIElementsPairs)
        {
            if (!stateDictionary.ContainsKey(pair.state))
            {
                stateDictionary.Add(pair.state, pair.uiElement);
            }
        }

        this.State = UIState.StartMenu;
    }

    private void UpdateUI()
    {
        if (stateDictionary.Count == 0) 
            return;

        foreach (var kvp in stateDictionary)
        {
            kvp.Value.SetActive(kvp.Key == State);
        }
    }
}
