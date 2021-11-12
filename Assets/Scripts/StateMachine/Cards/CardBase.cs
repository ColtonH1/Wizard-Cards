using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public int attackAmount;
    public int healAmount;
    public int manaCost;
    protected GameObject currentCharacter;
    private GameObject opposingCharacter;
    CharacterBase hurtCharacter;
    CharacterBase healCharacter;

    private void Start()
    {
        currentCharacter = gameObject.GetComponentInParent<CharacterBase>().thisCharacter;
        opposingCharacter = currentCharacter.GetComponent<CharacterBase>().opposingCharacter;
        hurtCharacter = opposingCharacter.GetComponent<CharacterBase>();
        healCharacter = currentCharacter.GetComponent<CharacterBase>();

        ManaCost(manaCost);
        Damage(attackAmount);
        Heal(healAmount);
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
}
