using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum SliceAttribute
    {
        ATTACK,
        DODGE,
        NOTHING,
        HEAL,
        NULL // this is if an attribute hasn't been selected
        // will add more later
    }

    public SliceAttribute attribute;

    // angle of the spinner when this slice is selected
    public float rotationRangeMin, rotationRangeMax;
    public Color startColor;
    private Color highlightcolor = Color.blue;

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
    //decreases durability (How do we have different durability attributes w/o manually setting each one in the inspector?)
    public void durabilityDecrease()
    {
       currentDurability = durability - currentUsage;
    }
    //temporary! gets rid of useless ass slice smfh
    public void durabilityDepleted()
    {
        if (currentDurability <= 0)
        {
            Debug.Log("This shouldn't work anymore");

        }
    }
    //Increase the current useage count
    public void usageIncrement()
    {
        currentUsage++;
    }
}
