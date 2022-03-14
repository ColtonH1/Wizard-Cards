using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] public int maxHPEasy;
    [SerializeField] public int maxHPMed;
    [SerializeField] public int maxHPHard;
    [HideInInspector] public int maxHP;
    [SerializeField] protected TextMeshProUGUI hpText;
    public int currentHP;

    public SettingsSO settings;

    [SerializeField] TextMeshProUGUI manaText;
    public int currentMana;

    [SerializeField] TextMeshProUGUI shieldText;
    public int currentShieldAmount;

    [SerializeField] public GameObject thisCharacter;
    [SerializeField] public GameObject opposingCharacter;

    private void OnEnable()
    {
        SetMaxHealth();
        currentMana = 0;
    }

    protected virtual void SetMaxHealth()
    {
        switch(settings.difficulty)
        {
            case 0:
                Debug.Log("Easy");
                maxHP = maxHPEasy;
                break;
            case 1:
                Debug.Log("Medium");
                maxHP = maxHPMed;
                break;
            case 2:
                Debug.Log("Hard");
                maxHP = maxHPHard;
                break;
            default:
                Debug.LogError("No difficulty set. Game not valid!");
                break;
        }
        currentHP = maxHP;
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
