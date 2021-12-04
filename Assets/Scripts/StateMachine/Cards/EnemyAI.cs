using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> enemyDeck; //the cards in the hand
    public GameObject enemyHand; //the empty game object holding the cards
    [SerializeField] GameObject placeCardPnl;
    [SerializeField] float moveSpeed;
    private bool cardChosen;

    private int currentMana, currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyDeck = new List<GameObject>(enemyHand.transform.childCount);
        currentHealth = GetComponent<CharacterBase>().currentHP;
        currentMana = GetComponent<CharacterBase>().currentMana;
        maxHealth = GetComponent<CharacterBase>().maxHP;
    }

    public void LookAtHand()
    {
        cardChosen = false;
        currentHealth = GetComponent<CharacterBase>().currentHP;
        currentMana = GetComponent<CharacterBase>().currentMana;
        enemyDeck.Clear(); //hand might have changed since last looked at, so start over
        for (int i = 0; i < enemyHand.transform.childCount; i++)
        {
            enemyDeck.Add(enemyHand.transform.GetChild(i).gameObject);
        }
    }

    public GameObject Think()
    {
        //check if we have looked at each card and decided there are none to play (zero left in enemyDeck)
        if(enemyDeck.Count == 0)
        {
            Debug.Log("No cards");
            return null; //pass
        }



        /*Note to self: Code this more efficiently*/
        //check if there is a mana bonus
        /*
        for(int i = 0; i < enemyDeck.Count; i++)
        {
            if(enemyDeck[i].GetComponent<CardBase>().manaCost == -3)
            {
                GameObject temp;
                cardChosen = true;
                temp = enemyDeck[i];
                enemyDeck[i] = enemyDeck[0];
                enemyDeck[0] = temp;
                return enemyDeck[0]; //play this card 
            }
            else if(enemyDeck[i].GetComponent<CardBase>().shieldAmount == 3)
            {
                GameObject temp;
                cardChosen = true;
                temp = enemyDeck[i];
                enemyDeck[i] = enemyDeck[0];
                enemyDeck[0] = temp;
                return enemyDeck[0]; //play this card 
            }
        }*/
        //if current health is greater than 50%, then sort by greatest attack damage
        if(currentHealth > (maxHealth/2))
        {
            SelectionSortByManaAmount(enemyDeck);
            if(enemyDeck[0].GetComponent<CardBase>().manaCost == 0)
            {
                return enemyDeck[0];
            }
            else
            {
                SelectionSortByAttack(enemyDeck);
                Debug.Log("SortByAttack");
                return MoveBasedOnMana();
            }

        }
        //if less than 50%, sort by health
        else
        {
            SelectionSortByHealAmount(enemyDeck);
            //if there are no healing cards, sort by damage cards
            if(enemyDeck[0].GetComponent<CardBase>().healAmount == 0)
            {
                SelectionSortByAttack(enemyDeck);

                return MoveBasedOnMana();
            }
            else
            {
                return MoveBasedOnMana();
            }
        }


    }

    private GameObject MoveBasedOnMana()
    {
        //if card costs more mana than what enemy has...
        if (enemyDeck[0].GetComponent<CardBase>().manaCost > currentMana)
        {
            Debug.Log("Not enough mana");
            Debug.Log("Card mana cost: " + enemyDeck[0].GetComponent<CardBase>().manaCost + " and current mana is: " + currentMana);
            //check if card can be reached by passing twice
            if (enemyDeck[0].GetComponent<CardBase>().manaCost > currentMana + 2)
            {
                Debug.Log("Too little mana");
                enemyDeck.RemoveAt(0);
                Think();
                if(cardChosen)
                {
                    return enemyDeck[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Debug.Log("null");
                return null; //pass
            }

        }
        else
        {
            Debug.Log("Playing card");
            cardChosen = true;
            return enemyDeck[0]; //play this card
        }
    }

    public void SelectionSortByAttack(List<GameObject> unsortedList)
    {
        int max;
        GameObject temp;

        for(int i = 0; i < unsortedList.Count; i++)
        {
            max = i;

            for(int j = i + 1; j  < unsortedList.Count; j++)
            {
                if(enemyDeck[j].GetComponent<CardBase>().attackAmount > enemyDeck[max].GetComponent<CardBase>().attackAmount)
                {
                    max = j;
                }
            }

            if(max != i)
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
    }

    // Update is called once per frame
    void Update()
    {
        //move chosen card and then "flip" card while also playing placement sound
        if(cardChosen)
        {
            Sprite changeToOriginalImage;
            enemyDeck[0].transform.SetParent(enemyHand.transform.parent, false);
            enemyDeck[0].transform.position = Vector2.MoveTowards(enemyDeck[0].transform.position, placeCardPnl.transform.position, moveSpeed * Time.deltaTime);
            changeToOriginalImage = enemyDeck[0].GetComponent<CardBase>().originalImage;
            enemyDeck[0].transform.position = new Vector3(enemyDeck[0].transform.position.x, enemyDeck[0].transform.position.y, 0);
            if (enemyDeck[0].transform.position.magnitude == placeCardPnl.transform.position.magnitude)
            {
                Debug.Log("Finished Moving");
                enemyDeck[0].GetComponent<Image>().sprite = changeToOriginalImage;
                FindObjectOfType<AudioManager>().Play("CardPlace");
                cardChosen = false; //stop moving card
            }
        }
    }
}
