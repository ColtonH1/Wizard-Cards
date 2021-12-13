using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCarlo : MonoBehaviour
{    
    [SerializeField] GameObject playerGO;
    public List<GameObject> enemyDeck;
    public List<Node> cards;
    public List<Node> possibleHoldCards;
    private PlayerHP player;
    

    public int currentMana, currentHealth, maxHealth, currentPlayerHealth;

    public void SetMonteCarlo(List<GameObject> enemyDeck, int currentMana, int currentHealth, int maxHealth)
    {
        this.enemyDeck = enemyDeck;

        player = playerGO.GetComponent<PlayerHP>();

        this.currentMana = currentMana;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        currentPlayerHealth = player.currentHP;

        possibleHoldCards.Clear();
    }

    public GameObject Play(List<GameObject> enemyDeck, int currentMana, int currentHealth, int maxHealth)
    {
        SetMonteCarlo(enemyDeck, currentMana, currentHealth, maxHealth);
        GameObject playCard;
        //list of information regarding each card
        
        cards = new List<Node>(enemyDeck.Count);
        foreach(GameObject card in enemyDeck)
        {
            CardBase currentCard = card.GetComponent<CardBase>();
            Node node = new Node(card, currentCard.manaCost, currentCard.healAmount, currentCard.attackAmount, currentCard.shieldAmount, currentCard);
            cards.Add(node);
        }

        playCard = Think(cards);
        return playCard;
    }

    public GameObject Think(List<Node> cards)
    {
        GameObject playCard;
        //ranking rules: 
        //0 we have to play
        //1 we might play
        //-1 we won't use, but might save for future play
        //-2 we will remove from deck, maybe discard
        for (int i = 0; i < cards.Count; i++)
        {
            //attacking card
            ThinkAboutAttacking(cards, i);

            //healing card
            ThinkAboutHealing(cards, i);

            //all other cards that don't cost mana
            ThinkAboutOther(cards, i);
        }

        playCard = WhichCardToPlay(cards);
        return playCard;
        //for every card in hand
        //play and see what happens
        //if attack card: would player die, 
            //if so, play, 
            //if not think more
        //if heal card, is AI at full health, 
            //if not, maybe play, 
            //if so, don't play
        //if no mana cost card:
            //if give mana card, would ai be able to play a card within (currentmana + given mana)
                //if so, maybe play
                //if not, save card
            //if shield card, add to maybe play
    }

    private void ThinkAboutAttacking(List<Node> cards, int i)
    {
        if (cards[i].GetCardBase().actionType == CardBase.ActionType.ATTACKING)
        {
            Debug.Log("Attack Card");
            if (cards[i].manaCost <= currentMana)
            {
                Debug.Log("Attack Card has enough mana " + cards[i].cardGO.name);
                if (cards[i].amountOfDammage > currentPlayerHealth)
                    cards[i].SetRank(0);
                else
                    cards[i].SetRank(1);
            }
            //if the card's mana cost is a lot more than what the current amount of mana is, don't even consider it
            else if (cards[i].manaCost > currentMana + 2)
            {
                Debug.Log("Attack Card is more than two points away " + cards[i].cardGO.name);
                cards[i].SetRank(-2);
            }
                
            //if we can't play it, but the mana cost is close, consider holding it and playing it later
            else
                possibleHoldCards.Add(cards[i]);
        }
    }

    private void ThinkAboutHealing(List<Node> cards, int i)
    {
        if (cards[i].GetCardBase().actionType == CardBase.ActionType.HEALING)
        {
            //if we need to heal and we have enough mana...
            if (ShouldHeal())
            {
                if (cards[i].manaCost <= currentMana)
                {
                    cards[i].SetRank(1);
                }
            }
            //if the card's mana cost is a lot more than what the current amount of mana is, don't even consider it
            else if (cards[i].manaCost > currentMana + 2)
            {
                cards[i].SetRank(-2);         
            }
 
            //if we can't play it, but the mana cost is close, consider holding it and playing it later
            else
            {
                possibleHoldCards.Add(cards[i]);
            }
                
        }
    }


    private static void ThinkAboutOther(List<Node> cards, int i)
    {
        if (cards[i].GetCardBase().actionType == CardBase.ActionType.OTHER)
        {
            cards[i].SetRank(1);
        }
    }

    public bool ShouldHeal()
    {
        float percentHealth = currentHealth / maxHealth;
        bool shouldHeal = false;
        if (percentHealth < .8f)
        {
            float randomHealth = Random.Range(0, percentHealth);
            if (randomHealth < 0.3f)
                shouldHeal = true;
            else
                shouldHeal = false;
        }
        return shouldHeal;
    }

    public GameObject WhichCardToPlay(List<Node> cards)
    {
        int pass;
        //if there's a card we have to play
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].GetRank() == 0)
            {
                return cards[i].cardGO;
            }
            if(cards[i].GetRank() == -2)
            {
                cards.Remove(cards[i]);
                i--;
            }
        }
        //randomly choose a move
        int chooseCard = Random.Range(0, cards.Count + 1);
        pass = cards.Count;
        Debug.Log("chooseCard is " + chooseCard);
        //if we chose to pass
        if(chooseCard == pass)
        {
            Debug.Log("passed");
            return null;
        }
        Debug.Log("Rank of chosen card is: " + cards[chooseCard].GetRank());
        //which card we chose to play if we didn't pass
        if(cards[chooseCard].GetRank() == 1)
        {
            Debug.Log("Rank is: " + cards[chooseCard].GetRank());
            return cards[chooseCard].cardGO;
        }
        return null;
    }

}
