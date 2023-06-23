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

   
   
    private void ProcessEnemyTurn()
    {
        if (enemyAttribute == SpinnerSlice.SliceAttribute.ATTACK)
        {
            if (playerAttribute == SpinnerSlice.SliceAttribute.DODGE)
            {
                // enemy attacks, player dodges
            }
            else
            {
                playerHealth.TakeDamage(3);
            }
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


      
        TurnSystem.Instance().SetState(TurnSystem.TurnState.START);
    }
    private void ProcessPlayerTurn()
    {
        if (playerAttribute == SpinnerSlice.SliceAttribute.ATTACK)
        {
            if (enemyAttribute == SpinnerSlice.SliceAttribute.DODGE)
            {
                // player attacks, enemy dodges
            }
            else
            {
                enemyHealth.TakeDamage(3);
            }
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
        StartCoroutine(DelayedEnemySpin());
        TurnSystem.Instance().SetState(TurnSystem.TurnState.ENEMYTURN);
    }
    IEnumerator DelayedEnemySpin()
    {
        yield return new WaitForSeconds(1);
        enemyControl.Spin();
    }
}
