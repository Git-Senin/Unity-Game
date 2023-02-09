using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private FloatVariable mana;
    [SerializeField] private GameObject bulletPrefab;
    public Transform weaponObject;
    private InputAction fire;

    private void Shoot() 
    {
        if (mana.value > 0f)
        {
            Instantiate(bulletPrefab, transform.position, weaponObject.rotation);
            mana.value--;
        }  
    }

    public void flipX()
    {
        transform.Rotate(180, 0, 0);
    }


}

