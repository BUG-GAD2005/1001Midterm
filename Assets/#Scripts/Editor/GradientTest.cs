using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GradientTest : MonoBehaviour
{
    private Material mat;
   

    private void Start()
    {
        SetUV();
    }

    private void SetUV()
    {
        var index = transform.parent.GetSiblingIndex();
        var x = index % 3;
        var y = index / 3;
        Material mat = Instantiate(GetComponent<Image>().materialForRendering);
        //mat.SetColor("_Color",Color.black);
        mat.SetFloat("_x",x);
        mat.SetFloat("_y",y);
        GetComponent<Image>().material = mat;
    }
}
