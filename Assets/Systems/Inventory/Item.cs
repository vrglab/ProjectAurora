using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemTypeSlot
{
    Item = 0,
    Holdable = 1,
}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite Image;
    public string Name;
    public string Discription;
    public ItemTypeSlot Type;
    public MinigType Minnig;
    public bool Stackable;
    public int StackLimit;
    public bool Craftable;
    public string Id;
    public GameObject GameObject;

   

}
