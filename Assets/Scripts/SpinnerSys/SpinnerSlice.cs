using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script goes on each slice in the spinner!
 * We'll assign different values + attributes and 
 * shit to each slice later but for now it just 
 * helps the spinner with selecting which slice
 * is the current one.
 */

public class SpinnerSlice : MonoBehaviour
{
    //durability counter variable
    private int currentUsage;
    public int durability;
    private int currentDurability;
    private int poisonDuration = 3;
   
    



    public enum SliceAttribute
    {
        DEFAULT,
        ATTACK,
        DODGE,
        HEAL,
        BLOCKED,
        SWAPPABLE,
        EXPAND,
        SKIP,
        ABOOST,
        POISON,
        NULL // this is if an attribute hasn't been selected
        // will add more later
    }

    public SliceAttribute attribute;

    // angle of the spinner when this slice is selected
    public float rotationRangeMin, rotationRangeMax;
    public Color startColor;
    private Color highlightcolor = Color.blue;

    public void Start()
    {
        
        currentUsage = 0;
    }
    // returns true if the spinner's current rotation matches the range that would make this slice the current one
    public bool SpinnerRotationInRange(float currentSpinnerRotation)
    {
        if (currentSpinnerRotation > rotationRangeMin && currentSpinnerRotation <= rotationRangeMax)
            return true;
        return false;
    }

    // returns the center of this slice
    public float GetCenteredSlicerotation()
    {
        return rotationRangeMin + (rotationRangeMax - rotationRangeMin) / 2.0f;
    }

    public int GetCurrentDurability()
    {
        
        return currentDurability;
    }

    public int GetCurrentUsage()
    {
        return currentUsage;
    }

    // sets color of slice to highlighted color
    public void HighlightSlice()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    // sets color of slice to default
    public void UnhighlightSlice()
    {
        GetComponent<SpriteRenderer>().color = startColor;
    }

    // takes info from input slice and copies it to this slice
    public void CopySliceInfo(SpinnerSlice slice)
    {
        currentUsage = slice.GetCurrentUsage();
        durability = slice.durability;
        currentDurability = slice.GetCurrentDurability();
        attribute = slice.attribute;
        rotationRangeMin = slice.rotationRangeMin;
        rotationRangeMax = slice.rotationRangeMax;
        startColor = slice.startColor;
    }

    //decreases durability (How do we have different durability attributes w/o manually setting each one in the inspector?)
    public void durabilityDecrease()
    {
       currentDurability = durability - currentUsage;
        Debug.Log(currentDurability);
    }
    //temporary! Should turn the piece to the 'Default" piece (Deal 1 damage)
    public void durabilityDepletedCheck()
    {
        if (currentDurability == 0)
        {
            attribute = SliceAttribute.DEFAULT;
            //For testing
            Debug.Log("Piece is broken!");
        }
        else
        {
            return;
           
        }
    }
    //Increase the current usage count
    public void usageIncrement()
    {
        currentUsage++;
    }
    //Decrease the current usage count (used for the Heal durability method)
    public void usageDecrement()
    {
        currentUsage--;
    }
    //Decrease poision duration
    public void poisonDurationDecrement()
    {
        
        poisonDuration--;
        //If zero, nothing happens 
        if (poisonDuration < 0)
        {
            return;
        }
    }
    
}
