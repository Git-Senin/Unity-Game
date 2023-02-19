using System.Collections;
using System.Collections.Generic;
using Ink;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }
    private delegate IEnumerator MovementAbility();

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D body;

    private MovementAbility movementAbility;
    private InputAction movementAbilityAction;
    private Vector2 moveDirection = Vector2.zero;
    private bool movementAbilityReady;
    private bool faceRight = true;
    public float movementAbilityCooldown { get; private set; }
    public float moveSpeed = 1f;
    public float dashForce = 50f;

    // For Testing Remove Later
    [SerializeField] GameObject abilitySlotPrefab;
    // -------------------------------------
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        SetMovementAbility("dash");
    }
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        movementAbilityAction = PlayerController.instance.movementAbility;
        movementAbilityAction.performed += MovementDash;
        movementAbilityCooldown = 3;
        movementAbilityReady = true;
    }
    private void OnEnable()
    {
        if(movementAbilityAction != null )
            movementAbilityAction.performed += MovementDash;
    }
    private void OnDisable()
    {
        movementAbilityAction.performed -= MovementDash;
    }
    private void Update() 
    {
        moveDirection = PlayerController.instance.move.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));
    }
    private void FixedUpdate () 
    {
        body.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Impulse);
    }

    private void MovementDash(InputAction.CallbackContext context)
    {
        StartCoroutine(movementAbility());
    }
    private IEnumerator Dash()
    {
        // Force
        Vector2 force = moveDirection.normalized * dashForce;

        // Not moving
        if (force.Equals(Vector2.zero)) 
            yield break;

        // Not ready
        if (!movementAbilityReady)
        {
            StartCoroutine(UIManager.instance.Alert.Announce("Dash Not Ready"));
            yield break;
        }

        // Dash
        body.AddForce(force, ForceMode2D.Impulse);

        // UI and Cooldown
        movementAbilityReady = false;
        GameObject dashSlotObject = Instantiate(abilitySlotPrefab);
        AbilitySlot dashSlot = dashSlotObject.GetComponent<AbilitySlot>();
        float _movementAbilityCooldown = movementAbilityCooldown;
        while (movementAbilityCooldown > 0)
        {
            dashSlot.SetFill(movementAbilityCooldown, _movementAbilityCooldown);
            dashSlot.SetCooldown(movementAbilityCooldown);
            yield return new WaitForSeconds(1);
            movementAbilityCooldown -= 1;
        }
        Destroy(dashSlotObject);
        movementAbilityCooldown = _movementAbilityCooldown;
        movementAbilityReady = true;
    }
    private IEnumerator Blink()
    {
        Debug.Log("Implement Blink Movement Ability!");
        yield return null;
    }

    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public void SetMovementAbility(string ability)
    {
        ability = ability.ToLower();
        switch (ability)
        {
            case "dash":
                movementAbility = Dash;
                break;

            case "blink":
                movementAbility = Blink;
                break;

            default:
                Debug.Log($"Movement Ability \"{ability}\" Not Found");
                break;
        }
    }
}

