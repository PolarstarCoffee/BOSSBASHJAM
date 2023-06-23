using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// note to self: combine this with player health script?

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public HealthUI healthUI;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // called by combat manager during combat
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthUI.AlterHealth(-damage);

        // TEMPORARY
        if (currentHealth == 0)
            Destroy(gameObject);
    }

    // called by combat manager during combat
    public void Heal(int healthHealed)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthHealed, 0, maxHealth);
        healthUI.AlterHealth(healthHealed);
    }
}
