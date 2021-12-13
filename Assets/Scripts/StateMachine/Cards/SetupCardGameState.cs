using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] public int startingCardNumber = 7;
    [SerializeField] public float timeToWait;
    [SerializeField] GameObject gameManager;
    private DrawCards draw;

    bool _activated = false;

    public override void Enter()
    {
        draw = gameManager.GetComponent<DrawCards>();
        StartCoroutine(draw.PlaceStartingCards(timeToWait, startingCardNumber));
        //CANT change state while still in Enter()/Exit() transition!
        //DONT put ChangeState<> here.
        _activated = false;
    }

    public override void Tick()
    {
        //admittedly hacky for demo. You would usually have delays or Input.
        if(_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
    }
}
