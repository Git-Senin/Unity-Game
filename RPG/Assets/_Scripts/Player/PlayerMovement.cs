using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    [SerializeField] GameObject pivot;

    public Rigidbody2D body;
    public Animator animator;
    private float horizontalMove;
    private float verticalMove;
    private bool faceRight = true; 
    public float moveSpeed = 1f;

    public bool frozen = false;

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
        if (frozen) return;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove));
    }

    void FixedUpdate () 
    {
        if (frozen) return;
        body.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
    }

    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Freeze()
    {
        body.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
        pivot.gameObject.SetActive(false);
        frozen = true;
    }

    public void Unfreeze()
    {
        pivot.gameObject.SetActive(true);
        frozen = false;
    }
}

