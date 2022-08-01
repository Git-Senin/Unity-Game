using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public Camera cam;
    public Weapon weapon;
    public PlayerMovement player;
    private bool direction;
    private float mouseAngle;
    
    void Start()
    {
        direction = true;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition; //  Gets Mouse Pos Vector

        Vector3 objectPos = cam.WorldToScreenPoint(transform.position); // Gets Object Pos vector

        mousePos.x = mousePos.x - objectPos.x;  //  Slope of X
        mousePos.y = mousePos.y - objectPos.y;  //  Slope of Y

        mouseAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;    // Angle Using Adjacent & Opposite for angle 

        //  Debug.Log(mouseAngle);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, mouseAngle));   // Rotate Pivot

        if (mouseAngle > 0f && mouseAngle < 90f || mouseAngle > -90 && mouseAngle < 0f)    // -90 -> 90
        {
            if (!direction)         //If Left
            {
                direction = true;   
                weapon.flipX();         //  Flip Weapon X
                player.Flip();
            }
        }
        else
        {
            if (direction)          //if Right
            {
                direction = false;
                weapon.flipX();
                player.Flip();
            }
        }
    }

}
