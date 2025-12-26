using System;
using System.Collections.Generic;
using UnityEngine;

public class GunUIManager : MonoBehaviour
{
    public GameObject Selector;

    [System.Serializable]
    public struct GunPositionData
    {
        public string gunName;
        public float xLocation;
    }

    public List<GunPositionData> gunUiPositions;
    private Dictionary<string, float> _positionLookup = new Dictionary<string, float>();

    void Awake()
    {
        foreach (var data in gunUiPositions)
        {
            if (!_positionLookup.ContainsKey(data.gunName))
            {
                _positionLookup.Add(data.gunName, data.xLocation);
            }
        }
    }

    public void UpdateGunUI(Gun currentGun)
    {
        string gunType = currentGun.GetType().Name;

        if (_positionLookup.ContainsKey(gunType))
        {
            Vector3 selectorPos = Selector.transform.localPosition;

            selectorPos.x = _positionLookup[gunType];
            Selector.transform.localPosition = selectorPos;
        }
        else
        {
            Debug.LogWarning($"UI: No X-coordinate found in Inspector list for: {gunType}");
        }
    }
}