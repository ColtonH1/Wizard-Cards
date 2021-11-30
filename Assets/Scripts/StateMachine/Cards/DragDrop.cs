using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragDrop : MonoBehaviour
{
    public GameObject canvas;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject newCollision;


    private void Awake()
    {
        canvas = GameObject.Find("UIController");
    }

    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        newCollision = collision.gameObject;
        isOverDropZone = true;
        /*
        if(dropZone == null)
        {
            isOverDropZone = true;
            dropZone = collision.gameObject;
            Debug.Log("Entering collision with " + dropZone.tag);
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (dropZone == null)
        {
            isOverDropZone = true;
            dropZone = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if(dropZone == collision.gameObject)
        {
            isOverDropZone = false;
            dropZone = null;
        }

    }

    public void StartDrag()
    {
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        bool canPlay = false;
        if (dropZone != null && dropZone.tag == "Player Drop Zone")
            canPlay = gameObject.GetComponent<CardBase>().CheckManaAvailable();
        else if (dropZone != null && (dropZone.tag == "Discard Drop Zone" || dropZone.tag == "Player Card Area Drop Zone"))
            canPlay = true;
        if (isOverDropZone && canPlay)
        {
            if(dropZone.tag == "Discard Drop Zone" || dropZone.tag == "Player Drop Zone")
            {
                if(dropZone.transform.childCount > 0 && dropZone.transform.GetChild(0) != transform)
                {
                    while (dropZone.transform.childCount > 0)
                    {
                        Transform child;
                        Transform newParent = transform.parent;
                        child = dropZone.transform.GetChild(0);
                        child.transform.position = startPosition;
                        child.transform.SetParent(startParent.transform);
                    }
                }

            }

            transform.SetParent(dropZone.transform, false);
            FindObjectOfType<AudioManager>().Play("CardPlace");
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
}
