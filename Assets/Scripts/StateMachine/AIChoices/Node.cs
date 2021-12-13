using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public GameObject cardGO;
    public int manaCost, amountOfHealthGive, amountOfDammage, amountOfShield, rank;
    private CardBase card;

    public Node(GameObject cardGO, int manaCost, int amountOfHealthGive, int amountOfDammage, int amountOfShield, CardBase card)
    {
        this.cardGO = cardGO;
        this.manaCost = manaCost;
        this.amountOfHealthGive = amountOfHealthGive;
        this.amountOfDammage = amountOfDammage;
        this.amountOfShield = amountOfShield;
        this.card = card;
        rank = -1;
    }

    public CardBase GetCardBase()
    {
        return card;
    }

    public void SetRank(int rank)
    {
        this.rank = rank;
    }

    public int GetRank()
    {
        return rank;
    }
}
