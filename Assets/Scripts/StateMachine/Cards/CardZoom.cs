using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
    public GameObject canvas;
    public GameObject zoomCardPanel;

    private GameObject zoomCard;

    private void Awake()
    {
        canvas = GameObject.Find("UIController");
        zoomCardPanel = GameObject.Find("ZoomCard_pnl");
    }

    public void OnHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(zoomCardPanel.transform.localPosition.x, zoomCardPanel.transform.localPosition.y), Quaternion.identity);
        zoomCard.GetComponent<Image>().raycastTarget = false;
        zoomCard.transform.SetParent(canvas.transform, false);

        RectTransform rectCard = zoomCard.GetComponent<RectTransform>();
        RectTransform rectPanel = zoomCardPanel.GetComponent<RectTransform>();
        rectCard.anchorMin = rectPanel.anchorMin;
        rectCard.anchorMax = rectPanel.anchorMax;
        rectCard.pivot = new Vector2(0.5f, 0.5f);
        rectCard.sizeDelta = new Vector2(rectPanel.sizeDelta.x, rectPanel.sizeDelta.y);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
