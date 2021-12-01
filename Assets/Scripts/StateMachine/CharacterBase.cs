using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] public int maxHP;
    [SerializeField] protected TextMeshProUGUI hpText;
    public int currentHP;

    [SerializeField] TextMeshProUGUI manaText;
    public int currentMana;

    [SerializeField] TextMeshProUGUI shieldText;
    public int currentShieldAmount;

    [SerializeField] public GameObject thisCharacter;
    [SerializeField] public GameObject opposingCharacter;

    private void OnEnable()
    {
        currentHP = maxHP;
        currentMana = 0;
    }

    protected virtual void DisplayHP()
    {
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
        if(currentHP <= 0)
        {
            currentHP = 0;
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

    protected void DisplayShield()
    {
        shieldText.text = "Shield: " + currentShieldAmount;
    }

    public virtual void DamageCharacter(int amount)
    {
        int totalAmount = amount;
        amount -= currentShieldAmount;
        currentShieldAmount -= totalAmount;
        if (currentShieldAmount < 0)
            currentShieldAmount = 0;
        currentHP -= amount;
    }

    public void HealCharacter(int amount)
    {
        currentHP -= amount;
    }

    public void ManaCost(int amount)
    {
        currentMana -= amount;
    }

    public void AddShield(int amount)
    {
        currentShieldAmount += amount;
    }
}
