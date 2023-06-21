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
    // angle of the spinner when this slice is selected
    public float rotationRangeMin, rotationRangeMax;
    public Color color;
    private Color highlightcolor = Color.blue;

    // returns true if the spinner's current rotation matches the range
    // that would make this slice the current one
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
        GetComponent<SpriteRenderer>().color = color;
    }
}
