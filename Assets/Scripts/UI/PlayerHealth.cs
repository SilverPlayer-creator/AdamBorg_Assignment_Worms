using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private int _index;
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerUnit.OnDamageEvent += UpdateHealth;
        Weapon.OnAmmoChangedEvent += UpdateAmmo;
    }
    void UpdateHealth(int currentHealth, int index)
    {
        //if(index == _index)
        //_healthText.text = currentHealth.ToString();
    }
    void UpdateAmmo(int currentAmmo, int maxAmmo, int index)
    {
        if(index == _index)
        _ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}
