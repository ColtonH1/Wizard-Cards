using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] TextMeshProUGUI hpText;
    private int currentHP;

    [SerializeField] TextMeshProUGUI manaText;
    private int currentMana;

    [SerializeField] public GameObject thisCharacter;
    [SerializeField] public GameObject opposingCharacter;

    private void OnEnable()
    {
        currentHP = maxHP;
        currentMana = 0;
    }

    protected void DisplayHP()
    {
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }

        hpText.text = "Health: " + currentHP;
    }

    protected void DisplayMana()
    {
        if (currentMana <= 0)
        {
            currentMana = 0;
        }

        manaText.text = "Mana: " + currentMana;
    }

    public void DamageCharacter(int amount)
    {
        currentHP -= amount;
    }

    public void HealCharacter(int amount)
    {
        currentHP -= amount;
    }

    public void ManaCost(int amount)
    {
        currentMana += amount;
    }
}
