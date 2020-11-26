using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


class LayerSorter : MonoBehaviour
{

    private void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y / 1f) / +1;


    }

}


