using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Simple script that just updates the
 * scale of the health bar based on 
 * the damage done.
 */

public class HealthUI : MonoBehaviour
{
    public int startHealth;
    public RectTransform bar;
    public TMP_Text text;
    // this makes the lerp have a shape that we can control instead of just being linear
    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    // need all these for lerping
    private int currentHealth;
    private float startScale;
    private float currentScale;
    private float lerpProgress;

    void Start()
    {
        currentHealth = startHealth;
        startScale = bar.localScale.x;
        currentScale = startScale;
        lerpProgress = 0.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AlterHealth(-3);
        }
        // each frame check if scale needs to change
        float newScale = ((float)currentHealth / (float)startHealth) * startScale;
        
        // if so, lerp bar to where it needs to be to reflect new health amount
        if (currentScale != newScale)
        {
            lerpProgress += Time.deltaTime;
            bar.localScale = new Vector3(Mathf.Lerp(currentScale, newScale, curve.Evaluate(lerpProgress)), bar.localScale.y, bar.localScale.z);
            if (lerpProgress >= 1)
            {
                if (currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
                currentScale = newScale;
            }
        }
    }

    // update health based on damage taken / health healed
    public void AlterHealth(int amount) // if taking damage, amount should be negative
    {
        currentHealth = (int)Mathf.Clamp(currentHealth + amount, 0.0f, startHealth);
        text.text = currentHealth + "/" + startHealth;
        lerpProgress = 0.0f;
    }
}
