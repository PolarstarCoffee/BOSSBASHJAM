using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This script controls manages the spinner. There's a 
 * lotta gross variables but most of them are just 
 * helping keep track of rotations. It's set up as a singleton
 * bc we'll want to have other objects subscribe to the 
 * OnSelection event so we can do whatever happens after a 
 * spin!
 */

public class PlayerSpinnerControl : MonoBehaviour
{
    // singleton
    private PlayerSpinnerControl instance;

    private UnityEvent OnSelection; // not using yet, but this will be called after a spin is finished

    // controlling the amount of spin that happens
    public float spinForceMin, spinForceMax;
    private float currentSpinAmount;
    public float currentRotation;
    private bool spinning;

    // centering the spinner on the current slice after spin
    private bool centering;
    private float oldRotation;
    private float centeringSpeed;

    // managing the slices
    public SpinnerSlice[] slices = new SpinnerSlice[8];
    private SpinnerSlice currentSlice;

    // singleton access
    public PlayerSpinnerControl Instance()
    {
        if (instance == null)
            instance = this;
        return this;
    }

    void Start()
    {
        spinning = false;
        centering = false;
    }

    void Update()
    {
        // temporary for making sure stuff works!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }

        if (spinning)
        {
            float spinAmount = currentSpinAmount * 10f * Time.deltaTime;

            // find current slice and highlight it
            if (currentRotation > 360)
                currentRotation -= 360.0f;
            currentRotation += spinAmount;
            foreach (SpinnerSlice slice in slices)
            {
                if (slice.SpinnerRotationInRange(currentRotation))
                {
                    currentSlice = slice;
                    currentSlice.HighlightSlice();
                }
                else
                {
                    // if not the current slice, set to default color
                    slice.UnhighlightSlice();
                }
            }

            // rotate spinner
            transform.Rotate(new Vector3(0.0f, 0.0f, spinAmount));
            currentSpinAmount -= Time.deltaTime * 40f;
            if (currentSpinAmount <= 0.0f)
            {
                // finished spinning, now recenter
                currentSpinAmount = 0.0f; // reusing this variable in centering
                oldRotation = currentRotation;
                centering = true;
                spinning = false;
            }
        }

        if (centering)
        {
            // change speed of recentering based on how far we are from target rotation
            if (Mathf.Abs(currentSlice.GetCenteredSlicerotation() - oldRotation) > 15.0f)
                centeringSpeed = 10.0f;
            else
                centeringSpeed = 5.0f;

            currentSpinAmount += Time.deltaTime * centeringSpeed;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0.0f, 0.0f, oldRotation), Quaternion.Euler(0.0f, 0.0f, currentSlice.GetCenteredSlicerotation()), currentSpinAmount);
            if (currentSpinAmount >= 1.0f)
            {
                // done recentering
                centering = false;
            }
        }
    }

    // begin a spin
    public void Spin()
    {
        spinning = true;
        currentSpinAmount = Random.Range(spinForceMin, spinForceMax);
    }
}
