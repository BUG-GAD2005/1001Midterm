using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

[ExecuteInEditMode]
public class GradientTileEditor : MonoBehaviour
{
    public List<Image> tiles;
    public Material tileMaterial;
    public GameObject glassObject;

    public bool execute = false;
    public bool executeOlds = false;
    public bool updateColors = false;

    public List<Image> oldTiles;

    public Color color1;
    public Color color2;
    private void Update()
    {
        if (execute)
        {
            execute = false;
            //UpdateTiles();
        }
        if (executeOlds)
        {
            executeOlds = false;
            //UpdateOldTiles();
        }
        if (updateColors)
        {
            updateColors = false;
            UpdateColor();
        }
    }

    private void UpdateTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            var tile = tiles[i];
            Material mat = Instantiate(tileMaterial);
            
            tile.color = Color.white;
            tile.sprite = null;
            tile.material = mat;
            var glass =  Instantiate(glassObject);
            glass.transform.parent = tile.transform;
            glass.transform.localPosition = new UnityEngine.Vector3(-0.4f,0,0);
            glass.transform.localScale = 0.1645003f * UnityEngine.Vector3.one;

        }
    }

    private void UpdateOldTiles()
    {
        for (int i = 0; i < oldTiles.Count; i++)
        {
            var tile = oldTiles[i];
            Material mat = Instantiate(tileMaterial);

            tile.material = mat;
        }
    }

    private void UpdateColor()
    {
        for (int i = 0; i < oldTiles.Count; i++)
        {
            Material mat = oldTiles[i].materialForRendering;
            mat.SetColor("_Color1",color1);
            mat.SetColor("_Color_1",color2);
        }
    }
}
