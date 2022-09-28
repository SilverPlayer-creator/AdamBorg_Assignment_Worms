using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PickupManager _pickupManager;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _pickupManager.OnSpawned += ChangeText;
    }
    private void OnDisable()
    {
        _pickupManager.OnSpawned -= ChangeText;
    }

    public void ChangeText()
    {
       
    }

}
