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

    private void OnEnable()
    {
        look = PlayerController.instance.look;
        look.performed += FollowMousePosition;
    }
    private void OnDisable()
    {
        look.performed -= FollowMousePosition;
    }

    private void FollowMousePosition(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();     // Get Mouse position on screen
        Vector3 objectPos = cam.WorldToScreenPoint(transform.position); // Get Pivot position on screen

        mouseScreenPosition.x = mouseScreenPosition.x - objectPos.x;  //  Slope of X
        mouseScreenPosition.y = mouseScreenPosition.y - objectPos.y;  //  Slope of Y

        mouseAngle = Mathf.Atan2(mouseScreenPosition.y, mouseScreenPosition.x) * Mathf.Rad2Deg;    // Angle Using Adjacent & Opposite for angle 

        //Debug.Log(mouseAngle);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, mouseAngle));   // Rotate Pivot
        
        if (mouseAngle > 0f && mouseAngle < 90f || mouseAngle > -90 && mouseAngle < 0f)    // -90 -> 90
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
