using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphicsControl : MonoBehaviour
{
    private SpriteRenderer renderer;
    private int maxHealth;
    public Sprite[] sprites;

    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // set by enemy spinner at the beginning of combat
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    // called by enemy spinner whenever current health changes
    public void UpdateDamageState(int currentHealth)
    { 
        if (currentHealth == 0)
        {
            renderer.sprite = sprites[sprites.Length - 1];
        }
        else
        {
            renderer.sprite = sprites[Mathf.Clamp((sprites.Length - 1) - (int)((float)currentHealth / (float)maxHealth * (float)(sprites.Length + 1)), 
                0, sprites.Length - 2)];
        }
    }
}
