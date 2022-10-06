using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickup : MonoBehaviour
{
    [SerializeField] private _typeOfPickup _type;
    [SerializeField] private int _value;
    private enum _typeOfPickup { health, grenade, damage, rocket }
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayer player = other.GetComponent<ActivePlayer>();
        if(player != null)
        {
            switch (_type)
            {
                case _typeOfPickup.health:
                    player.PlayerHealth.AddHealth(_value);
                    AudioManager.Instance.PlaySound("Health");
                    break;

                case _typeOfPickup.damage:
                    player.WeaponHolder.IncreaseDamage(_value);
                    AudioManager.Instance.PlaySound("DamagePick");
                    break;

                case _typeOfPickup.grenade:
                    player.WeaponHolder.AddGrenades();
                    AudioManager.Instance.PlaySound("GrenadePick");
                    break;

                case _typeOfPickup.rocket:
                    player.WeaponHolder.AddWeapon("Rocket");
                    AudioManager.Instance.PlaySound("RocketPick");
                    break;

                default:
                    break;
            }
            PickupManager.GetInstance.InvokePickup(transform.position);
            Destroy(gameObject);
        }
    }
}
