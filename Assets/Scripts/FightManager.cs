using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public FightManager nextFight;
    public bool startingFight = false;

    public GameObject enemySpinner;

    // Start is called before the first frame update
    void Start()
    {
        if (startingFight)
            StartCoroutine(DelayedStart());
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1.0f);
        SetUpFight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpFight()
    {
        enemySpinner.tag = "EnemySpinner";
        CombatManager.Instance().AssignEnemy();
        enemySpinner.GetComponent<EnemyHealth>().ActivateHealthUI();
    }

    public void MoveToNextStage()
    {
        int gameStatus = CameraController.Instance().MoveToNextCheckpoint();
        if (gameStatus == -1)
        {
            // credits, etc
        }
        else
        {
            nextFight.gameObject.SetActive(true);
            nextFight.SetUpFight();
        }
    }
}
