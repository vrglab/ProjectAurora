using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class OptimiesTile : MonoBehaviour
{

    public Camera main;

    private void Update()
    {
        
            if (IsVisible(main) == true)
            {
                if (gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (gameObject.GetComponent<Collider2D>())
                {
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }

            }
            if (IsVisible(main) == false)
            {
                if (gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (gameObject.GetComponent<Collider2D>())
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
      

    }




    bool IsVisible(Camera c)
    {
 
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = gameObject.transform.position - new Vector3(-0.5f, -0.5f,0);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < -2)
                return false;
        }
        return true;
    }
}
