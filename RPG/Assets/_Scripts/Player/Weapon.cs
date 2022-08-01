using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private FloatVariable mana;

    public Transform weaponObject;
    public GameObject bulletPrefab;

    void Update()
    {
        // Primary
        if (Input.GetButtonDown("Fire1") && mana.value > 0) {
            mana.value--;
            Shoot();
        }
    }

    void Shoot () 
    {
        Instantiate(bulletPrefab, transform.position, weaponObject.rotation);
    }

    public void flipX()
    {
        transform.Rotate(180, 0, 0);
    }
}

