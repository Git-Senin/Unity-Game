using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private FloatVariable mana;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform weaponObject;

    private InputAction fire;

    private void Start()
    {
        fire = PlayerController.instance.fire;
        fire.performed += Fire;
    }
    private void OnEnable()
    {
        if (fire != null) { fire.performed += Fire; }
    }
    private void OnDisable()
    {
        fire.performed -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Shoot();
    }

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

