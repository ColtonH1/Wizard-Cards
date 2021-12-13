using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] GameObject playAreaPnl;
    [SerializeField] GameObject discardPnl;
    [SerializeField] CharacterBase playerCB;

    private bool passed;

    public override void Enter()
    {
        playerCB.ManaCost(-1); //add one mana point for starting new round
        passed = false; //assume player isn't passing

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    public void OnPressedConfirm()
    {
        if(!passed && playAreaPnl.transform.childCount > 0)
        {
            playAreaPnl.transform.GetChild(0).GetComponent<CardBase>().PlayCard();
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
        else if(passed)
            StateMachine.ChangeState<EnemyTurnCardGameState>();

    }

    public void OnPressedDiscard()
    {
        if(discardPnl.transform.childCount > 0)
        {
            discardPnl.transform.GetChild(0).GetComponent<CardBase>().DiscardCard();
            Passed();
        }

    }

    public void Passed()
    {
        passed = true;
        Exit();
        OnPressedConfirm();
    }
}
