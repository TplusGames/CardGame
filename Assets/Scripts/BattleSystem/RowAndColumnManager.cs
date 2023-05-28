using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowAndColumnManager : MonoBehaviour
{
    public List<List<Transform>> columnLists = new List<List<Transform>>();
    [SerializeField] private List<int> playerColumnScores = new List<int>();
    [SerializeField] private List<int> enemyColumnScores = new List<int>();

    public List<Transform> columnOne = new List<Transform>();
    public List<Transform> columnTwo = new List<Transform>();
    public List<Transform> columnThree = new List<Transform>();
    public List<Transform> columnFour = new List<Transform>();
    public List<Transform> columnFive = new List<Transform>();

    public List<Transform> rowOne = new List<Transform>();
    public List<Transform> rowTwo = new List<Transform>();
    public List<Transform> rowThree = new List<Transform>();
    public List<Transform> rowFour = new List<Transform>();

    [SerializeField] private TextMeshProUGUI columnOnePlayerScoreUI;
    [SerializeField] private TextMeshProUGUI columnTwoPlayerScoreUI;
    [SerializeField] private TextMeshProUGUI columnThreePlayerScoreUI;
    [SerializeField] private TextMeshProUGUI columnFourPlayerScoreUI;
    [SerializeField] private TextMeshProUGUI columnFivePlayerScoreUI;

    [SerializeField] private TextMeshProUGUI columnOneEnemyScoreUI;
    [SerializeField] private TextMeshProUGUI columnTwoEnemyScoreUI;
    [SerializeField] private TextMeshProUGUI columnThreeEnemyScoreUI;
    [SerializeField] private TextMeshProUGUI columnFourEnemyScoreUI;
    [SerializeField] private TextMeshProUGUI columnFiveEnemyScoreUI;

    [SerializeField] private Image columnOneHolderSignal;
    [SerializeField] private Image columnTwoHolderSignal;
    [SerializeField] private Image columnThreeHolderSignal;
    [SerializeField] private Image columnFourHolderSignal;
    [SerializeField] private Image columnFiveHolderSignal;

    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;

    private void Start()
    {
        columnLists.Add(columnOne);
        columnLists.Add(columnTwo);
        columnLists.Add(columnThree);
        columnLists.Add(columnFour);
        columnLists.Add(columnFive);

        playerColumnScores.Add(0);
        playerColumnScores.Add(0);
        playerColumnScores.Add(0);
        playerColumnScores.Add(0);
        playerColumnScores.Add(0);
        enemyColumnScores.Add(0);
        enemyColumnScores.Add(0);
        enemyColumnScores.Add(0);
        enemyColumnScores.Add(0);
        enemyColumnScores.Add(0);
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        columnOnePlayerScoreUI.text = playerColumnScores[0].ToString();
        columnTwoPlayerScoreUI.text = playerColumnScores[1].ToString();
        columnThreePlayerScoreUI.text = playerColumnScores[2].ToString();
        columnFourPlayerScoreUI.text = playerColumnScores[3].ToString();
        columnFivePlayerScoreUI.text = playerColumnScores[4].ToString();

        columnOneEnemyScoreUI.text = enemyColumnScores[0].ToString();
        columnTwoEnemyScoreUI.text = enemyColumnScores[1].ToString();
        columnThreeEnemyScoreUI.text = enemyColumnScores[2].ToString();
        columnFourEnemyScoreUI.text = enemyColumnScores[3].ToString();
        columnFiveEnemyScoreUI.text = enemyColumnScores[4].ToString();
    }

    public void CalculateCardScore(Transform slot, CardPlacement card)
    {
        foreach (List<Transform> column in columnLists)
        {
            if (column.Contains(slot))
            {
                if (card.IsPlayerCard())
                {
                    if (rowOne.Contains(slot))
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().longDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().longDamage);
                    }
                    else if (rowTwo.Contains(slot))
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().mediumDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().mediumDamage);
                    }
                    else
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().shortDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().shortDamage);
                    }
                }
                else
                {
                    if (rowFour.Contains(slot))
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().longDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().longDamage);
                    }
                    else if (rowThree.Contains(slot))
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().mediumDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().mediumDamage);
                    }
                    else
                    {
                        slot.GetComponent<CardSlot>().SetDamage(card.GetComponent<Card>().shortDamage);
                        card.GetComponent<Card>().SetDamage(card.GetComponent<Card>().shortDamage);
                    }
                }
                break;
            }
            
        }
        
    }

    public void CalculateColumnScore()
    {
        for (int i = 0; i < columnLists.Count; i++)
        {
            int playerScoreToAdd = 0;
            int enemyScoreToAdd = 0;

            foreach (Transform slot in columnLists[i])
            {

                if (slot.childCount > 0)
                {
                    CardSlot card = slot.GetComponent<CardSlot>();

                    if (!card.hasBeenCounted)
                    {
                        if (card.heldByPlayer)
                        {
                            playerScoreToAdd += card.GetDamage();
                        }
                        else
                        {
                            enemyScoreToAdd += card.GetDamage();
                        }
                    }
                }

                playerColumnScores[i] = playerScoreToAdd;
                enemyColumnScores[i] = enemyScoreToAdd;
            }
        }

        
        UpdateUI();
        CalculatePlayerScore();
    }

    public void ResetScores()
    {
        for (int i = 0; i < playerColumnScores.Count; i++) 
        { 
            playerColumnScores[i] = 0;
            enemyColumnScores[i] = 0;

        }
    }

    public void CalculatePlayerScore()
    {
        playerScore = 0;
        enemyScore = 0;

        if (playerColumnScores[0] > enemyColumnScores[0])
        {
            //Player wins column
            playerScore += 1;
            columnOneHolderSignal.color = Color.green;
        }
        else if (enemyColumnScores[0] > playerColumnScores[0])
        {
            //Enemy wins column
            enemyScore += 1;
            columnOneHolderSignal.color = Color.red;
        }

        if (playerColumnScores[1] > enemyColumnScores[1])
        {
            //Player wins column
            playerScore += 1;
            columnTwoHolderSignal.color = Color.green;
        }
        else if (enemyColumnScores[1] > playerColumnScores[1])
        {
            //Enemy wins column
            enemyScore += 1;
            columnTwoHolderSignal.color = Color.red;
        }

        if (playerColumnScores[2] > enemyColumnScores[2])
        {
            //Player wins column
            playerScore += 1;
            columnThreeHolderSignal.color = Color.green;
        }
        else if (enemyColumnScores[2] > playerColumnScores[2])
        {
            //Enemy wins column
            enemyScore += 1;
            columnThreeHolderSignal.color = Color.red;
        }

        if (playerColumnScores[3] > enemyColumnScores[3])
        {
            //Player wins column
            playerScore += 1;
            columnFourHolderSignal.color = Color.green;
        }
        else if (enemyColumnScores[3] > playerColumnScores[3])
        {
            //Enemy wins column
            enemyScore += 1;
            columnFourHolderSignal.color = Color.red;
        }

        if (playerColumnScores[4] > enemyColumnScores[4])
        {
            //Player wins column
            playerScore += 1;
            columnFiveHolderSignal.color = Color.green;
        }
        else if (enemyColumnScores[4] > playerColumnScores[4])
        {
            //Enemy wins column
            enemyScore += 1;
            columnFiveHolderSignal.color = Color.red;
        }
    }
}
