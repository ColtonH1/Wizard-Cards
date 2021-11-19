using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] GameObject playAreaPnl;
    [SerializeField] CharacterBase playerCB;

    private bool passed;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        playerCB.ManaCost(-1); //add one mana point for starting new round
        passed = false; //assume player isn't passing

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;

        Debug.Log("Player Turn: ...Exiting");
    }

    public void OnPressedConfirm()
    {
        if(!passed)
            playAreaPnl.transform.GetChild(0).GetComponent<CardBase>().PlayCard();
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    public void Passed()
    {
        passed = true;
        Exit();
        OnPressedConfirm();
    }
}
