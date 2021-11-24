using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHP : CharacterBase
{
    public float smooth;
    public float startVignetteTime;
    public float endVignetteTime;
    public Transform target;
    Volume volume;

    private void Start()
    {
        volume = target.GetComponent<Volume>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayHP();
        DisplayMana();
    }

    public override void DamageCharacter(int amount)
    {
        base.DamageCharacter(amount);
        if (amount > 0)
            StartCoroutine(DisplayVignette());
        if (currentHP <= 0)
            StartCoroutine(DisplayLoseScreen());
    }

    IEnumerator DisplayVignette()
    {

        float elapsed = 0.0f;
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            while (elapsed < startVignetteTime)
            {
                vignette.intensity.value = Mathf.Lerp(0f, .25f, elapsed / startVignetteTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        yield return new WaitForSeconds(endVignetteTime);
        elapsed = 0.0f;
        while (elapsed < startVignetteTime)
        {
            vignette.intensity.value = Mathf.Lerp(.25f, 0f, elapsed / startVignetteTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DisplayLoseScreen()
    {
        yield return new WaitForSeconds(startVignetteTime);
        SceneManager.LoadScene("4 Lose Scene");
    }
}
