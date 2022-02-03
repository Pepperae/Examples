using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSetActive : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void SetInteractableActive()
    {
        gameObject.SetActive(true);
    }

    public void SetInteractableInactive()
    {
        gameObject.SetActive(false);
    }

    public void ToggleInteractable()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            SetInteractableActive();
        }
        else
        {
            SetInteractableInactive();
        }
    }
}
