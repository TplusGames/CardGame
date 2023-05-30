using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int shortDamage;
    public int mediumDamage;
    public int longDamage;

    [SerializeField] private TextMeshProUGUI shortDamageUI;
    [SerializeField] private TextMeshProUGUI mediumDamageUI;
    [SerializeField] private TextMeshProUGUI longDamageUI;

    private int damage;

    public bool dead;

    private void Start()
    {
        shortDamage = Random.Range(50, 150);
        mediumDamage = Random.Range(50, 150);
        longDamage = Random.Range(50, 150);

        shortDamageUI.text = shortDamage.ToString();
        mediumDamageUI.text = mediumDamage.ToString();
        longDamageUI.text = longDamage.ToString();
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
        shortDamageUI.enabled = false;
        mediumDamageUI.enabled = false;
        longDamageUI.text = damage.ToString();
    }

    public int GetDamage()
    {
        return damage;
    }
}
