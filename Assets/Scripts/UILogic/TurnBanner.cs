using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnBanner : MonoBehaviour
{
    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;

    [SerializeField] private TextMeshProUGUI banner;

    private void Start()
    {
        banner.gameObject.SetActive(false);
    }

    public void StartBanner()
    {
        StartCoroutine(DisplayTurnBanner());
    }

    private IEnumerator DisplayTurnBanner()
    {
        if (TurnManager.Instance().EnemyTurn())
        {
            banner.gameObject.SetActive(true);
            banner.color = enemyColor;
            banner.alpha = 255f;
            banner.text = "Enemy Turn";
        }
        else
        {
            banner.gameObject.SetActive(true);
            banner.color = playerColor;
            banner.alpha = 255f;
            banner.text = "Your Turn";
        }


        yield return new WaitForSeconds(5);

        banner.gameObject.SetActive(false);
    }
}
