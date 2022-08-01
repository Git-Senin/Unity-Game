using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;

    private float horizontalMove;
    private float verticalMove;
    private bool faceRight = true; 
    public float moveSpeed = 1f;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove));
    }

    void FixedUpdate () 
    {
        body.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
    }

    public void Flip()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }
}

