using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


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

    // selection highlighting on mouse over
    private GameObject highlightGraphics;
    private bool canHighlight;
    //durability UI
    public TextMeshProUGUI durabilityText;
    //public TextMeshProUGUI durabilityText2;
   
    void Start()
    {
        spinning = false;
        centering = false;
        highlightGraphics = transform.Find("GraphicsHighlight").gameObject;
        highlightGraphics.SetActive(false);
        canHighlight = true;
        
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
                canHighlight = true;
                centering = false;


                //methods to calculate durability 
                currentSlice.usageIncrement();
                currentSlice.durabilityDecrease();
                currentSlice.durabilityDepletedCheck();
                durabilityUIUpdate();



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
        CombatManager.Instance().PlayerTurn(currentSlice.attribute);
    }


    private void OnMouseOver()
    {
        if (canHighlight && TurnSystem.Instance().GetCurrentState() == TurnSystem.TurnState.PLAYERTURN)
            highlightGraphics.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlightGraphics.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (TurnSystem.Instance().GetCurrentState() == TurnSystem.TurnState.PLAYERTURN)
        {
            Spin();
            highlightGraphics.SetActive(false);
            canHighlight = false;
            TurnSystem.Instance().SetState(TurnSystem.TurnState.WAITING);
        }
    }
   public void durabilityUIUpdate()
    {
        durabilityText.text = currentSlice.GetCurrentDurability().ToString();
        //durabilityText2.text = currentSlice.GetCurrentDurability().ToString();
    }
}
