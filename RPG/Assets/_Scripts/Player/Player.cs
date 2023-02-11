using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public NPCData selectedNPC { get; private set; }
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
    }
    private void Start()
    {
        //SetPlayerStats();
        interact = PlayerController.instance.interract;
    }
    private void Update()
    {
        RegenerateMana();

        // Update Float Var Every Frame
        // SetVariableStats();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            selectedNPC = collision.GetComponent<NPC>().data;
            collision.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, Color.red, 0);
            interact.performed += UIManager.instance.DialogueBox.Interact;
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            collision.GetComponent<SpriteRenderer>().color = Color.white;
            interact.performed -= UIManager.instance.DialogueBox.Interact;
        }
    }
    #endregion 

    private void SetPlayerStats()
    {
        // Sets from Float Variables
        health = _health.value;
        maxHealth = _maxHealth.value;
        mana = _mana.value;
        maxMana = _maxMana.value;
        exp = _exp.value;
        damage = _damage.value;
    }
    private void SetVariableStats()
    {
        // Sets from Player Stats
        _health.value = health;
        _maxHealth.value = maxHealth;
        _mana.value = mana;
        _maxMana.value = maxMana;
        _exp.value = exp;
        _damage.value = damage;
    }
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
