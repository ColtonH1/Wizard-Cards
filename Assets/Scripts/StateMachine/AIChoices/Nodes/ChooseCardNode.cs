using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardNode
{
    /*
    private int currentMana, currentHealth, maxHealth;
    private EnemyAI enemyAI;
    [SerializeField] GameObject enemy;
    public List<GameObject> enemyDeck; //the cards in the hand



    public ChooseCardNode(GameObject enemy, int currentMana, int currentHealth, int maxHealth)
    {
        this.enemy = enemy;
        this.currentMana = enemy.GetComponent<EnemyHP>().currentMana;
        this.currentHealth = enemy.GetComponent<EnemyHP>().currentHP;
        this.maxHealth = enemy.GetComponent<EnemyHP>().maxHP;
        this.enemyAI = enemy.GetComponent<EnemyAI>();
    }

    public override NodeState Evaluate()
    {
        GameObject playCard;
        //get cards in hand
        //enemyDeck = enemyAI.LookAtHand();
        playCard = enemyDeck[0];
        //attempt to play card that costs mana
        return playCard != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    public bool ShouldHeal()
    {
        float percentHealth = currentHealth / maxHealth;
        bool shouldHeal = false;
        if(percentHealth < .8f)
        {
            float randomHealth = Random.Range(0, percentHealth);
            if (randomHealth < 0.3f)
                shouldHeal = true;
            else
                shouldHeal = false;
        }
        return shouldHeal;
    }

    public void SelectionSortByAttack(List<GameObject> unsortedList)
    {
        int max;
        GameObject temp;

        for (int i = 0; i < unsortedList.Count; i++)
        {
            max = i;

            for (int j = i + 1; j < unsortedList.Count; j++)
            {
                if (enemyDeck[j].GetComponent<CardBase>().attackAmount > enemyDeck[max].GetComponent<CardBase>().attackAmount)
                {
                    max = j;
                }
            }

            if (max != i)
            {
                temp = unsortedList[i];
                unsortedList[i] = unsortedList[max];
                unsortedList[max] = temp;
            }
        }
    }

    public void SelectionSortByHealAmount(List<GameObject> unsortedList)
    {
        int max;
        GameObject temp;

        for (int i = 0; i < unsortedList.Count; i++)
        {
            max = i;

            for (int j = i + 1; j < unsortedList.Count; j++)
            {
                if (enemyDeck[j].GetComponent<CardBase>().healAmount > enemyDeck[max].GetComponent<CardBase>().healAmount)
                {
                    max = j;
                }
            }

            if (max != i)
            {
                temp = unsortedList[i];
                unsortedList[i] = unsortedList[max];
                unsortedList[max] = temp;
            }
        }
    }

    public void SelectionSortByManaAmount(List<GameObject> unsortedList)
    {
        int min;
        GameObject temp;

        for (int i = 0; i < unsortedList.Count; i++)
        {
            min = i;

            for (int j = i + 1; j < unsortedList.Count; j++)
            {
                if (enemyDeck[j].GetComponent<CardBase>().manaCost < enemyDeck[min].GetComponent<CardBase>().manaCost)
                {
                    min = j;
                }
            }

            if (min != i)
            {
                temp = unsortedList[i];
                unsortedList[i] = unsortedList[min];
                unsortedList[min] = temp;
            }
        }
    }*/
}
