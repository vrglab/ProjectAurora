using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tile", menuName = "Tiles")]
public class TileObject : ScriptableObject
{
    public TileBlockType tt;
    public GameObject gameObject;

}
