using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ItemEntity
{
    public override Item item { get => base.item; set => base.item = value; }
    public override BoxCollider2D Collider { get => base.Collider; protected set => base.Collider = value; }
    public override Transform Trransform { get => base.Trransform; protected set => base.Trransform = value; }
    public override Rigidbody2D Rigidbody { get => base.Rigidbody; protected set => base.Rigidbody = value; }
    public override GameObject Object { get => base.Object; protected set => base.Object = value; }
   

    public override void Awake()
    {
        base.Awake();
    }

    public override void Drop()
    {
        base.Drop();
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    public override void Update()
    {
        base.Update();
    }
}
