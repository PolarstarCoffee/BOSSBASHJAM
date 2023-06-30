using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // singleton
    private static CombatManager instance;

    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private EnemySpinnerControl enemyControl;

    // attributes being played on the current turn, will be reassigned with each turn
    private SpinnerSlice.SliceAttribute enemyAttribute;
    private SpinnerSlice.SliceAttribute playerAttribute;

    // singleton access
    public static CombatManager Instance()
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

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        enemyHealth = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
        enemyControl = GameObject.FindWithTag("Enemy").GetComponent<EnemySpinnerControl>();

        enemyAttribute = SpinnerSlice.SliceAttribute.NULL;
        playerAttribute = SpinnerSlice.SliceAttribute.NULL;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyTurn(SpinnerSlice.SliceAttribute attribute)
    {
        enemyAttribute = attribute;
        if (playerAttribute != SpinnerSlice.SliceAttribute.NULL)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.RESOLVINGCOMBAT);
            ProcessEnemyTurn();
        }
    }

    public void PlayerTurn(SpinnerSlice.SliceAttribute attribute)
    {
        playerAttribute = attribute;
        TurnSystem.Instance().SetState(TurnSystem.TurnState.RESOLVINGCOMBAT);
        ProcessPlayerTurn();
    }

   
   //Method that stores all the enemies possible actions
    private void ProcessEnemyTurn()
    {
        //Attack/Dodge Piece (Enemy)
        if (enemyAttribute == SpinnerSlice.SliceAttribute.ATTACK)
        {
            if (playerAttribute == SpinnerSlice.SliceAttribute.DODGE)
            {
                // enemy attacks, player dodges
                playerHealth.GetCurrentHealth();
            }
            else
            {
                playerHealth.TakeDamage(3);
            }
        }
        //Heal Piece (Enemy)
        if (enemyAttribute == SpinnerSlice.SliceAttribute.HEAL)
        {
            enemyHealth.Heal(3);
        }
        //Default Piece (Enemy)
        if (enemyAttribute == SpinnerSlice.SliceAttribute.DEFAULT)
        {
            if (playerAttribute == SpinnerSlice.SliceAttribute.DODGE)
            {
                // enemy attacks, player dodges
                playerHealth.GetCurrentHealth();
            }
            else
            {
                playerHealth.TakeDamage(1);
            }
        }
        //Player Skip piece (The Show Must Go On) will most likely change 
        if (enemyAttribute == SpinnerSlice.SliceAttribute.SKIP)
        {
            StartCoroutine(DelayedEnemySpin());
            TurnSystem.Instance().SetState(TurnSystem.TurnState.ENEMYTURN);

            //TEMP
        }
        //Attack boost piece (Win the Crowd) will most likely change 
        if (enemyAttribute == SpinnerSlice.SliceAttribute.ABOOST)
        {
            playerHealth.TakeDamage(5);
        }


       

        if (playerHealth.GetCurrentHealth() < 1)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.DEFEAT);
            return;
        }
        else if (enemyHealth.GetCurrentHealth() < 1)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.VICTORY);
            return;
        }


      if (enemyAttribute != SpinnerSlice.SliceAttribute.SKIP)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.START);
        }
        //TurnSystem.Instance().SetState(TurnSystem.TurnState.START);
    }
    //Method that stores all the players possible actions
    private void ProcessPlayerTurn()
    {
        //Default attribute
        if (playerAttribute == SpinnerSlice.SliceAttribute.DEFAULT)
        {
            enemyHealth.TakeDamage(1);
        }
        //Blocked (Ketchup) attribute, nothing happens (Might need to change this piece to be skipped entirely to make more sense)
        if (playerAttribute == SpinnerSlice.SliceAttribute.BLOCKED)
        {
            return;
        }
        //Attack / Dodge attribute
        if (playerAttribute == SpinnerSlice.SliceAttribute.ATTACK)
        {
            if (enemyAttribute == SpinnerSlice.SliceAttribute.DODGE)
            {
                // player attacks, enemy dodges (Seemingly nothing happens here)
                enemyHealth.GetCurrentHealth();
            }
            else
            {
                enemyHealth.TakeDamage(3);
            }
        }
        //Heal attribute
        if (playerAttribute == SpinnerSlice.SliceAttribute.HEAL)
        {
            playerHealth.Heal(3);
        }
        //Defeat game state
        if (playerHealth.GetCurrentHealth() < 1)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.DEFEAT);
            return;
        }
        //Victory game state
        else if (enemyHealth.GetCurrentHealth() < 1)
        {
            TurnSystem.Instance().SetState(TurnSystem.TurnState.VICTORY);
            return;
        }


        //End of player turn
        StartCoroutine(DelayedEnemySpin());
        TurnSystem.Instance().SetState(TurnSystem.TurnState.ENEMYTURN);
    }
    //Slightly delayed start of enemy turn 
    IEnumerator DelayedEnemySpin()
    {
        yield return new WaitForSeconds(1);
        enemyControl.Spin();
    }
}
