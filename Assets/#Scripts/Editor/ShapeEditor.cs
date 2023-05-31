using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShapeEditor : MonoBehaviour
{
    public Color color1;
    public Color color2;

    public bool configure = false;
    public bool setColor = false;
    
    public Material tileMaterial;
    public GameObject glassObject;

    private int[] indexCoord = new int[9]
    {
        6, 7, 8,3,4,5,0,1,2
    };
    private void Update()
    {
        if (configure)
        {
            configure = false;
            Configure();
        }
        if (setColor)
        {
            setColor = false;
            SetColor();
        }
    }

    private void Configure()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tile = transform.GetChild(i).GetComponent<Image>();
            Material mat = Instantiate(tileMaterial);
            
            tile.color = Color.white;
            tile.sprite = null;
            tile.material = mat;
            var glass =  Instantiate(glassObject);
            glass.transform.parent = tile.transform;
            glass.transform.localPosition = new UnityEngine.Vector3(-0.4f,0,0);
            glass.transform.localScale = new Vector3(0.1645003f,0.1645003f,0.1645003f);
            
            var index = indexCoord[transform.GetSiblingIndex()];
            var x = index % 3;
            var y = index / 3;
            //mat.SetColor("_Color",Color.black);
            mat.SetFloat("_x",x);
            mat.SetFloat("_y",y);
        }
    }
    private void SetColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Material mat = transform.GetChild(i).GetComponent<Image>().materialForRendering;
            mat.SetColor("_Color1",color1);
            mat.SetColor("_Color_1",color2);
        }
    }
}
