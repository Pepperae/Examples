using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public bool activeCollision = false;

    public void SetInteractableActive()
    {
        gameObject.SetActive(gameObject);
    }

    public void SetInteractableInactive()
    {
        gameObject.SetActive(!gameObject);
    }

    public void ToggleInteractable()
    {
        if (gameObject.CompareTag("Interactable"))
        {
            activeCollision = !activeCollision;
            if (activeCollision)
            {
                SetInteractableActive();
            }
            else
            {
                SetInteractableInactive();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeCollision = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeCollision = false;
        }
    }
}
