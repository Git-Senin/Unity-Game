using System.Collections;
using System.Collections.Generic;
using Ink;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    [Header("Cache")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;

    private Vector2 moveDirection = Vector2.zero; 
    private bool faceRight = true;

    [Header("Stats")]
    public float moveSpeed = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        moveDirection = PlayerController.instance.move.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));
    }
    void FixedUpdate () 
    {
        body.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

}

