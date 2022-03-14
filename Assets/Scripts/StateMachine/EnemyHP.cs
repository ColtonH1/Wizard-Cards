using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHP : CharacterBase
{
    void Update()
    {
        DisplayHP();
        DisplayMana();
        DisplayShield();
    }

    public override void DamageCharacter(int amount)
    {
        base.DamageCharacter(amount);
        if(currentHP <= 0)
            StartCoroutine(DisplayWinScreen());
    }

    IEnumerator DisplayWinScreen()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Win Scene");
    }
}
