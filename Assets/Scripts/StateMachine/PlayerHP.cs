using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHP : CharacterBase
{    
    public CameraShake cameraShake;
    public float shakeTime;

    // Update is called once per frame
    void Update()
    {
        DisplayHP();
        DisplayMana();
    }

    public override void DamageCharacter(int amount)
    {
        base.DamageCharacter(amount);
        if(amount > 0)
            cameraShake.VibrateForTime(shakeTime);
        if (currentHP <= 0)
            StartCoroutine(DisplayLoseScreen());
    }

    IEnumerator DisplayLoseScreen()
    {
        yield return new WaitForSeconds(shakeTime);
        SceneManager.LoadScene("4 Lose Scene");
    }
}
