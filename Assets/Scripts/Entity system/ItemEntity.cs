using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEntity : MonoBehaviour
{
    public virtual Item item { get; set; }
    public virtual BoxCollider2D Collider { get; protected set; }
    public virtual Transform Trransform { get; protected set; }
    public virtual Rigidbody2D Rigidbody { get; protected set; }
    public virtual GameObject Object { get; protected set; }

    

    public virtual void Awake()
    {
        Object = gameObject;
        Rigidbody = Object.GetComponent<Rigidbody2D>();
        Trransform = transform;
        Collider = Object.GetComponent<BoxCollider2D>();
   
    }

    public virtual void Update()
    {
       
   
    
    }

    public virtual void Drop()
    {
            Inventory.instance.AddItemToSlot(item);
            Destroy(Object);
    }

    public virtual void OnMouseDown()
    {
        Drop();
    }
}

public enum ItemType
{
    stick,
    stone
}

