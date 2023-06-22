using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using enum states to change the game "state"!
public enum TurnState { START, END, PLAYERTURN, WAITING, ENEMYTURN, VICTORY, DEFEAT }
public class TurnSystem : MonoBehaviour
{

    public TurnState gameState;
    private void Start() //Setting the state of the game to "Start" 
    {
        gameState = TurnState.START;
    }


}
