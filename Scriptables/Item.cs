using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public bool isQuestItem;
    public bool isKey;
    public bool isCurrency;
    public bool isHealth;
    public bool isEquipment;
    public bool isFood;
    public bool isMagic;
}
