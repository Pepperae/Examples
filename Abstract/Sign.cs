using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : Interactable
{
    //public static Sign ActiveSign { get; private set; }

    public GameObject pup;
    public PlayerController player;

    //public GameObject titleCard;
    public string signText;
    public GameObject text;
    public TextMeshProUGUI placeText;
    [Tooltip("Add: Player")] public PlayerMovement playerMove;
    

    void Start()
    {
        pup.SetActive(false);
    }

    void Update()
    {
        if (activeCollision && !pup.activeInHierarchy && (Input.GetKeyDown(KeyCode.F)))
        {
            //SetActiveSign(true);
            SetNoticeActive();
        }
        else if (pup.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
        {
            //SetActiveSign(false);
            SetNoticeInactive();
        }
    }

    public void SetNoticeActive()
    {
        pup.SetActive(true);
        text.SetActive(true);
        placeText.SetText(signText);
        playerMove.currentState = PlayerState.interact;
        Debug.Log("Option one...");
    }

    public void SetNoticeInactive()
    {
        pup.SetActive(false);
        text.SetActive(false);
        player.OnDialogEnd();
        playerMove.currentState = PlayerState.walk;
        Debug.Log("Option two...");
    }
}
