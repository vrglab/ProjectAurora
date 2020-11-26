using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MinigType
{
    Hand = 0,
    Pickaxe = 1,
    Axe = 2,
    Shovel = 3

}

public abstract class Ore : MonoBehaviour
{
    protected ItemObject ItemTodrop;
    protected int resistent;
    protected MinigType MineType;

    public Ore(ItemObject todrop,int resitance, MinigType mineType)
    {
        ItemTodrop = todrop;
        resistent = resitance;
        MineType = mineType;
    }

    public abstract void Drop();

    public virtual ItemObject GetItemToDrop()
    {
        return ItemTodrop;
    }

    public virtual int GetResitance()
    {
        return resistent;
    }

    public virtual MinigType GetMinieType()
    {
        return MineType;
    }
}
