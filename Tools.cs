using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools 
{
   public static void SetRandomColor(GameObject go)
    {
        MeshRenderer mr = go.GetComponentInChildren<MeshRenderer>();
        if (mr)
        {
            mr.material.color = Random.ColorHSV();
        }
    }
}
