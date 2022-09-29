using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickup : MonoBehaviour
{
    private enum _typeOfPickup { health, grenade, damage, rocket }
    [SerializeField] private _typeOfPickup _type;
    [SerializeField] private int _value;
    [SerializeField] private GameObject _mesh;
    public GameObject Mesh { get { return _mesh; } }
    [SerializeField] private string _name;
    public string PickupName { get { return _name; } }
    private string _locationName;
    private int _transformIndex;
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayer player = other.GetComponent<ActivePlayer>();
        if(player != null)
        {
            switch (_type)
            {
                case _typeOfPickup.health:
                    player.PlayerHealth.AddHealth(_value);
                    AudioManager.AudioInstance().PlaySound("Health");
                    break;

                case _typeOfPickup.damage:
                    player.WeaponHolder.IncreaseDamage(_value);
                    break;

                case _typeOfPickup.grenade:
                    player.WeaponHolder.AddGrenades();
                    break;

                case _typeOfPickup.rocket:
                    player.WeaponHolder.AddWeapon("Rocket");
                    break;

                default:
                    break;
            }
            PickupManager.GetInstance().InvokePickup(transform.position, _locationName);
            Destroy(gameObject);
        }
    }
    public void GetName(string name)
    {
        _locationName = name;
    }
    public void SetIndex(int index)
    {
        _transformIndex = index;
    }
}
