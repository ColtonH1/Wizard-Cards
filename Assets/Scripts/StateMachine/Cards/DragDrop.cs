using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    private Vector2 startPosition;

    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        bool canPlay = gameObject.GetComponent<CardBase>().CheckManaAvailable();
        if (isOverDropZone && canPlay)
        {
            while (dropZone.transform.childCount > 0)
            {
                Transform child;
                Transform newParent = transform.parent;
                child = dropZone.transform.GetChild(0);
                child.transform.position = startPosition;
                child.transform.SetParent(newParent);
            }
            transform.SetParent(dropZone.transform, false);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
