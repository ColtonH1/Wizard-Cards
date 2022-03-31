using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    [SerializeField] List<GameObject> numOfCards;
    //[SerializeField] int numOfDrawCards;
    //[SerializeField] float timeToWait;
    [SerializeField] Sprite backOfCard;
    public GameObject PlayerCardArea;
    public GameObject EnemyCardArea;

    private void Start()
    {
        //StartCoroutine(PlaceCards(timeToWait));
    }

    public IEnumerator PlaceStartingCards(float waitTime, int numOfDrawCards)
    {
        for (int i = 0; i < numOfDrawCards; i++)
        {
            yield return new WaitForSeconds(waitTime);
            DrawPlayerCards();
            DrawEnemyCards();
        }
    }

    private void DrawPlayerCards()
    {
        GameObject playerCard = Instantiate(numOfCards[Random.Range(0, numOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(PlayerCardArea.transform, false);
        PlayDeltSound();
    }

    private void DrawEnemyCards()
    {
        GameObject enemyCard = Instantiate(numOfCards[Random.Range(0, numOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        enemyCard.transform.SetParent(EnemyCardArea.transform, false);
        enemyCard.GetComponent<CardBase>().originalImage = enemyCard.GetComponent<Image>().sprite;
        enemyCard.GetComponent<Image>().sprite = backOfCard;

        PlayDeltSound();

        enemyCard.GetComponent<Image>().raycastTarget = false;
    }

    public IEnumerator ReplacePlayerCard(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DrawPlayerCards();
        /*
        GameObject playerCard = Instantiate(numOfCards[Random.Range(0, numOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(PlayerCardArea.transform, false);
        PlayDeltSound();*/
    }

    public IEnumerator ReplaceEnemyCard(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DrawEnemyCards();
    }

    public void PlayDeltSound()
    {
        FindObjectOfType<AudioManager>().Play("CardDelt");
    }
}
