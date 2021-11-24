using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    public enum ActionType
    {
        THINKING,
        ATTACKING, 
        HEALING, 
        PASSING
    }

    public ActionType actionType = ActionType.THINKING;
    public int attackAmount;
    public int healAmount;
    public int manaCost;
    private float pauseTime;
    public Sprite originalImage;
    protected GameObject currentCharacter;
    private GameObject opposingCharacter;
    CharacterBase hurtCharacter;
    CharacterBase healCharacter;
    GameManager gameManager;
    GameObject cardGameController;
    DrawCards draw;

    private void Start()
    {
        currentCharacter = gameObject.GetComponentInParent<CharacterBase>().thisCharacter;
        opposingCharacter = currentCharacter.GetComponent<CharacterBase>().opposingCharacter;
        hurtCharacter = opposingCharacter.GetComponent<CharacterBase>();
        healCharacter = currentCharacter.GetComponent<CharacterBase>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        draw = GameObject.Find("GameManager").GetComponent<DrawCards>();
        cardGameController = GameObject.Find("CardGameController");
        pauseTime = cardGameController.GetComponentInChildren<EnemyTurnCardGameState>()._pauseDuration;
    }

    public void PlayCard()
    {
        bool canPlay;
        canPlay = CheckManaAvailable();
        Debug.Log("Can play?: " + canPlay);
        if (canPlay)
        {
            ManaCost(manaCost);
            Damage(attackAmount);
            Heal(healAmount);

            Destroy(gameObject, pauseTime);
            if(currentCharacter.name == "Player")
            {
                StartCoroutine(draw.ReplacePlayerCard(pauseTime-.05f));
            }
            else
            {
                StartCoroutine(draw.ReplaceEnemyCard(pauseTime-.05f));
            }

        }
    }

    public bool CheckManaAvailable()
    {
        bool canPlay = false;
        int playerMana = healCharacter.currentMana;

        if(playerMana >= manaCost)
        {
            canPlay = true;
        }
        else
        {
            StartCoroutine(gameManager.ShowTextWait(3));
        }

        return canPlay;
    }

    public void ManaCost(int amount)
    {
        healCharacter.ManaCost(amount);
    }

    public void Damage(int amount)
    {
        hurtCharacter.DamageCharacter(amount);
    }

    public void Heal(int amount)
    {
        healCharacter.HealCharacter(amount * -1);
    }

    public void DiscardCard()
    {
        StartCoroutine(draw.ReplacePlayerCard(0));
        GetComponent<CardZoom>().OnHoverExit();
        Destroy(GetComponent<CardZoom>());
        Destroy(gameObject, .5f);
    }
}
