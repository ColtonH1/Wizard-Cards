using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    [SerializeField] List<GameObject> numOfCards;
    [SerializeField] int numOfDrawCards;
    [SerializeField] float timeToWait;
    [SerializeField] Sprite backOfCard;
    public GameObject PlayerCardArea;
    public GameObject EnemyCardArea;

    private void Start()
    {
        StartCoroutine(PlaceCards(timeToWait));
    }

    IEnumerator PlaceCards(float waitTime)
    {
        for (int i = 0; i < numOfDrawCards; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject playerCard = Instantiate(numOfCards[Random.Range(0, numOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerCardArea.transform, false);
            DrawEnemyCards();
        }
    }

    private void DrawEnemyCards()
    {
        GameObject enemyCard = Instantiate(numOfCards[Random.Range(0, numOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        enemyCard.transform.SetParent(EnemyCardArea.transform, false);
        enemyCard.GetComponent<Image>().sprite = backOfCard;
    }
}
