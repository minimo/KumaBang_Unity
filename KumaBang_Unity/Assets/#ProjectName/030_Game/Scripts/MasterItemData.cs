using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "MyGame/Create ItemMaster", fileName = "MasterItemData" )]
public class MasterItemData : ScriptableObject {
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public int score;
}
