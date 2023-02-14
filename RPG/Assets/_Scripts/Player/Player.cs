using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using enums;
using System.Linq;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    [SerializeField] private FloatVariable _health;
    [SerializeField] private FloatVariable _maxHealth;
    [SerializeField] private FloatVariable _mana;
    [SerializeField] private FloatVariable _maxMana;
    [SerializeField] private FloatVariable _exp;
    [SerializeField] private FloatVariable _damage;

    #region Stats 
    /*
        health
        maxHealth
        mana
        maxMana
        exp
        damage
    */
    public float health 
    { 
        get { return _health.value; } 
        private set { _health.value = Mathf.Clamp(value, 0, maxHealth); } 
    }
    public float maxHealth 
    { 
        get { return _maxHealth.value; } 
        private set { _maxHealth.value = value; } 
    }

    public float mana 
    {
        get { return _mana.value; }
        private set { _mana.value = Mathf.Clamp(value, 0, maxMana); }
    }
    public float maxMana
    {
        get { return _maxMana.value; }
        private set { _maxMana.value = value; }
    }

    public float exp 
    {
        get { return _exp.value; }
        private set { _exp.value = Mathf.Clamp(value, 0, 999999999); }
    }

    public float damage
    {
        get { return _damage.value; }
        private set { _damage.value = Mathf.Clamp(value, 0, 999999999); }
    }
    #endregion 

    private CircleCollider2D interactRange;
    private InputAction interact;
    private float manaRegenMultiplier = 1f;

    public NPC selectedNPC { get; private set; }
    public List<NPC> selectedNPCs { get; private set; }
    public bool dead = false;

    #region Unity Methods
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        interactRange = GetComponent<CircleCollider2D>();
        selectedNPCs = new List<NPC>();
    }
    private void Start()
    {
        //SetPlayerStats();
        interact = PlayerController.instance.interract;
    }
    private void Update()
    {
        RegenerateMana();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTrigger2D(collision, Handle.Enter);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HandleTrigger2D(collision, Handle.Exit);
    }

    private void HandleTrigger2D(Collider2D collision, Handle option)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            NPC npc = collision.GetComponent<NPC>();
            switch (option)
            {
                case Handle.Enter:
                    selectedNPC = npc;
                    foreach (NPC _npc in selectedNPCs)
                        _npc.EnableOutline(false);
                    selectedNPCs.Add(npc);
                    npc.EnableOutline(true);
                    interact.performed += UIManager.instance.DialogueBox.Interact;
                    break;

                case Handle.Exit:
                    npc.EnableOutline(false);
                    interact.performed -= UIManager.instance.DialogueBox.Interact;
                    break;

                default:
                    Debug.Log(collision.name + " cannot handle Trigger option.");
                    break;
            }
        }
    }
    #endregion 

    private bool CheckDeath()
    {
        if (maxHealth > 0)
            return false;
        else
            return true;
    }
    private void Die() 
    {
        Debug.Log("You died.");
    }

    public void gainHealth(float hp)
    {
        maxHealth += hp;
    }
    public void TakeDamage(float dmg)
    {
        Debug.Log(_damage + " damage taken. ");
        maxHealth -= dmg;
        if(CheckDeath())
            Die();
    }
    public IEnumerator TakeDamageOverTime(float dmg, float duration, float tickSpeed, bool canDie)
    {
        // Ticks
        while (duration != 0)
        {
            if (health - dmg < 1)
            {
                // Clamp to 1 on no die
                if (!canDie)
                    health = 1;
                else
                {
                    health -= dmg;
                    Die();
                }
            }
            yield return new WaitForSeconds(tickSpeed);
            duration--;
        }
        yield return null;
    }
    public void RegenerateMana()
    {
        if (mana < maxMana)
        {
            mana += (Time.deltaTime * manaRegenMultiplier);
        }
    }
}
