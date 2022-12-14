using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private FloatVariable maxHealthData;
    [SerializeField] private FloatVariable maxManaData;
    [SerializeField] private FloatVariable expData;
    [SerializeField] private FloatVariable damageData;

    public bool dead = false;
    private float maxMana;
    private float manaRegenMultiplier = 1f;
    private float maxHealth;

    private void Awake()
    {
        instance = this;
        maxHealthData.value = 100;
        maxManaData.value = 10;
    }

    private void Start()
    {
        //  Calculate Max Health/Mana
        maxMana = maxManaData.value;
        maxHealth = maxHealthData.value;
    }

    private void Update()
    {
        if (maxManaData.value < maxMana)
        {
            RegenerateMana();
        }
        cam.ScreenToWorldPoint(Input.mousePosition);
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
