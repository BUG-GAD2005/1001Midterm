using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TileEditor : MonoBehaviour
{
    public Color color1;
    public Color color2;

    public bool setColor = false;
    private void Update()
    {
        if (setColor)
        {
            setColor = false;
            SetProperties();
        }
    }

    private void SetProperties()
    {
        Material mat = GetComponent<Image>().materialForRendering;
        mat.SetColor("_Color1",color1);
        mat.SetColor("_Color_1",color2);
    }
}
