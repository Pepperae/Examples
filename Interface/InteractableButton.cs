using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRadius = 5f;
            Collider2D[] collArray = Physics2D.OverlapCircleAll(playerTransform.position, interactRadius);
            foreach (Collider2D collider2D in collArray)
            {
                IInteractable trigger = collider2D.GetComponent<IInteractable>();
                if(trigger != null)
                    trigger.ToggleInteractable();
            }
        }
    }
}