using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using enums;

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

    private InputAction interact;
    private float manaRegenMultiplier = 1f;

    public NPC selectedNPC { get; private set; }
    private List<NPC> selectedNPCs;
    
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
        selectedNPCs = new List<NPC>();
    }
    private void Start()
    {
        interact = PlayerController.instance.interract;
    }
    private void Update()
    {
        RegenerateMana();
    }
    #endregion 

    private bool CheckDeath()
    {
        if (health > 0)
            return false;
        else
            return true;
    }
    private void Die() 
    {
        PlayerController.instance.EnablePlayerController(false);
        PlayerController.instance.SubscribeEvents(false);
        StartCoroutine(WaitSeconds(2));
        Loader.LoadScene(Loader.Scene.Dead);
    }   
    private IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void gainHealth(float hp)
    {
        health += hp;
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
            // Clamp to 1 on no die
            if (!canDie)
                health = 1;
            else
            {
                health -= dmg;
            }
            yield return new WaitForSeconds(tickSpeed);
            duration--;
            if(CheckDeath())
            {
                Debug.Log("Die");
                Die();

            }
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
    public void Select(NPC npc, bool selected = true)
    {
        if (selected)
        {
            foreach (NPC _selectedNPC in selectedNPCs)
                _selectedNPC.EnableOutline(false);

            // Select
            selectedNPC = npc;
            selectedNPCs.Add(npc);
            npc.EnableOutline(true);

            interact.performed += UIManager.instance.DialogueBox.Interact;
        }
        else
        {
            // Deselect
            if (selectedNPCs.Contains(npc))
            {
                selectedNPCs.Remove(npc);
                npc.EnableOutline(false);
            }

            // Not Empty Set Closest NPC
            if (selectedNPCs.Count != 0)
            {
                selectedNPC = selectedNPCs.Last();
                selectedNPC.EnableOutline(false);
            }

            // Empty
            else
            {
                interact.performed -= UIManager.instance.DialogueBox.Interact;
            }
        }
    }
    
}
 