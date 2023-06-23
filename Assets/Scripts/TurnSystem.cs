using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TurnSystem : MonoBehaviour

{
    //Using enum states to change the game "state"! (Using enum states I think gives off the JRPG vibe) Do so outside of the class to reference it anywhere

    public enum TurnState { START, END, PLAYERTURN, WAITING, ENEMYTURN, VICTORY, DEFEAT }
    //game state variable
    public TurnState gameState;
    //TMPro variable to signal change of game state
    public TextMeshProUGUI stateText;
    private void Start() //setting the state of the game to "Start" 
    {
        //setting the inital game state to start!!!!! RAAAAAAGh
        gameState = TurnState.START;
        //Running the setUp method
        StartCoroutine(setUp());
        setState();
    }


    IEnumerator setUp() //setting up the scene for battle 
    {
        yield return new WaitForSeconds(1);
        //Changing the state of the game to the player's turn 
        gameState = TurnState.PLAYERTURN;
        setState();
    }

    //Converts enum to string to display to player (I can go for some string cheese rn)
    public void setState()
    {
        stateText.text = gameState.ToString();
    }
    //Does nothing for now, here's where the player would choose their actions (Might change later to simplify things)
    IEnumerator playerAction()
    {
        yield return new WaitForSeconds(1);
        
    }

    //Enemy action method (Might change later to simplify things)
    IEnumerator enemyAction()
    {
        yield return new WaitForSeconds(1);

    }



}
