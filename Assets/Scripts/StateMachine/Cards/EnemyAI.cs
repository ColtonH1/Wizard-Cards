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
    private GameObject card;

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
        MonteCarlo monteCarlo = gameObject.GetComponent<MonteCarlo>();
        //GameObject card;
        card = monteCarlo.Play(enemyDeck, currentMana, currentHealth, maxHealth);
        if (card != null)
            cardChosen = true;
        return card;
    }

    /*
    public GameObject Think()
    {
        //check if we have looked at each card and decided there are none to play (zero left in enemyDeck)
        if(enemyDeck.Count == 0)
        {
            Debug.Log("No cards");
            return null; //pass
        }

        int whichCard = Random.Range(0, 4);

        while(!cardChosen && !passing)
        {
            if(holdCard != null)
            {
                if(holdCard.GetComponent<CardBase>().manaCost > currentMana)
                {
                    SelectionSortByManaAmount(enemyDeck);
                    if (enemyDeck[0].GetComponent<CardBase>().manaCost <= 0)
                    {
                        cardChosen = true;
                        return enemyDeck[0];
                    }
                    else
                    {
                        passing = true;
                        return null; //pass to wait for more mana
                    }
                }
                else
                {
                    enemyDeck[0] = holdCard;
                    holdCard = null;
                    cardChosen = true;
                    return enemyDeck[0]; //play this card 
                }
                //maybe decide if ai should keep waiting for the card or to discard it

            }
            switch(whichCard)
            {
                //choose to give mana
                case 0:
                    SelectionSortByManaAmount(enemyDeck);
                    if (enemyDeck[0].GetComponent<CardBase>().manaCost < 0)
                    {
                        cardChosen = true;
                        return enemyDeck[0];
                    }
                    else
                        whichCard = Random.Range(0, 4);
                    break;
                //choose to use shield
                case 1:
                    for (int i = 0; i < enemyDeck.Count; i++)
                    {
                        if (enemyDeck[i].GetComponent<CardBase>().manaCost == 0)
                        {
                            GameObject temp;
                            cardChosen = true;
                            temp = enemyDeck[i];
                            enemyDeck[i] = enemyDeck[0];
                            enemyDeck[0] = temp;
                            return enemyDeck[0]; //play this card 
                        }
                    }
                    Think();
                    if (cardChosen)
                    {
                        return enemyDeck[0];
                    }
                    else
                    {
                        passing = true;
                        return null;
                    }
                //choose to attack
                case 2:
                    SelectionSortByAttack(enemyDeck);
                    Debug.Log("SortByAttack");
                    return MoveBasedOnMana();
                //choose to heal
                case 3:
                    if(currentHealth < maxHealth)
                    {
                        SelectionSortByHealAmount(enemyDeck);
                        //if there are no healing cards, sort by damage cards
                        if (enemyDeck[0].GetComponent<CardBase>().healAmount == 0)
                        {
                            SelectionSortByAttack(enemyDeck);

                            return MoveBasedOnMana();
                        }
                        else
                        {
                            return MoveBasedOnMana();
                        }
                    }
                    else
                    {
                        Think();
                        if (cardChosen)
                        {
                            return enemyDeck[0];
                        }
                        else
                        {
                            passing = true;
                            return null;
                        }
                    }
                default:
                    Debug.Log("Shouldn't be able to reach this statement. Check code!");
                    return null;
            }
        }
        if (cardChosen)
        {
            return enemyDeck[0];
        }
        else
        {
            passing = true;
            return null;
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

        /*
        //if current health is greater than 50%, then sort by greatest attack damage
        if(currentHealth > (maxHealth/2))
        {
            SelectionSortByManaAmount(enemyDeck);
            if(enemyDeck[0].GetComponent<CardBase>().manaCost <= 0)
            {
                cardChosen = true;
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

    private bool ShouldAIPlayThisCard()
    {
        int shouldPlayCard = Random.Range(0, 2);

        if (shouldPlayCard == 0)
            return false;
        else
            return true;
    }

    private GameObject MoveBasedOnMana()
    {
        bool playCard = ShouldAIPlayThisCard();
        //if card costs more mana than what enemy has...
        if ((enemyDeck[0].GetComponent<CardBase>().manaCost > currentMana) && (enemyDeck[0].GetComponent<CardBase>().manaCost > currentMana + 2))
        {
            //pass but remember which card the ai was wanting to play
            if(playCard)
            {
                holdCard = enemyDeck[0];
                passing = true;
                return null; 
            }
            else
            {
                enemyDeck.RemoveAt(0);
                Think();
                if (cardChosen)
                {
                    return enemyDeck[0];
                }
                else
                {
                    passing = true;
                    return null;
                }
            }

            /*
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
            if(playCard)
            {
                Debug.Log("Playing card");
                cardChosen = true;
                return enemyDeck[0]; //play this card
            }
            else
            {            
                enemyDeck.RemoveAt(0);
                Think();
                if (cardChosen)
                {
                    return enemyDeck[0];
                }
                else
                {
                    return null;
                }
            }
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
    }*/

    // Update is called once per frame
    void Update()
    {
        //move chosen card and then "flip" card while also playing placement sound
        if(cardChosen)
        {
            Sprite changeToOriginalImage;
            card.transform.SetParent(enemyHand.transform.parent, false);
            card.transform.position = Vector2.MoveTowards(card.transform.position, placeCardPnl.transform.position, moveSpeed * Time.deltaTime);
            changeToOriginalImage = card.GetComponent<CardBase>().originalImage;
            card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, 0);
            if (card.transform.position.magnitude == placeCardPnl.transform.position.magnitude)
            {
                Debug.Log("Finished Moving");
                card.GetComponent<Image>().sprite = changeToOriginalImage;
                FindObjectOfType<AudioManager>().Play("CardPlace");
                cardChosen = false; //stop moving card
            }
        }
    }
}
