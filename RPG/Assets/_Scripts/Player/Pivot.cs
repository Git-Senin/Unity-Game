using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Pivot : MonoBehaviour
{
    public Camera cam;
    public Weapon weapon;
    public PlayerMovement player;

    private InputAction look;
    private bool faceRight = true;
    private float mouseAngle;

    private void Start()
    {
        look = PlayerController.instance.look;
        look.performed += FollowMousePosition;
    }
    private void OnEnable()
    {
        if (look != null) look.performed += FollowMousePosition;
    }
    private void OnDisable()
    {
        look.performed -= FollowMousePosition;
    }

    private void FollowMousePosition(InputAction.CallbackContext context)
    {
        // Get Mouse position on screen
        // Get Pivot position on screen
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();
        Vector3 objectPos = cam.WorldToScreenPoint(transform.position);

        //  Slope of X
        //  Slope of Y
        mouseScreenPosition.x = mouseScreenPosition.x - objectPos.x;
        mouseScreenPosition.y = mouseScreenPosition.y - objectPos.y;

        // Angle Using Adjacent & Opposite for angle 
        mouseAngle = Mathf.Atan2(mouseScreenPosition.y, mouseScreenPosition.x) * Mathf.Rad2Deg;

        //Debug.Log(mouseAngle);

        // Rotate Pivot
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, mouseAngle));
        
        // -90 -> 90
        if (mouseAngle >= 0f && mouseAngle < 90f || mouseAngle > -90 && mouseAngle < 0f)    
        {
            if (!faceRight)         //If Left
            {
                faceRight = true;
                weapon.flipX();         //  Flip Weapon X
                player.Flip();
            }
        }
        else
        {
            if (faceRight)          //if Right
            {
                faceRight = false;
                weapon.flipX();
                player.Flip();
            }
        }
    }
}
