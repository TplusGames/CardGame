using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;

    [SerializeField] private bool enemyTurn;
    [SerializeField] private TurnBanner banner;

    public RowAndColumnManager rowAndColumnManager;

    private void Awake()
    {
        instance = this;
    }

    public static TurnManager Instance() 
    {
        return instance;
    }

    public bool EnemyTurn()
    {
        return enemyTurn;
    }

    public void ChangeTurns()
    {
        if (enemyTurn)
        {
            enemyTurn = false;
        }
        else
        {
            enemyTurn = true;
            EnemyLogic.Instance().StartEnemyTurn();
        }
        banner.StartBanner();
        
        rowAndColumnManager.ResetScores();
        rowAndColumnManager.CalculateColumnScore();
    }
}
