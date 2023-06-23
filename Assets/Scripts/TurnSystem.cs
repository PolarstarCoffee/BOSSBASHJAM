using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    // singleton
    private static TurnSystem instance;

    //Using enum states to change the game "state"! (Using enum states I think gives off the JRPG vibe) Do so outside of the class to reference it anywhere

    public enum TurnState { START, END, PLAYERTURN, WAITING, ENEMYTURN, RESOLVINGCOMBAT, VICTORY, DEFEAT }
    //game state variable
    public TurnState gameState;
    //TMPro variable to signal change of game state
    public TextMeshProUGUI stateText;

    // singleton access
    public static TurnSystem Instance()
    {
        return instance;
    }

    private void Awake()
    {
        // setting up singleton
        if (instance != null && instance != this)
            Destroy(this);
        instance = this;
    }

    private void Start() //setting the state of the game to "Start" 
    {
        //setting the inital game state to start!!!!! RAAAAAAGh
        SetState(TurnState.START);
    }

    IEnumerator SetUp() //setting up the scene for battle 
    {
        yield return new WaitForSeconds(1);
        //Changing the state of the game to the player's turn 
        SetState(TurnState.PLAYERTURN);
    }

    //Converts enum to string to display to player (I can go for some string cheese rn)
    public void SetState(TurnState newState)
    {
        gameState = newState;
        stateText.text = gameState.ToString(); // update text

        if (gameState == TurnState.START)
        {
            StartCoroutine(SetUp());
        }
        else if (gameState == TurnState.DEFEAT)
        {
            // trigger losing screen
        }
        else if (gameState == TurnState.VICTORY)
        {
            // trigger going to next enemy
        }
    }

    public TurnState GetCurrentState()
    {
        return gameState;
    }
}
