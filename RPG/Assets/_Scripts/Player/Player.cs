using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    [SerializeField] private FloatVariable maxHealthData;
    [SerializeField] private FloatVariable maxManaData;
    [SerializeField] private FloatVariable expData;
    [SerializeField] private FloatVariable damageData;

    private CircleCollider2D interactRange;
    private InputAction interact;
    private float maxMana;
    private float manaRegenMultiplier = 1f;
    private float maxHealth;

    public NPCData selectedNPC { get; private set; }
    public bool dead = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        interactRange = GetComponent<CircleCollider2D>();

        // For Testing -------------------
        maxHealthData.value = 100;
        maxManaData.value = 10;
    }
    private void Start()
    {
        interact = PlayerController.instance.interract;
        // Calculate Max Health/Mana
        maxMana = maxManaData.value;
        maxHealth = maxHealthData.value;
    }
    private void Update()
    {
        // Regen Mana
        if (maxManaData.value < maxMana)
        {
            RegenerateMana();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            selectedNPC = collision.GetComponent<NPC>().data;
            collision.GetComponent<SpriteRenderer>().color = Color.blue;
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
    public void gainHealth(float _health)
    {
        maxHealthData.value += _health;
    }

    public void TakeDamage(float _damage)
    {

        Debug.Log(_damage + " damage taken. ");
        maxHealthData.value = Mathf.Clamp(maxHealthData.value - _damage, 0, maxHealth);
        
        if (maxHealthData.value > 0)
        {
            // Hurt Visual
        }
        else
        {
            // Destroy(gameObject);
        }
    }
    
    public IEnumerator TakeDamageOverTime(float _damage, float _duration)
    {
        while (_duration != 0)
        {
            maxHealthData.value = Mathf.Clamp(maxHealthData.value - _damage, 0, maxHealth);
            yield return new WaitForSeconds(1);
            _duration--;
        }
        yield return null;
    }

    void RegenerateMana()
    {
        maxManaData.value += (Time.deltaTime * manaRegenMultiplier);
    }
}
