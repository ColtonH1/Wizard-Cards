using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject CardArea;

    List<GameObject> cards = new List<GameObject>();
    void Start()
    {
        cards.Add(card1);
        cards.Add(card2);
    }

    public void OnClick()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(CardArea.transform, false);
        }
    }
}
