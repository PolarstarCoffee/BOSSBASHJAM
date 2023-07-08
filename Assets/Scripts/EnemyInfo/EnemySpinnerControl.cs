using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpinnerControl : MonoBehaviour
{
    private UnityEvent OnSelection; // not using yet, but this will be called after a spin is finished

    // controlling the amount of spin that happens
    public float spinForceMin, spinForceMax;
    private float currentSpinAmount;
    private float currentRotation;
    private bool spinning;
    

    // centering the spinner on the current slice after spin
    private bool centering;
    private float oldRotation;
    private float centeringSpeed;

    // managing the slices
    public SpinnerSlice[] slices = new SpinnerSlice[8];
    private SpinnerSlice currentSlice;
    public TurnSystem.TurnState gameState;

    void Start()
    {
        spinning = false;
        centering = false;
    }

    void Update()
    {
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
                // done recentering, tell combat manager what the result of the spin is
                SubmitCombat();
                centering = false;

                currentSlice.usageIncrement();
                currentSlice.durabilityDecrease();
                currentSlice.durabilityDepletedCheck();

                /*if (CombatManager.Instance().isPoisoned == true)
                *{
                 *   CombatManager.Instance().poisonDurationDecrement();
                }*/

            }
        }
    }

    // begin a spin
    public void Spin()
    {
        spinning = true;
        currentSpinAmount = Random.Range(spinForceMin, spinForceMax);
    }

    public void SubmitCombat()
    {
        CombatManager.Instance().EnemyTurn(currentSlice.attribute);
    }
}
