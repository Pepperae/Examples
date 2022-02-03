using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void SetInteractableActive();

    void SetInteractableInactive();

    void ToggleInteractable();
}
