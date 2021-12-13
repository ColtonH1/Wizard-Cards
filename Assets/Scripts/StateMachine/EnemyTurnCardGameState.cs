using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] GameObject enemyGO;
    [SerializeField] CharacterBase enemyCB;
    [SerializeField] GameObject placeCardPnl;
    [SerializeField] GameObject screenBlocker;
    [SerializeField] TextMeshProUGUI passText;
    private GameObject cardToBePlayed;
    EnemyAI enemy;

    [SerializeField] public float _pauseDuration = 3f;

    private bool turnTaken;

    public override void Enter()
    {
        EnemyTurnBegan?.Invoke();

        screenBlocker.gameObject.SetActive(true);
        turnTaken = false;
        enemy = enemyGO.GetComponent<EnemyAI>();
        enemyCB.ManaCost(-1); //add one mana to enemy
        enemy.LookAtHand();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Tick()
    {
    }

    private void Thinking()
    {
        if (!turnTaken)
        {
            cardToBePlayed = enemy.Think();
            EnemyTurnEnded?.Invoke();
            if (cardToBePlayed == null)
                passText.gameObject.SetActive(true);
            else
                cardToBePlayed.GetComponent<CardBase>().PlayCard();

            turnTaken = true;
        }
    }

    public override void Exit()
    {
        
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        Thinking();
        yield return new WaitForSeconds(2f);
        passText.gameObject.SetActive(false);
        screenBlocker.gameObject.SetActive(false);
        StateMachine.ChangeState<PlayerTurnCardGameState>();
    }
}
