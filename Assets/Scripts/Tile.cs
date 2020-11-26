using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





public class Tile : BlockEntity
{
    public Tile(int x, int y) : base(x, y)
    {
    }

    public Tile(int x, int y, int z) : base(x, y, z)
    {
    }

    public Tile(int x, int y, TileBlockType type) : base(x, y, type)
    {
    }

    public override TileBlockType tt { get => base.tt; set => base.tt = value; }
    public override TileObject to { get => base.to; set => base.to = value; }
    public override Rigidbody2D rg { get => base.rg; set => base.rg = value; }
    public override BoxCollider2D bc2 { get => base.bc2; set => base.bc2 = value; }
    public override GameObject go { get => base.go; set => base.go = value; }
    public override float x { get => base.x; set => base.x = value; }
    public override float y { get => base.y; set => base.y = value; }
    public override float z { get => base.z; set => base.z = value; }
    public override bool HasRidigdbody2D { get => base.HasRidigdbody2D; set => base.HasRidigdbody2D = value; }
    public override bool HasCollider2D { get => base.HasCollider2D; set => base.HasCollider2D = value; }
    public override bool HasSpriteRenderer { get => base.HasSpriteRenderer; set => base.HasSpriteRenderer = value; }
    public override bool IsSolid { get => base.IsSolid; set => base.IsSolid = value; }

    public override Vector2 Get2DPosition()
    {
        return base.Get2DPosition();
    }

    public override Vector3 Get3DPosition()
    {
        return base.Get3DPosition();
    }

    public override Collider2D GetCollider()
    {
        return base.GetCollider();
    }

    public override Rigidbody2D GetRigidbody()
    {
        return base.GetRigidbody();
    }

    public override SpriteRenderer GetSpRenderer()
    {
        return base.GetSpRenderer();
    }

    public override Sprite GetSprite()
    {
        return base.GetSprite();
    }

    public override TileBlockType GetTiletype()
    {
        return base.GetTiletype();
    }

    public override bool IsBlockSolid()
    {
        return base.IsBlockSolid();
    }



    public override void SetPos(Vector2 pos)
    {
        base.SetPos(pos);
    }

    public override void SetPos(Vector3 pos)
    {
        base.SetPos(pos);
    }

    public override void SetSprite(Sprite sprite)
    {
        base.SetSprite(sprite);
    }

    public override void SetTiletype(TileBlockType type)
    {
        base.SetTiletype(type);
    }

  

  
}


[Serializable]
public class TileData
{
    [NonSerialized]
    private GameObject go;

    [SerializeField]
    public int tt;
    public Sprite sprite;

    [SerializeField]
    //Component Data
    public bool HasRidigdbody2D;
    public bool HasCollider2D;
    public bool HasSpriteRenderer;

    [SerializeField]
    //In componanet data
    public bool IsC2DTrriger;
    public float GravetyScale;
    public string sortingLayername;

    [SerializeField]
    //Position data
    public Vector3 pos3D;
    public Vector2 pos2D;


    [SerializeField]
    //Gameobject data
    public string GoName;

    [SerializeField]
    //Gameobject transform data
    public float xScale;
    public float yScale;
    public float zScale;

    //The normal config
    public TileData(TileBlockType tt, bool hasRB2D, bool HasC2D,float x, float y, float z, Sprite sprite, GameObject go, bool isC2Dtrriger, float gravetyscale, bool SpriteRenderer, string SortingName)
    {
        //Tile data 
        if(tt == TileBlockType.empty)
        {
            this.tt = 0;
        }
        else if(tt == TileBlockType.grass)
        {
            this.tt = 1;
        }
        else if (tt == TileBlockType.stone)
        {
            this.tt = 2;
        } 
        else if (tt == TileBlockType.IronOre)
        {
            this.tt = 3;
        }

        //Position data
        Vector3 T3Dpos = new Vector3(x, y, z);
        Vector2 T2Dpos = new Vector2(x, y);

        this.pos3D = T3Dpos;
        this.pos2D = T2Dpos;

        //Componant data
        HasRidigdbody2D = hasRB2D;
        HasCollider2D = HasC2D;
        HasSpriteRenderer = SpriteRenderer;

        //in Componant data
        IsC2DTrriger = isC2Dtrriger;
        GravetyScale = gravetyscale;
        sortingLayername = SortingName;

        //Image data
        this.sprite = sprite;




        //Gameobject data
        this.go = go;


        GoName = this.go.name;


        //Gameobject transform data
        xScale = this.go.transform.localScale.x;
        yScale = this.go.transform.localScale.y;
        zScale = this.go.transform.localScale.z;


    }

    //Ref safe
    public TileData(TileBlockType tt, bool hasRB2D, bool HasC2D, float x, float y, float z, Sprite sprite, GameObject go, float gravetyscale, bool SpriteRenderer, string SortingName)
    {
        //Tile data 
        if (tt == TileBlockType.empty)
        {
            this.tt = 0;
        }
        else if (tt == TileBlockType.grass)
        {
            this.tt = 1;
        }
        else if (tt == TileBlockType.stone)
        {
            this.tt = 2;
        }
        else if (tt == TileBlockType.IronOre)
        {
            this.tt = 3;
        }

        //Position data
        Vector3 T3Dpos = new Vector3(x, y, z);
        Vector2 T2Dpos = new Vector2(x, y);

        this.pos3D = T3Dpos;
        this.pos2D = T2Dpos;

        //Componant data
        HasRidigdbody2D = hasRB2D;
        HasCollider2D = HasC2D;
        HasSpriteRenderer = SpriteRenderer;

        //in Componant data
        GravetyScale = gravetyscale;
        sortingLayername = SortingName;

        //Image data
        this.sprite = sprite;




        //Gameobject data
        this.go = go;


        GoName = this.go.name;


        //Gameobject transform data
        xScale = this.go.transform.localScale.x;
        yScale = this.go.transform.localScale.y;
        zScale = this.go.transform.localScale.z;


    }
    public TileData(TileBlockType tt, bool hasRB2D, bool HasC2D, float x, float y, float z, Sprite sprite, GameObject go, bool SpriteRenderer, string SortingName)
    {
        //Tile data 
        if (tt == TileBlockType.empty)
        {
            this.tt = 0;
        }
        else if (tt == TileBlockType.grass)
        {
            this.tt = 1;
        }
        else if (tt == TileBlockType.stone)
        {
            this.tt = 2;
        }
        else if (tt == TileBlockType.IronOre)
        {
            this.tt = 3;
        }

        //Position data
        Vector3 T3Dpos = new Vector3(x, y, z);
        Vector2 T2Dpos = new Vector2(x, y);

        this.pos3D = T3Dpos;
        this.pos2D = T2Dpos;

        //Componant data
        HasRidigdbody2D = hasRB2D;
        HasCollider2D = HasC2D;
        HasSpriteRenderer = SpriteRenderer;

        //in Componant data
        sortingLayername = SortingName;

        //Image data
        this.sprite = sprite;




        //Gameobject data
        this.go = go;


        GoName = this.go.name;


        //Gameobject transform data
        xScale = this.go.transform.localScale.x;
        yScale = this.go.transform.localScale.y;
        zScale = this.go.transform.localScale.z;


    }

    bool IsTargetVisible(Camera c)
    {
       
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

}









