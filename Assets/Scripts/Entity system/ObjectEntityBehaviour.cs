using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectEntityBehaviour : MonoBehaviour
{
    public virtual TileObject to { get; set; }
    public virtual Rigidbody2D rg { get; set; }
    public virtual BoxCollider2D bc2 { get; set; }
    public virtual GameObject go { get; set; }
    public virtual float x { get; set; }
    public virtual float y { get; set; }
    public virtual float z { get; set; }
    public virtual bool HasRidigdbody2D { get; set; }
    public virtual bool HasCollider2D { get; set; }
    public virtual bool HasSpriteRenderer { get; set; }
    public virtual bool IsSolid { get; set; }
    public virtual string Name { get; set; }
}
